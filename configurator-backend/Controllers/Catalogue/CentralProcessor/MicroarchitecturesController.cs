using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.CentralProcessor;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.CentralProcessor
{
    [Route("api/CentralProcessor/[controller]")]
    [ApiController]
    public class MicroarchitecturesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public MicroarchitecturesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<MicroarchitectureListItem>>> GetMicroarchitectures(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<MicroarchitectureListItem>.CreateAsync(
                _context.CentralProcessorMicroarchitectures
                .AsNoTracking()
                .Select(microarchitecture => new MicroarchitectureListItem(microarchitecture)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<MicroarchitectureDto>> GetMicroarchitecture(int id)
        {
            var microarchitecture = await _context.CentralProcessorMicroarchitectures
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (microarchitecture is null)
            {
                return NotFound();
            }

            return new MicroarchitectureDto(microarchitecture);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutMicroarchitecture(int id, MicroarchitectureDbo microarchitecture)
        {
            var microarchitectureToUpdate = await _context.CentralProcessorMicroarchitectures.FirstOrDefaultAsync(m => id == m.ID);

            if (microarchitectureToUpdate is null)
            {
                return NotFound();
            }

            if (!MicroarchitectureIsValid(microarchitecture))
            {
                return BadRequest();
            }

            microarchitectureToUpdate.Name = microarchitecture.Name;
            _context.Entry(microarchitectureToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MicroarchitectureExists(id))
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
        public async Task<ActionResult<Microarchitecture>> PostMicroarchitecture(MicroarchitectureDbo microarchitecture)
        {

            if (!MicroarchitectureIsValid(microarchitecture))
            {
                return BadRequest();
            }

            var emptyMicroarchitecture = new Microarchitecture
            {
                Name = microarchitecture.Name,
            };

            _context.CentralProcessorMicroarchitectures.Add(emptyMicroarchitecture);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMicroarchitecture), new { id = emptyMicroarchitecture.ID }, emptyMicroarchitecture);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteMicroarchitecture(int id)
        {
            var microarchitectureToDelete = await _context.CentralProcessorMicroarchitectures.FirstOrDefaultAsync(m => id == m.ID);

            if (microarchitectureToDelete is null)
            {
                return NotFound();
            };

            _context.CentralProcessorMicroarchitectures.Remove(microarchitectureToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MicroarchitectureExists(int id)
        {
            return _context.CentralProcessorMicroarchitectures.Any(e => id == e.ID);
        }

        private bool MicroarchitectureIsValid(MicroarchitectureDbo microarchitecture)
        {
            if (String.IsNullOrWhiteSpace(microarchitecture.Name))
            {
                return false;
            }

            return true;
        }
    }
}
