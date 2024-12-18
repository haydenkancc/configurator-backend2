using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.Memory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Memory
{
    [Route("api/Memory/[controller]")]
    [ApiController]
    public class KitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;

        public KitsController(CatalogueContext context, ComponentsController componentsController)
        {
            _context = context;
            _componentsController = componentsController;
        }

        public async Task<ActionResult<PaginatedList<KitListItem>>> GetKits(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<KitListItem>.CreateAsync(
                _context.MemoryKits
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(kit => kit.Type)
                .Select(kit => new KitListItem(kit)),
                pageIndex,
                pageSize
            );
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<KitDto>> GetKit(int id)
        {
            var c = await _componentsController.GetComponent(id);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }

            var kit = await _context.MemoryKits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(kit => kit.Type)
                .Include(kit => kit.FormFactor)
                .FirstOrDefaultAsync();

            if (kit is null)
            {
                return NotFound();
            }

            return new KitDto(component, kit);
        }

        [HttpGet("params")]
        public async Task<ActionResult<KitParams>> GetKitParams()
        {
            var kitParams = new KitParams
            {
                Component = await _componentsController.GetComponentParams(),

                Types = await _context.MemoryTypes
                .AsNoTracking()
                .ToListAsync(),

                FormFactors = await _context.MemoryFormFactors
                .AsNoTracking()
                .ToListAsync(),
            };

            return kitParams;
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutKit(int id, KitDbo kit)
        {

            var componentResult = await _componentsController.PutComponent(id, kit.Component);

            if (componentResult is not NoContentResult)
            {
                return componentResult;
            }

            var kitToUpdate = await _context.MemoryKits.FirstOrDefaultAsync(kit => id == kit.ComponentID);

            if (kitToUpdate == null)
            {
                return NotFound();
            }

            if (!await KitIsValid(kit))
            {
                return BadRequest(ModelState);
            }

            kitToUpdate.FormFactorID = kit.FormFactorID;
            kitToUpdate.TypeID = kit.FormFactorID;
            kitToUpdate.Capacity = kit.Capacity;


            kitToUpdate.ClockFrequency = kit.ClockFrequency;
            kitToUpdate.Height = kit.Height;
            kitToUpdate.ModuleCount = kit.ModuleCount;
            kitToUpdate.CASLatency = kit.CASLatency;
            kitToUpdate.FirstWordLatency = kit.FirstWordLatency;
            kitToUpdate.Voltage = kit.Voltage;
            kitToUpdate.Timing = kit.Timing;
            kitToUpdate.IsECC = kit.IsECC;
            kitToUpdate.IsBuffered = kit.IsBuffered;

            _context.Entry(kitToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KitExists(id))
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
        public async Task<ActionResult<Kit>> PostKit(KitDbo kit)
        {
            var c = await _componentsController.PostComponent(kit.Component);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }


            if (!await KitIsValid(kit))
            {
                return BadRequest(ModelState);
            }


            var emptyKit = new Kit
            {
                ComponentID = component.ID,
                FormFactorID = kit.FormFactorID,
                TypeID = kit.TypeID,
                Capacity = kit.Capacity,


                ClockFrequency = kit.ClockFrequency,
                Height = kit.Height,
                ModuleCount = kit.ModuleCount,
                CASLatency = kit.CASLatency,
                FirstWordLatency = kit.FirstWordLatency,
                Voltage = kit.Voltage,
                Timing = kit.Timing,
                IsECC = kit.IsECC,
                IsBuffered = kit.IsBuffered
            };


            _context.MemoryKits.Add(emptyKit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetKit), new { id = emptyKit.ComponentID }, emptyKit);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteKit(int id)
        {
            return await _componentsController.DeleteComponent(id);
        }

        private bool KitExists(int id)
        {
            return _context.MemoryKits.Any(e => e.ComponentID == id);
        }



        private async Task<bool> KitIsValid(KitDbo kit)
        {
            if (String.IsNullOrWhiteSpace(kit.Timing) ||
                kit.ClockFrequency <= 0 ||
                kit.Height <= 0 ||
                kit.CASLatency <= 0 ||
                kit.FirstWordLatency <= 0 ||
                kit.Voltage <= 0)
            {
                return false;
            }

            if (!await _context.MemoryFormFactors.AnyAsync(e => e.ID == kit.FormFactorID) ||
                !await _context.MemoryTypes.AnyAsync(e => e.ID == kit.TypeID)
                )
            {
                return false;
            }

            return true;
        }
    }
}
