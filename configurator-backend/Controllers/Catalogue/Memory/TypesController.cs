using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Memory;
using ConfiguratorBackend.Models.Catalogue;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Memory
{
    [Route("api/Memory/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public TypesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<TypeListItem>>> GetTypes(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<TypeListItem>.CreateAsync(
                _context.MemoryTypes
                .AsNoTracking()
                .Select(type => new TypeListItem(type)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<TypeDto>> GetType(int id)
        {
            var type = await _context.MemoryTypes
                .AsNoTracking()
                .Where(e => e.ID == id)
                .FirstOrDefaultAsync();

            if (type is null)
            {
                return NotFound();
            }

            return new TypeDto(type);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutType(int id, TypeDbo type)
        {
            var typeToUpdate = await _context.MemoryTypes.FirstOrDefaultAsync(m => id == m.ID);

            if (typeToUpdate is null)
            {
                return NotFound();
            }

            if (!TypeIsValid(type))
            {
                return BadRequest(ModelState);
            }

            typeToUpdate.Name = type.Name;
            _context.Entry(typeToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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
        public async Task<ActionResult<Models.Catalogue.Memory.Type>> PostType(TypeDbo type)
        {

            if (!TypeIsValid(type))
            {
                return BadRequest(ModelState);
            }

            var emptyType = new Models.Catalogue.Memory.Type
            {
                Name = type.Name,
            };

            _context.MemoryTypes.Add(emptyType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetType), new { id = emptyType.ID }, emptyType);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var typeToDelete = await _context.MemoryTypes.FirstOrDefaultAsync(m => id == m.ID);

            if (typeToDelete is null)
            {
                return NotFound();
            };

            _context.MemoryTypes.Remove(typeToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeExists(int id)
        {
            return _context.MemoryTypes.Any(e => e.ID == id);
        }

        private bool TypeIsValid(TypeDbo type)
        {
            if (String.IsNullOrWhiteSpace(type.Name))
            {
                return false;
            }

            return true;
        }
    }
}
