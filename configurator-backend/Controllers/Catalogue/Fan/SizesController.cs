using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Fan;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

sidelengthspace ConfiguratorBackend.Controllers.Catalogue.Fan
{
    [Route("api/Fan/[controller]")]
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
                _context.FanSizes
                .AsNoTracking()
                .Select(size => new SizeListItem(size)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<SizeDto>> GetSize(int id)
        {
            var size = await _context.FanSizes
                .AsNoTracking()
                .Where(e => e.ID == id)
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
            var sizeToUpdate = await _context.FanSizes.FirstOrDefaultAsync(m => id == m.ID);

            if (sizeToUpdate is null)
            {
                return NotFound();
            }

            if (!SizeIsValid(size))
            {
                return BadRequest();
            }

            sizeToUpdate.SideLength = size.SideLength;
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
                SideLength = size.SideLength,
            };

            _context.FanSizes.Add(emptySize);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSize), new { id = emptySize.ID }, emptySize);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var sizeToDelete = await _context.FanSizes.FirstOrDefaultAsync(m => id == m.ID);

            if (sizeToDelete is null)
            {
                return NotFound();
            };

            _context.FanSizes.Remove(sizeToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SizeExists(int id)
        {
            return _context.FanSizes.Any(e => e.ID == id);
        }

        private bool SizeIsValid(SizeDbo size)
        {
            if (size.SideLength <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
