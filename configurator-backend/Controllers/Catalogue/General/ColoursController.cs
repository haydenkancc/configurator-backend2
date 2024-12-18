using Configurator.Data;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.General
{
    [Route("api/General/[controller]")]
    [ApiController]
    public class ColoursController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ColoursController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ColourListItem>>> GetColours(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<ColourListItem>.CreateAsync(
                _context.Colours
                .AsNoTracking()
                .Select(colour => new ColourListItem(colour)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ColourDto>> GetColour(int id)
        {
            var colour = await _context.Colours
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (colour is null)
            {
                return NotFound();
            }

            return new ColourDto(colour);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutColour(int id, ColourDbo colour)
        {
            var colourToUpdate = await _context.Colours.FirstOrDefaultAsync(m => id == m.ID);

            if (colourToUpdate is null)
            {
                return NotFound();
            }

            if (!ColourIsValid(colour))
            {
                return BadRequest(ModelState);
            }

            colourToUpdate.Name = colour.Name;
            _context.Entry(colourToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColourExists(id))
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
        public async Task<ActionResult<Colour>> PostColour(ColourDbo colour)
        {

            if (!ColourIsValid(colour))
            {
                return BadRequest(ModelState);
            }

            var emptyColour = new Colour
            {
                Name = colour.Name,
            };

            _context.Colours.Add(emptyColour);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColour), new { id = emptyColour.ID }, emptyColour);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteColour(int id)
        {
            var colourToDelete = await _context.Colours.FirstOrDefaultAsync(m => id == m.ID);

            if (colourToDelete is null)
            {
                return NotFound();
            };

            _context.Colours.Remove(colourToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColourExists(int id)
        {
            return _context.Colours.Any(e => id == e.ID);
        }

        private bool ColourIsValid(ColourDbo colour)
        {
            if (String.IsNullOrWhiteSpace(colour.Name))
            {
                return false;
            }

            return true;
        }
    }
}
