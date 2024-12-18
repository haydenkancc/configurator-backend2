using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Cooler;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Cooler
{
    [Route("api/Cooler/[controller]")]
    [ApiController]
    public class RadiatorSizesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public RadiatorSizesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<RadiatorSizeListItem>>> GetRadiatorSizes(int pageIndex = 1, int pageRadiatorSize = 20)
        {
            return await PaginatedList<RadiatorSizeListItem>.CreateAsync(
                _context.CoolerRadiatorSizes
                .AsNoTracking()
                .Select(radiatorSize => new RadiatorSizeListItem(radiatorSize)),
                pageIndex,
                pageRadiatorSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<RadiatorSizeDto>> GetRadiatorSize(int id)
        {
            var radiatorSize = await _context.CoolerRadiatorSizes
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (radiatorSize is null)
            {
                return NotFound();
            }

            return new RadiatorSizeDto(radiatorSize);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutRadiatorSize(int id, RadiatorSizeDbo radiatorSize)
        {
            var radiatorSizeToUpdate = await _context.CoolerRadiatorSizes.FirstOrDefaultAsync(m => id == m.ID);

            if (radiatorSizeToUpdate is null)
            {
                return NotFound();
            }

            if (!RadiatorSizeIsValid(radiatorSize))
            {
                return BadRequest();
            }

            radiatorSizeToUpdate.Length = radiatorSize.Length;
            _context.Entry(radiatorSizeToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RadiatorSizeExists(id))
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
        public async Task<ActionResult<RadiatorSize>> PostRadiatorSize(RadiatorSizeDbo radiatorSize)
        {

            if (!RadiatorSizeIsValid(radiatorSize))
            {
                return BadRequest();
            }

            var emptyRadiatorSize = new RadiatorSize
            {
                Length = radiatorSize.Length,
            };

            _context.CoolerRadiatorSizes.Add(emptyRadiatorSize);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRadiatorSize), new { id = emptyRadiatorSize.ID }, emptyRadiatorSize);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteRadiatorSize(int id)
        {
            var radiatorSizeToDelete = await _context.CoolerRadiatorSizes.FirstOrDefaultAsync(m => id == m.ID);

            if (radiatorSizeToDelete is null)
            {
                return NotFound();
            };

            _context.CoolerRadiatorSizes.Remove(radiatorSizeToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RadiatorSizeExists(int id)
        {
            return _context.CoolerRadiatorSizes.Any(e => id == e.ID);
        }

        private bool RadiatorSizeIsValid(RadiatorSizeDbo radiatorSize)
        {
            if (radiatorSize.Length <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
