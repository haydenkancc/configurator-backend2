using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.Motherboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Motherboard
{
    [Route("api/Motherboard/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;

        public UnitsController(CatalogueContext context, ComponentsController componentsController)
        {
            _context = context;
            _componentsController = componentsController;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<UnitListItem>>> GetUnits(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<UnitListItem>.CreateAsync(
                _context.MotherboardUnits
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Colour)
                .Include(unit => unit.Chipset)
                .ThenInclude(chipset => chipset.Socket)
                .Include(unit => unit.FormFactor)
                .Select(unit => new UnitListItem(unit)),
                pageIndex,
                pageSize
            );
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UnitDto>> GetUnit(int id)
        {
            var c = await _componentsController.GetComponent(id);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }

            var unit = await _context.MotherboardUnits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.IOConnectors)
                .Include(unit => unit.M2Slots)
                .ThenInclude(slot => slot.Series)
                .Include(unit => unit.M2Slots)
                .ThenInclude(slot => slot.Processors)
                .Include(unit => unit.M2Slots)
                .ThenInclude(slot => slot.CoreFamilies)
                .Include(unit => unit.PcieSlots)
                .ThenInclude(slot => slot.Series)
                .Include(unit => unit.PcieSlots)
                .ThenInclude(slot => slot.Processors)
                .Include(unit => unit.PcieSlots)
                .ThenInclude(slot => slot.CoreFamilies)
                .Include(unit => unit.PowerSupplyConnectors)
                .Include(unit => unit.Chipset)
                .Include(unit => unit.FormFactor)
                .Include(unit => unit.MemoryFormFactor)
                .Include(unit => unit.MemoryType)
                .FirstOrDefaultAsync();

            if (unit is null)
            {
                return NotFound();
            }

            return new UnitDto(component, unit);
        }

        [HttpGet("params")]
        public async Task<ActionResult<UnitParams>> GetUnitParams()
        {
            var unitParams = new UnitParams
            {
                Component = await _componentsController.GetComponentParams(),

                FormFactors = await _context.MotherboardFormFactors.AsNoTracking().Select(e => new FormFactorDto(e)).ToListAsync(),
                Chipsets = await _context.MotherboardChipsets
                    .AsNoTracking()
                    .Include(e => e.Socket)
                    .Select(e => new ChipsetDtoSimple(e))
                    .ToListAsync(),

                IOConnectors = await _context.IOConnectors.AsNoTracking().Select(e => new Models.Catalogue.IO.ConnectorDtoSimple(e)).ToListAsync(),
                PowerSupplyConnectors = await _context.PowerSupplyConnectors.AsNoTracking().Select(e => new Models.Catalogue.PowerSupply.ConnectorDtoSimple(e)).ToListAsync(),

                Processors = await _context.CentralProcessorUnits
                    .AsNoTracking()
                    .Include(e => e.Component)
                    .ThenInclude(e => e.Manufacturer)
                    .Include(e => e.Series)
                    .Select(e => new Models.Catalogue.CentralProcessor.UnitDtoSimple(e))
                    .ToListAsync(),

                Series = await _context.CentralProcessorSeries.AsNoTracking().Select(e => new Models.Catalogue.CentralProcessor.SeriesDto(e)).ToListAsync(),
                CoreFamilies = await _context.CentralProcessorCoreFamilies.AsNoTracking().Select(e => new Models.Catalogue.CentralProcessor.CoreFamilyDtoSimple(e)).ToListAsync(),
                MemoryFormFactors = await _context.MemoryFormFactors.AsNoTracking().Select(e => new Models.Catalogue.Memory.FormFactorDto(e)).ToListAsync(),
                MemoryTypes = await _context.MemoryTypes.AsNoTracking().Select(e => new Models.Catalogue.Memory.TypeDto(e)).ToListAsync(),

                M2Slots = await _context.M2Slots
                .AsNoTracking()
                .Include(e => e.FormFactors)
                .Include(e => e.Key)
                .Include(e => e.Version)
                .Include(e => e.LaneSize)
                .Select(slot => new Models.Catalogue.M2.SlotDtoSimple(slot))
                .ToListAsync(),

                PcieSlots = await _context.PcieSlots
                .AsNoTracking()
                .Include(e => e.LaneSize)
                .Include(e => e.PhysicalSize)
                .Include(e => e.Version)
                .Select(slot => new Models.Catalogue.Pcie.SlotDtoSimple(slot))
                .ToListAsync(),
            };

            return unitParams;
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutUnit(int id, UnitDbo unit)
        {

            var componentResult = await _componentsController.PutComponent(id, unit.Component);

            if (componentResult is not NoContentResult)
            {
                return componentResult;
            }

            var unitToUpdate = await _context.MotherboardUnits.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

            unitToUpdate.IOConnectors = unit.IOConnectors.Select(e => new UnitIOConnector
            {
                ConnectorID = e.ConnectorID,
                ConnectorCount = e.ConnectorCount
            }).ToList();

            unitToUpdate.M2Slots = unit.M2Slots.Select(slot => new UnitM2Slot
            {
                SlotID = slot.SlotID,
                SlotPosition = slot.SlotPosition,
                Series = _context.CentralProcessorSeries.Where(e => slot.SeriesIDs.Contains(e.ID)).ToList(),
                Processors = _context.CentralProcessorUnits.Where(e => slot.ProcessorIDs.Contains(e.ComponentID)).ToList(),
                CoreFamilies = _context.CentralProcessorCoreFamilies.Where(e => slot.CoreFamilyIDs.Contains(e.ID)).ToList(),
                HasConfigurationNumber = slot.ConfigurationNumber > 0,
                ConfigurationNumber = slot.ConfigurationNumber,
            }).ToList();

            unitToUpdate.PcieSlots = unit.PcieSlots.Select(slot => new UnitPcieSlot
            {
                SlotID = slot.SlotID,
                SlotPosition = slot.SlotPosition,
                Series = _context.CentralProcessorSeries.Where(e => slot.SeriesIDs.Contains(e.ID)).ToList(),
                Processors = _context.CentralProcessorUnits.Where(e => slot.ProcessorIDs.Contains(e.ComponentID)).ToList(),
                CoreFamilies = _context.CentralProcessorCoreFamilies.Where(e => slot.CoreFamilyIDs.Contains(e.ID)).ToList(),
                HasConfigurationNumber = slot.ConfigurationNumber > 0,
                ConfigurationNumber = slot.ConfigurationNumber,
            }).ToList();

            unitToUpdate.PowerSupplyConnectors = unit.PowerSupplyConnectors.Select(e => new UnitPowerSupplyConnector
            {
                ConnectorID = e.ConnectorID,
                ConnectorCount = e.ConnectorCount
            }).ToList();

            unitToUpdate.ChipsetID = unit.ChipsetID;
            unitToUpdate.FormFactorID = unit.FormFactorID;
            unitToUpdate.ChannelCount = unit.ChannelCount;
            unitToUpdate.MemoryFormFactorID = unit.MemoryFormFactorID;
            unitToUpdate.MemoryTypeID = unit.MemoryTypeID;
            unitToUpdate.MemoryTotalCapacity = unit.MemoryTotalCapacity;


            unitToUpdate.MemorySlotCount = unit.MemorySlotCount;
            unitToUpdate.SupportECCMemory = unit.SupportECCMemory;
            unitToUpdate.SupportNonECCMemory = unit.SupportNonECCMemory;
            unitToUpdate.SupportBufferedMemory = unit.SupportBufferedMemory;
            unitToUpdate.SupportUnbufferedMemory = unit.SupportUnbufferedMemory;

            _context.Entry(unitToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(UnitDbo unit)
        {
            var c = await _componentsController.PostComponent(unit.Component);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }


            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            var emptyUnit = new Unit
            {
                Component = component,

                IOConnectors = unit.IOConnectors.Select(e => new UnitIOConnector
                {
                    ConnectorID = e.ConnectorID,
                    ConnectorCount = e.ConnectorCount
                }).ToList(),

                M2Slots = unit.M2Slots.Select(slot => new UnitM2Slot
                {
                    SlotID = slot.SlotID,
                    SlotPosition = slot.SlotPosition,
                    Series = _context.CentralProcessorSeries.Where(e => slot.SeriesIDs.Contains(e.ID)).ToList(),
                    Processors = _context.CentralProcessorUnits.Where(e => slot.ProcessorIDs.Contains(e.ComponentID)).ToList(),
                    CoreFamilies = _context.CentralProcessorCoreFamilies.Where(e => slot.CoreFamilyIDs.Contains(e.ID)).ToList(),
                    HasConfigurationNumber = slot.ConfigurationNumber > 0,
                    ConfigurationNumber = slot.ConfigurationNumber,
                }).ToList(),

                PcieSlots = unit.PcieSlots.Select(slot => new UnitPcieSlot
                {
                    SlotID = slot.SlotID,
                    SlotPosition = slot.SlotPosition,
                    Series = _context.CentralProcessorSeries.Where(e => slot.SeriesIDs.Contains(e.ID)).ToList(),
                    Processors = _context.CentralProcessorUnits.Where(e => slot.ProcessorIDs.Contains(e.ComponentID)).ToList(),
                    CoreFamilies = _context.CentralProcessorCoreFamilies.Where(e => slot.CoreFamilyIDs.Contains(e.ID)).ToList(),
                    HasConfigurationNumber = slot.ConfigurationNumber > 0,
                    ConfigurationNumber = slot.ConfigurationNumber,
                }).ToList(),

                PowerSupplyConnectors = unit.PowerSupplyConnectors.Select(e => new UnitPowerSupplyConnector
                {
                    ConnectorID = e.ConnectorID,
                    ConnectorCount = e.ConnectorCount
                }).ToList(),

                ChipsetID = unit.ChipsetID,
                FormFactorID = unit.FormFactorID,
                ChannelCount = unit.ChannelCount,
                MemoryFormFactorID = unit.MemoryFormFactorID,
                MemoryTypeID = unit.MemoryTypeID,
                MemoryTotalCapacity = unit.MemoryTotalCapacity,


                MemorySlotCount = unit.MemorySlotCount,
                SupportECCMemory = unit.SupportECCMemory,
                SupportNonECCMemory = unit.SupportNonECCMemory,
                SupportBufferedMemory = unit.SupportBufferedMemory,
                SupportUnbufferedMemory = unit.SupportUnbufferedMemory
            };


            _context.MotherboardUnits.Add(emptyUnit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUnit), new { id = emptyUnit.ComponentID }, emptyUnit);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            return await _componentsController.DeleteComponent(id);
        }

        private bool UnitExists(int id)
        {
            return _context.MotherboardUnits.Any(e => e.ComponentID == id);
        }



        private async Task<bool> UnitIsValid(UnitDbo unit)
        {
            if (unit.MemoryTotalCapacity < 0 || unit.MemorySlotCount < 0 || unit.ChannelCount < 0)
            {
                return false;
            }

            if (!await _context.MotherboardChipsets.AnyAsync(e => e.ID == unit.ChipsetID) ||
                !await _context.MotherboardFormFactors.AnyAsync(e => e.ID == unit.FormFactorID) ||
                !await _context.MemoryFormFactors.AnyAsync(e => e.ID == unit.MemoryFormFactorID) ||
                !await _context.MemoryTypes.AnyAsync(e => e.ID == unit.MemoryTypeID)
               )
            {
                return false;
            }

            foreach (var connector in unit.IOConnectors)
            {
                if (connector.ConnectorCount <= 0)
                {
                    return false;
                }

                if (!await _context.IOConnectors.AnyAsync(e => connector.ConnectorID == e.ID))
                {
                    return false;
                }
            }

            foreach (var connector in unit.PowerSupplyConnectors)
            {
                if (connector.ConnectorCount <= 0)
                {
                    return false;
                }

                if (!await _context.PowerSupplyConnectors.AnyAsync(e => connector.ConnectorID == e.ID))
                {
                    return false;
                }
            }

            foreach (var slot in unit.M2Slots)
            {
                if (slot.SlotPosition <= 0)
                {
                    return false;
                }

                if (!await _context.M2Slots.AnyAsync(e => slot.SlotID == e.ID) || !await UnitSlotIsValid(slot.SeriesIDs, slot.CoreFamilyIDs, slot.ProcessorIDs))
                {
                    return false;
                }
            }

            foreach (var slot in unit.PcieSlots)
            {
                if (slot.SlotPosition <= 0)
                {
                    return false;
                }

                if (!await _context.PcieSlots.AnyAsync(e => slot.SlotID == e.ID) || !await UnitSlotIsValid(slot.SeriesIDs, slot.CoreFamilyIDs, slot.ProcessorIDs))
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> UnitSlotIsValid(ICollection<int> seriesIDs, ICollection<int> coreFamilyIDs, ICollection<int> processorIDs)
        {
            foreach (var seriesID in seriesIDs)
            {
                if (!await _context.CentralProcessorSeries.AnyAsync(e => seriesID == e.ID))
                {
                    return false;
                }
            }

            foreach (var coreFamilyID in coreFamilyIDs)
            {
                if (!await _context.CentralProcessorCoreFamilies.AnyAsync(e => coreFamilyID == e.ID))
                {
                    return false;
                }
            }

            foreach (var processorID in processorIDs)
            {
                if (!await _context.CentralProcessorUnits.AnyAsync(e => processorID == e.ComponentID))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
