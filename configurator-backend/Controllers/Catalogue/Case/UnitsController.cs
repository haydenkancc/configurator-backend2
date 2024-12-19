using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.Case;
using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Case
{
    [Route("api/Case/[controller]")]
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
                _context.CaseUnits
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Colour)
                .Include(unit => unit.SidePanelMaterial)
                .Include(unit => unit.PrimaryMotherboardFormFactor)
                .Include(unit => unit.Size)
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

            var unit = await _context.CaseUnits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.PowerSupplyFormFactor)
                .Include(unit => unit.Size)
                .Include(unit => unit.SidePanelMaterial)
                .Include(unit => unit.IOConnectors)
                .Include(unit => unit.PowerSupplyConnectors)
                .Include(unit => unit.PrimaryMotherboardFormFactor)
                .Include(unit => unit.MotherboardFormFactors)
                .Include(unit => unit.ExpansionSlotAreas)
                .Include(unit => unit.Layouts)
                .ThenInclude(layout => layout.Panels)
                .ThenInclude(panel => panel.Fans)
                .Include(unit => unit.Layouts)
                .ThenInclude(layout => layout.Panels)
                .ThenInclude(panel => panel.Radiators)
                .Include(unit => unit.Layouts)
                .ThenInclude(layout => layout.StorageAreas)
                .ThenInclude(storageArea => storageArea.DriveBays)
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
                PowerSupplyFormFactors = await _context.PowerSupplyFormFactors.AsNoTracking().Select(e => new Models.Catalogue.PowerSupply.FormFactorDto(e)).ToListAsync(),
                MotherboardFormFactors = await _context.MotherboardFormFactors.AsNoTracking().Select(e => new Models.Catalogue.Motherboard.FormFactorDto(e)).ToListAsync(),
                Sizes = await _context.CaseSizes.AsNoTracking().Select(e => new SizeDto(e)).ToListAsync(),
                Materials = await _context.CaseMaterials.AsNoTracking().Select(e => new MaterialDto(e)).ToListAsync(),
                RadiatorSizes = await _context.CoolerRadiatorSizes.AsNoTracking().Select(e => new Models.Catalogue.Cooler.RadiatorSizeDto(e)).ToListAsync(),
                FanSizes = await _context.FanSizes.AsNoTracking().Select(e => new Models.Catalogue.Fan.SizeDto(e)).ToListAsync(),
                Brackets = await _context.PcieBrackets.AsNoTracking().Select(e => new Models.Catalogue.Pcie.BracketDto(e)).ToListAsync(),
                Panels = await _context.CasePanels.AsNoTracking().Select(e => new PanelDto(e)).ToListAsync(),
                IOConnectors = await _context.IOConnectors.AsNoTracking().Select(e => new Models.Catalogue.IO.ConnectorDto(e)).ToListAsync(),
                PowerSupplyConnectors = await _context.PowerSupplyConnectors.AsNoTracking().Select(e => new Models.Catalogue.PowerSupply.ConnectorDto(e)).ToListAsync(),
                StorageFormFactors = await _context.StorageFormFactors.AsNoTracking().Select(e => new Models.Catalogue.Storage.FormFactorDto(e)).ToListAsync(),
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

            var unitToUpdate = await _context.CaseUnits.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

            unitToUpdate.PowerSupplyFormFactorID = unit.PowerSupplyFormFactorID;
            unitToUpdate.PrimaryMotherboardFormFactorID = unit.PrimaryMotherboardFormFactorID;
            unitToUpdate.SizeID = unit.SizeID;
            unitToUpdate.SidePanelMaterialID = unit.SidePanelMaterialID;
            unitToUpdate.ExternalVolume = unit.ExternalVolume;
            unitToUpdate.Length = unit.Length;
            unitToUpdate.Width = unit.Width;
            unitToUpdate.Height = unit.Height;
            unitToUpdate.Layouts = unit.Layouts
                .Select(layout => new Layout
                {
                    MaxAirCoolerHeight = layout.MaxAirCoolerHeight,
                    MaxGraphicsProcessorUnitLength = layout.MaxGraphicsProcessorUnitLength,
                    MaxPowerSupplyLength = layout.MaxPowerSupplyLength,

                    Panels = layout.Panels.Select(layoutPanel => new LayoutPanel
                    {
                        PanelID = layoutPanel.PanelID,

                        Radiators = layoutPanel.Radiators.Select(radiator => new LayoutPanelRadiator
                        {
                            RadiatorSizeID = radiator.RadiatorSizeID,
                        }).ToList(),

                        Fans = layoutPanel.Fans.Select(fan => new LayoutPanelFan
                        {
                            FanSizeID = fan.FanSizeID,
                            FanCount = fan.FanCount,
                        }).ToList(),

                    }).ToList(),

                    StorageAreas = layout.StorageAreas.Select(area => new StorageArea
                    {
                        DriveBays = area.DriveBays.Select(driveBay => new DriveBay
                        {
                            FormFactorID = driveBay.FormFactorID,
                            DriveCount = driveBay.DriveCount
                        }).ToList()

                    }).ToList()

                }).ToList();

            unitToUpdate.IOConnectors = unit.IOConnectors
                .Select(connector => new UnitIOConnector
                {
                    ConnectorID = connector.ConnectorID,
                    ConnectorCount = connector.ConnectorCount,
                }).ToList();

            unitToUpdate.PowerSupplyConnectors = unit.PowerSupplyConnectors
                .Select(connector => new UnitPowerSupplyConnector
                {
                    ConnectorID = connector.ConnectorID,
                    ConnectorCount = connector.ConnectorCount,
                }).ToList();

            unitToUpdate.MotherboardFormFactors = _context.MotherboardFormFactors
                .Where(e => unit.MotherboardFormFactorIDs
                .Contains(e.ID))
                .ToList();

            unitToUpdate.ExpansionSlotAreas = unit.ExpansionSlotAreas
                .Select(area => new ExpansionSlotArea
                {
                    BracketID = area.BracketID,
                    SlotCount = area.SlotCount,
                    RiserRequired = area.RiserRequired,
                }).ToList();

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
                ComponentID = component.ID,
                PowerSupplyFormFactorID = unit.PowerSupplyFormFactorID,
                PrimaryMotherboardFormFactorID = unit.PrimaryMotherboardFormFactorID,
                SizeID = unit.SizeID,
                SidePanelMaterialID = unit.SidePanelMaterialID,
                ExternalVolume = unit.ExternalVolume,
                Length = unit.Length,
                Width = unit.Width,
                Height = unit.Height,

                Layouts = unit.Layouts
                .Select(layout => new Layout
                {
                    MaxAirCoolerHeight = layout.MaxAirCoolerHeight,
                    MaxGraphicsProcessorUnitLength = layout.MaxGraphicsProcessorUnitLength,
                    MaxPowerSupplyLength = layout.MaxPowerSupplyLength,

                    Panels = layout.Panels.Select(layoutPanel => new LayoutPanel
                    {
                        PanelID = layoutPanel.PanelID,

                        Radiators = layoutPanel.Radiators.Select(radiator => new LayoutPanelRadiator
                        {
                            RadiatorSizeID = radiator.RadiatorSizeID,
                        }).ToList(),

                        Fans = layoutPanel.Fans.Select(fan => new LayoutPanelFan
                        {
                            FanSizeID = fan.FanSizeID,
                            FanCount = fan.FanCount,
                        }).ToList(),

                    }).ToList(),

                    StorageAreas = layout.StorageAreas.Select(area => new StorageArea
                    {
                        DriveBays = area.DriveBays.Select(driveBay => new DriveBay
                        {
                            FormFactorID = driveBay.FormFactorID,
                            DriveCount = driveBay.DriveCount
                        }).ToList()

                    }).ToList()

                }).ToList(),

                IOConnectors = unit.IOConnectors
                .Select(connector => new UnitIOConnector
                {
                    ConnectorID = connector.ConnectorID,
                    ConnectorCount = connector.ConnectorCount,
                }).ToList(),

                PowerSupplyConnectors = unit.PowerSupplyConnectors
                .Select(connector => new UnitPowerSupplyConnector
                {
                    ConnectorID = connector.ConnectorID,
                    ConnectorCount = connector.ConnectorCount,
                }).ToList(),

                MotherboardFormFactors = _context.MotherboardFormFactors
                .Where(e => unit.MotherboardFormFactorIDs
                .Contains(e.ID))
                .ToList(),

                ExpansionSlotAreas = unit.ExpansionSlotAreas
                .Select(area => new ExpansionSlotArea
                {
                    BracketID = area.BracketID,
                    SlotCount = area.SlotCount,
                    RiserRequired = area.RiserRequired,
                }).ToList()
            };


            _context.CaseUnits.Add(emptyUnit);
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
            return _context.CaseUnits.Any(e => e.ComponentID == id);
        }



        private async Task<bool> UnitIsValid(UnitDbo unit)
        {
            if(!await _context.PowerSupplyFormFactors.AnyAsync(e => unit.PowerSupplyFormFactorID == e.ID) ||
               !await _context.MotherboardFormFactors.AnyAsync(e => unit.PrimaryMotherboardFormFactorID == e.ID) ||
               !await _context.CaseSizes.AnyAsync(e => unit.SizeID == e.ID) ||
               !await _context.CaseMaterials.AnyAsync(e => unit.SidePanelMaterialID == e.ID)
               )
            {
                return false;
            }

            if (unit.ExternalVolume <= 0 || unit.Length <= 0 || unit.Width <= 0 || unit.Height <= 0)
            {
                return false;
            }

            foreach (var layout in unit.Layouts)
            {
                if (layout.MaxPowerSupplyLength < 0 || layout.MaxAirCoolerHeight < 0 || layout.MaxGraphicsProcessorUnitLength < 0)
                {
                    return false;
                }

                foreach (var panel in layout.Panels)
                {
                    if(!await _context.CasePanels.AnyAsync(e => panel.PanelID == e.ID))
                    {
                        return false;
                    }

                    foreach (var radiator in panel.Radiators)
                    {
                        if (!await _context.CoolerRadiatorSizes.AnyAsync(e => radiator.RadiatorSizeID == e.ID))
                        {
                            return false;
                        }
                    }

                    foreach (var fan in panel.Fans)
                    {
                        if (fan.FanCount <= 0)
                        {
                            return false;
                        }

                        if (!await _context.FanSizes.AnyAsync(e => fan.FanSizeID == e.ID))
                        {
                            return false;
                        }
                    }
                }

                foreach (var area in layout.StorageAreas)
                {
                    foreach (var driveBay in area.DriveBays)
                    {
                        if (driveBay.DriveCount <= 0)
                        {
                            return false;
                        }

                        if (!await _context.StorageFormFactors.AnyAsync(e => driveBay.FormFactorID == e.ID))
                        {
                            return false;
                        }
                    }
                }
            }

            foreach (var connector in unit.IOConnectors)
            {
                if (!await _context.IOConnectors.AnyAsync(e => connector.ConnectorID == e.ID))
                {
                    return false;
                }
            }

            foreach (var connector in unit.PowerSupplyConnectors)
            {
                if (!await _context.PowerSupplyConnectors.AnyAsync(e => connector.ConnectorID == e.ID))
                {
                    return false;
                }
            }

            foreach (var formFactorID in unit.MotherboardFormFactorIDs)
            {
                if (!await _context.MotherboardFormFactors.AnyAsync(e => formFactorID == e.ID))
                {
                    return false;
                }
            }

            foreach (var area in unit.ExpansionSlotAreas)
            {
                if (area.SlotCount <= 0)
                {
                    return false;
                }

                if (!await _context.PcieBrackets.AnyAsync(e => area.BracketID == e.ID))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
