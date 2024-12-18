using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.Cooler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Cooler
{
    [Route("api/Cooler/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;
        private readonly AirUnitsController _airUnitsController;
        private readonly LiquidUnitsController _liquidUnitsController;

        public UnitsController(CatalogueContext context, ComponentsController componentsController, AirUnitsController airUnitsController, LiquidUnitsController liquidUnitsController)
        {
            _context = context;
            _componentsController = componentsController;
            _airUnitsController = airUnitsController;
            _liquidUnitsController = liquidUnitsController;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<UnitListItem>>> GetUnits(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<UnitListItem>.CreateAsync(
                _context.CoolerUnits
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Colour)
                .Select(unit => new UnitListItem(unit)),
                pageIndex,
                pageSize
            );
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UnitDto>> GetUnit(int id)
        {
            var unit = await _context.CoolerUnits.FirstOrDefaultAsync(e => id == e.ComponentID);

            if (unit is null)
            {
                return NotFound();
            }

            if (unit is AirUnit)
            {
                return await _airUnitsController.GetAirUnit(id);
            }
            else if (unit is LiquidUnit)
            {
                return await _liquidUnitsController.GetLiquidUnit(id);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("params")]
        public async Task<ActionResult<UnitParams>> GetUnitParams()
        {
            var unitParams = new UnitParams
            {
                Component = await _componentsController.GetComponentParams(),
                Sockets = await _context.CentralProcessorSockets.AsNoTracking().Select(e => new Models.Catalogue.CentralProcessor.SocketDto(e)).ToListAsync(),
                RadiatorSizes = await _context.CoolerRadiatorSizes.AsNoTracking().Select(e => new RadiatorSizeDto(e)).ToListAsync(),
                Connectors = await _context.IOConnectors.AsNoTracking().Select(e => new Models.Catalogue.IO.ConnectorDtoSimple(e)).ToListAsync(),
            };

            return unitParams;
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutUnit(int id, UnitDbo unit)
        {
            if (!UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

            if (unit.Type is Models.Catalogue.Cooler.Type.Air)
            {
                return await _airUnitsController.PutAirUnit(id, unit.AirUnit!);
            }
            else if (unit.Type is Models.Catalogue.Cooler.Type.Liquid)
            {
                return await _liquidUnitsController.PutLiquidUnit(id, unit.LiquidUnit!);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(UnitDbo unit)
        {
            if (!UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

            if (unit.Type is Models.Catalogue.Cooler.Type.Air)
            {
                return await _airUnitsController.PostAirUnit(unit.AirUnit!);
            }
            else if (unit.Type is Models.Catalogue.Cooler.Type.Liquid)
            {
                return await _liquidUnitsController.PostLiquidUnit(unit.LiquidUnit!);
            }

            return BadRequest(ModelState);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            return await _componentsController.DeleteComponent(id);
        }

        private bool UnitIsValid(UnitDbo unit)
        {

            if (unit.Type is Models.Catalogue.Cooler.Type.Air && unit.AirUnit is null)
            {
                return false;
            }
            else if (unit.Type is Models.Catalogue.Cooler.Type.Liquid && unit.LiquidUnit is null)
            {
                return false;
            }

            return true;
        }
    }
}
