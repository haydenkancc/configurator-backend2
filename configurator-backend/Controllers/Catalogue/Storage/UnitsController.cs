using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;
        private readonly M2.ExpansionCardsController _expansionCardsController;
        private readonly M2UnitsController _m2UnitsController;
        private readonly CaseUnitsController _caseUnitsController;
        private readonly DrivesController _drivesController;

        public UnitsController(CatalogueContext context, ComponentsController componentsController, M2.ExpansionCardsController expansionCardsController, M2UnitsController m2UnitsController, CaseUnitsController caseUnitsController, DrivesController drivesController)
        {
            _context = context;
            _componentsController = componentsController;
            _expansionCardsController = expansionCardsController;
            _m2UnitsController = m2UnitsController;
            _caseUnitsController = caseUnitsController;
            _drivesController = drivesController;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<UnitListItem>>> GetUnits(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<UnitListItem>.CreateAsync(
                _context.StorageUnits
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
            var unit = await _context.StorageUnits.FirstOrDefaultAsync(e => id == e.ComponentID);

            if (unit is null)
            {
                return NotFound();
            }

            if (unit is M2Unit)
            {
                return await _m2UnitsController.GetM2Unit(id);
            }
            else if (unit is CaseUnit)
            {
                return await _caseUnitsController.GetCaseUnit(id);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("params")]
        public async Task<ActionResult<UnitParams>> GetUnitParams()
        {
            var unitParams = new UnitParams
            {
                Component = await _componentsController.GetComponentParams(),
                ExpansionCard = await _expansionCardsController.GetExpansionCardParams(),
                Drive = await _drivesController.GetDriveParams(),
                ConnectionInterfaces = await _context.StorageConnectionInterfaces.Select(e => new ConnectionInterfaceDto(e)).ToListAsync(),
                FormFactors = await _context.StorageFormFactors.Select(e => new FormFactorDto(e)).ToListAsync(),
                PowerSupplyConnectors = await _context.PowerSupplyConnectors.Select(e => new Models.Catalogue.PowerSupply.ConnectorDtoSimple(e)).ToListAsync(),
                IOConnectors = await _context.IOConnectors.Select(e => new Models.Catalogue.IO.ConnectorDtoSimple(e)).ToListAsync(),
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

            if (unit.Location is Location.M2)
            {
                return await _m2UnitsController.PutM2Unit(id, unit.M2Unit!);
            }
            else if (unit.Location is Location.Case)
            {
                return await _caseUnitsController.PutCaseUnit(id, unit.CaseUnit!);
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

            if (unit.Location is Location.M2)
            {
                return await _m2UnitsController.PostM2Unit(unit.M2Unit!);
            }
            else if (unit.Location is Location.Case)
            {
                return await _caseUnitsController.PostCaseUnit(unit.CaseUnit!);
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

            if (unit.Location is Location.M2 && unit.M2Unit is null)
            {
                return false;
            }
            else if (unit.Location is Location.Case && unit.CaseUnit is null)
            {
                return false;
            }

            return true;
        }
    }
}
