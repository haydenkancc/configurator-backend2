using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.CentralProcessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.CentralProcessor
{
    [Route("api/CentralProcessor/[controller]")]
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
                _context.CentralProcessorUnits
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Colour)
                .Include(unit => unit.Series)
                .Include(unit => unit.CoreFamily)
                .ThenInclude(coreFamily => coreFamily.Microarchitecture)
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

            var unit = await _context.CentralProcessorUnits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.CoreFamily)
                .ThenInclude(unit => unit.Microarchitecture)
                .Include(unit => unit.Series)
                .Include(unit => unit.Socket)
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
                Sockets = await _context.CentralProcessorSockets.AsNoTracking().Select(e => new SocketDto(e)).ToListAsync(),
                Series = await _context.CentralProcessorSeries.AsNoTracking().Select(e => new SeriesDto(e)).ToListAsync(),
                CoreFamilies = await _context.CentralProcessorCoreFamilies.AsNoTracking().Select(e => new CoreFamilyDtoSimple(e)).ToListAsync()
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

            var unitToUpdate = await _context.CentralProcessorUnits.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

            unitToUpdate.SocketID = unit.SocketID;
            unitToUpdate.SeriesID = unit.SeriesID;
            unitToUpdate.CoreFamilyID = unit.CoreFamilyID;
            unitToUpdate.ChannelCount = unit.ChannelCount;
            unitToUpdate.MaxTotalMemoryCapacity = unit.MaxTotalMemoryCapacity;
            unitToUpdate.TotalPower = unit.TotalPower;
            unitToUpdate.HasIntegratedGraphics = unit.HasIntegratedGraphics;
            unitToUpdate.CoolerIncluded = unit.CoolerIncluded;
            unitToUpdate.SupportECCMemory = unit.SupportECCMemory;
            unitToUpdate.SupportNonECCMemory = unit.SupportNonECCMemory;
            unitToUpdate.SupportBufferedMemory = unit.SupportBufferedMemory;
            unitToUpdate.SupportUnbufferedMemory = unit.SupportUnbufferedMemory;
            unitToUpdate.PerformanceCoreCount = unit.PerformanceCoreCount;
            unitToUpdate.ThreadCount = unit.ThreadCount;
            unitToUpdate.PerformanceCoreClock = unit.PerformanceCoreClock;
            unitToUpdate.PerformanceCoreBoostClock = unit.PerformanceCoreBoostClock;
            unitToUpdate.HasEfficiencyCores = unit.HasEfficiencyCores;
            unitToUpdate.EfficiencyCoreCount = unit.HasEfficiencyCores ? unit.EfficiencyCoreCount : null;
            unitToUpdate.EfficiencyCoreClock = unit.HasEfficiencyCores ? unit.EfficiencyCoreClock : null;
            unitToUpdate.EfficiencyCoreBoostClock = unit.HasEfficiencyCores ? unit.EfficiencyCoreBoostClock : null;
            unitToUpdate.L2Cache = unit.L2Cache;
            unitToUpdate.L3Cache = unit.L3Cache;
            unitToUpdate.SimultaneousMultithreading = unit.SimultaneousMultithreading;

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
                SocketID = unit.SocketID,
                SeriesID = unit.SeriesID,
                CoreFamilyID = unit.CoreFamilyID,
                ChannelCount = unit.ChannelCount,
                MaxTotalMemoryCapacity = unit.MaxTotalMemoryCapacity,
                TotalPower = unit.TotalPower,
                HasIntegratedGraphics = unit.HasIntegratedGraphics,
                CoolerIncluded = unit.CoolerIncluded,
                SupportECCMemory = unit.SupportECCMemory,
                SupportNonECCMemory = unit.SupportNonECCMemory,
                SupportBufferedMemory = unit.SupportBufferedMemory,
                SupportUnbufferedMemory = unit.SupportUnbufferedMemory,
                PerformanceCoreCount = unit.PerformanceCoreCount,
                ThreadCount = unit.ThreadCount,
                PerformanceCoreClock = unit.PerformanceCoreClock,
                PerformanceCoreBoostClock = unit.PerformanceCoreBoostClock,
                HasEfficiencyCores = unit.HasEfficiencyCores,
                EfficiencyCoreCount = unit.HasEfficiencyCores ? unit.EfficiencyCoreCount : null,
                EfficiencyCoreClock = unit.HasEfficiencyCores ? unit.EfficiencyCoreClock : null,
                EfficiencyCoreBoostClock = unit.HasEfficiencyCores ? unit.EfficiencyCoreBoostClock : null,
                L2Cache = unit.L2Cache,
                L3Cache = unit.L3Cache,
                SimultaneousMultithreading = unit.SimultaneousMultithreading
            };


            _context.CentralProcessorUnits.Add(emptyUnit);
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
            return _context.CentralProcessorUnits.Any(e => e.ComponentID == id);
        }



        private async Task<bool> UnitIsValid(UnitDbo unit)
        {

            if (unit.TotalPower <= 0 ||
                unit.ThreadCount <= 0 ||
                unit.PerformanceCoreCount <= 0 ||
                unit.PerformanceCoreClock <= 0 ||
                unit.PerformanceCoreBoostClock <= 0 ||
                unit.L2Cache <= 0 ||
                unit.L3Cache <= 0 ||
                unit.ChannelCount <= 0)
            {
                return false;
            }

            if (unit.HasEfficiencyCores && (unit.EfficiencyCoreCount is null || unit.EfficiencyCoreClock is null || unit.EfficiencyCoreBoostClock is null))
            {
                return false;
            }

            if (!await _context.CentralProcessorSockets.AnyAsync(e => e.ID == unit.SocketID) ||
                !await _context.CentralProcessorSeries.AnyAsync(e => e.ID == unit.SeriesID) ||
                !await _context.CentralProcessorCoreFamilies.AnyAsync(e => e.ID == unit.CoreFamilyID)
                )
            {
                return false;
            }

            return true;
        }
    }
}
