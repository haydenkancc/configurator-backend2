using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.GraphicsCard;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.GraphicsCard
{
    [Route("api/GraphicsCard/[controller]")]
    [ApiController]
    public class ChipsetsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ChipsetsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ChipsetListItem>>> GetChipsets(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<ChipsetListItem>.CreateAsync(
                _context.GraphicsCardChipsets
                .AsNoTracking()
                .Select(chipset => new ChipsetListItem(chipset)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ChipsetDto>> GetChipset(int id)
        {
            var chipset = await _context.GraphicsCardChipsets
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (chipset is null)
            {
                return NotFound();
            }

            return new ChipsetDto(chipset);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutChipset(int id, ChipsetDbo chipset)
        {
            var chipsetToUpdate = await _context.GraphicsCardChipsets.FirstOrDefaultAsync(m => id == m.ID);

            if (chipsetToUpdate is null)
            {
                return NotFound();
            }

            if (!ChipsetIsValid(chipset))
            {
                return BadRequest();
            }

            chipsetToUpdate.Name = chipset.Name;
            _context.Entry(chipsetToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChipsetExists(id))
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
        public async Task<ActionResult<Chipset>> PostChipset(ChipsetDbo chipset)
        {

            if (!ChipsetIsValid(chipset))
            {
                return BadRequest();
            }

            var emptyChipset = new Chipset
            {
                Name = chipset.Name,
            };

            _context.GraphicsCardChipsets.Add(emptyChipset);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChipset), new { id = emptyChipset.ID }, emptyChipset);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteChipset(int id)
        {
            var chipsetToDelete = await _context.GraphicsCardChipsets.FirstOrDefaultAsync(m => id == m.ID);

            if (chipsetToDelete is null)
            {
                return NotFound();
            };

            _context.GraphicsCardChipsets.Remove(chipsetToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChipsetExists(int id)
        {
            return _context.GraphicsCardChipsets.Any(e => id == e.ID);
        }

        private bool ChipsetIsValid(ChipsetDbo chipset)
        {
            if (String.IsNullOrWhiteSpace(chipset.Name))
            {
                return false;
            }

            return true;
        }
    }
}
