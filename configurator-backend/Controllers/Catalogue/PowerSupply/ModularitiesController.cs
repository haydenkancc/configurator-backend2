using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.PowerSupply;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.PowerSupply
{
    [Route("api/PowerSupply/[controller]")]
    [ApiController]
    public class ModularitiesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ModularitiesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ModularityListItem>>> GetModularities(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<ModularityListItem>.CreateAsync(
                _context.PowerSupplyModularities
                .AsNoTracking()
                .Select(modularity => new ModularityListItem(modularity)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ModularityDto>> GetModularity(int id)
        {
            var modularity = await _context.PowerSupplyModularities
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (modularity is null)
            {
                return NotFound();
            }

            return new ModularityDto(modularity);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutModularity(int id, ModularityDbo modularity)
        {
            var modularityToUpdate = await _context.PowerSupplyModularities.FirstOrDefaultAsync(m => id == m.ID);

            if (modularityToUpdate is null)
            {
                return NotFound();
            }

            if (!ModularityIsValid(modularity))
            {
                return BadRequest(ModelState);
            }

            modularityToUpdate.Name = modularity.Name;
            _context.Entry(modularityToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModularityExists(id))
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
        public async Task<ActionResult<Modularity>> PostModularity(ModularityDbo modularity)
        {

            if (!ModularityIsValid(modularity))
            {
                return BadRequest(ModelState);
            }

            var emptyModularity = new Modularity
            {
                Name = modularity.Name,
            };

            _context.PowerSupplyModularities.Add(emptyModularity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModularity), new { id = emptyModularity.ID }, emptyModularity);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteModularity(int id)
        {
            var modularityToDelete = await _context.PowerSupplyModularities.FirstOrDefaultAsync(m => id == m.ID);

            if (modularityToDelete is null)
            {
                return NotFound();
            };

            _context.PowerSupplyModularities.Remove(modularityToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModularityExists(int id)
        {
            return _context.PowerSupplyModularities.Any(e => id == e.ID);
        }

        private bool ModularityIsValid(ModularityDbo modularity)
        {
            if (String.IsNullOrWhiteSpace(modularity.Name))
            {
                return false;
            }

            return true;
        }
    }
}
