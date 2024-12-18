using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.Fan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace ConfiguratorBackend.Controllers.Catalogue.Fan
{
    [Route("api/Fan/[controller]")]
    [ApiController]
    public class PacksController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;

        public PacksController(CatalogueContext context, ComponentsController componentsController)
        {
            _context = context;
            _componentsController = componentsController;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<PackListItem>>> GetPacks(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<PackListItem>.CreateAsync(
                _context.FanPacks
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Colour)
                .Include(pack => pack.Size)
                .Select(pack => new PackListItem(pack)),
                pageIndex,
                pageSize
            );
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<PackDto>> GetPack(int id)
        {
            var c = await _componentsController.GetComponent(id);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }

            var pack = await _context.FanPacks
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(pack => pack.Size)
                .Include(pack => pack.Connectors)
                .FirstOrDefaultAsync();

            if (pack is null)
            {
                return NotFound();
            }

            return new PackDto(component, pack);
        }

        [HttpGet("params")]
        public async Task<ActionResult<PackParams>> GetPackParams()
        {
            var packParams = new PackParams
            {
                Component = await _componentsController.GetComponentParams(),

                Sizes = await _context.FanSizes.AsNoTracking().Select(e => new SizeDto(e)).ToListAsync(),
                Connectors = await _context.IOConnectors.AsNoTracking().Select(e => new Models.Catalogue.IO.ConnectorDtoSimple(e)).ToListAsync(),
            };

            return packParams;
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutPack(int id, PackDbo pack)
        {

            var componentResult = await _componentsController.PutComponent(id, pack.Component);

            if (componentResult is not NoContentResult)
            {
                return componentResult;
            }

            var packToUpdate = await _context.FanPacks.FirstOrDefaultAsync(pack => id == pack.ComponentID);

            if (packToUpdate == null)
            {
                return NotFound();
            }

            if (!await PackIsValid(pack))
            {
                return BadRequest(ModelState);
            }

            packToUpdate.SizeID = pack.SizeID;
            packToUpdate.Quantity = pack.Quantity;
            packToUpdate.Rpm = pack.Rpm;
            packToUpdate.Airflow = pack.Airflow;
            packToUpdate.NoiseLevel = pack.NoiseLevel;
            packToUpdate.StaticPressure = pack.StaticPressure;
            packToUpdate.Pwm = pack.Pwm;
            packToUpdate.Connectors = pack.Connectors
                .Select(e => new PackConnector
                {
                    ConnectorID = e.ConnectorID,
                    ConnectorCount = e.ConnectorCount,
                })
                .ToList();

            _context.Entry(packToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackExists(id))
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
        public async Task<ActionResult<Pack>> PostPack(PackDbo pack)
        {
            var c = await _componentsController.PostComponent(pack.Component);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }


            if (!await PackIsValid(pack))
            {
                return BadRequest(ModelState);
            }


            var emptyPack = new Pack
            {
                ComponentID = component.ID,
                SizeID = pack.SizeID,
                Quantity = pack.Quantity,
                Rpm = pack.Rpm,
                Airflow = pack.Airflow,
                NoiseLevel = pack.NoiseLevel,
                StaticPressure = pack.StaticPressure,
                Pwm = pack.Pwm,

                Connectors = pack.Connectors
                    .Select(e => new PackConnector
                    {
                        ConnectorID = e.ConnectorID,
                        ConnectorCount = e.ConnectorCount,
                    })
                    .ToList()
            };



            _context.FanPacks.Add(emptyPack);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPack), new { id = emptyPack.ComponentID }, emptyPack);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeletePack(int id)
        {
            return await _componentsController.DeleteComponent(id);
        }

        private bool PackExists(int id)
        {
            return _context.FanPacks.Any(e => e.ComponentID == id);
        }



        private async Task<bool> PackIsValid(PackDbo pack)
        {
            if (String.IsNullOrWhiteSpace(pack.Rpm) ||
                String.IsNullOrWhiteSpace(pack.Airflow) ||
                String.IsNullOrWhiteSpace(pack.NoiseLevel) ||
                String.IsNullOrWhiteSpace(pack.StaticPressure) ||
                pack.Quantity <= 0
                )
            {
                return false;
            }

            if (!await _context.FanSizes.AnyAsync(e => e.ID == pack.SizeID))
            {
                return false;
            }

            foreach (var connector in pack.Connectors)
            {
                if (!await _context.IOConnectors.AnyAsync(e => connector.ConnectorID == e.ID))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
