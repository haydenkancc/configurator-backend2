using Configurator.Data;
using configurator_backend.Models.Catalogue.Pcie;
using configurator_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace configurator_backend.Controllers.Catalogue.Pcie
{
    [Route("api/Pcie/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public SizesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<SizeListItem>>> GetSizes(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<SizeListItem>.CreateAsync(
                _context.PcieSizes
                .AsNoTracking()
                .Select(size => new SizeListItem(size)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<SizeDto>> GetSize(int id)
        {
            var size = await _context.PcieSizes
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (size is null)
            {
                return NotFound();
            }

            return new SizeDto(size);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutSize(int id, SizeDbo size)
        {
            var sizeToUpdate = await _context.PcieSizes.FirstOrDefaultAsync(m => id == m.ID);

            if (sizeToUpdate is null)
            {
                return NotFound();
            }

            if (!SizeIsValid(size))
            {
                return BadRequest();
            }

            sizeToUpdate.LaneCount = size.LaneCount;
            _context.Entry(sizeToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizeExists(id))
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
        public async Task<ActionResult<Size>> PostSize(SizeDbo size)
        {

            if (!SizeIsValid(size))
            {
                return BadRequest();
            }

            var emptySize = new Size
            {
                LaneCount = size.LaneCount,
            };

            _context.PcieSizes.Add(emptySize);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSize), new { id = emptySize.ID }, emptySize);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var sizeToDelete = await _context.PcieSizes.FirstOrDefaultAsync(m => id == m.ID);

            if (sizeToDelete is null)
            {
                return NotFound();
            };

            _context.PcieSizes.Remove(sizeToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SizeExists(int id)
        {
            return _context.PcieSizes.Any(e => id == e.ID);
        }

        private bool SizeIsValid(SizeDbo size)
        {
            if (size.LaneCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
