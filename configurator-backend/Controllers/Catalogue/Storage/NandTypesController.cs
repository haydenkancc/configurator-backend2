using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Storage;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class NandTypesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public NandTypesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<NandTypeListItem>>> GetNandTypes(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<NandTypeListItem>.CreateAsync(
                _context.StorageNandTypes
                .AsNoTracking()
                .Select(nandType => new NandTypeListItem(nandType)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<NandTypeDto>> GetNandType(int id)
        {
            var nandType = await _context.StorageNandTypes
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (nandType is null)
            {
                return NotFound();
            }

            return new NandTypeDto(nandType);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutNandType(int id, NandTypeDbo nandType)
        {
            var nandTypeToUpdate = await _context.StorageNandTypes.FirstOrDefaultAsync(m => id == m.ID);

            if (nandTypeToUpdate is null)
            {
                return NotFound();
            }

            if (!NandTypeIsValid(nandType))
            {
                return BadRequest(ModelState);
            }

            nandTypeToUpdate.Name = nandType.Name;
            _context.Entry(nandTypeToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NandTypeExists(id))
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
        public async Task<ActionResult<NandType>> PostNandType(NandTypeDbo nandType)
        {

            if (!NandTypeIsValid(nandType))
            {
                return BadRequest(ModelState);
            }

            var emptyNandType = new NandType
            {
                Name = nandType.Name,
            };

            _context.StorageNandTypes.Add(emptyNandType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNandType), new { id = emptyNandType.ID }, emptyNandType);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteNandType(int id)
        {
            var nandTypeToDelete = await _context.StorageNandTypes.FirstOrDefaultAsync(m => id == m.ID);

            if (nandTypeToDelete is null)
            {
                return NotFound();
            };

            _context.StorageNandTypes.Remove(nandTypeToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NandTypeExists(int id)
        {
            return _context.StorageNandTypes.Any(e => id == e.ID);
        }

        private bool NandTypeIsValid(NandTypeDbo nandType)
        {
            if (String.IsNullOrWhiteSpace(nandType.Name))
            {
                return false;
            }

            return true;
        }
    }
}
