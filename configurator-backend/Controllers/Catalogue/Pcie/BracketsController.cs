using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Pcie;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Pcie
{
    [Route("api/Pcie/[controller]")]
    [ApiController]
    public class BracketsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public BracketsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<BracketListItem>>> GetBrackets(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<BracketListItem>.CreateAsync(
                _context.PcieBrackets
                .AsNoTracking()
                .Select(bracket => new BracketListItem(bracket)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<BracketDto>> GetBracket(int id)
        {
            var bracket = await _context.PcieBrackets
                .AsNoTracking()
                .Where(e => e.ID == id)
                .FirstOrDefaultAsync();

            if (bracket is null)
            {
                return NotFound();
            }

            return new BracketDto(bracket);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutBracket(int id, BracketDbo bracket)
        {
            var bracketToUpdate = await _context.PcieBrackets.FirstOrDefaultAsync(m => id == m.ID);

            if (bracketToUpdate is null)
            {
                return NotFound();
            }

            if (!BracketIsValid(bracket))
            {
                return BadRequest();
            }

            bracketToUpdate.Name = bracket.Name;
            _context.Entry(bracketToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BracketExists(id))
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
        public async Task<ActionResult<Bracket>> PostBracket(BracketDbo bracket)
        {

            if (!BracketIsValid(bracket))
            {
                return BadRequest();
            }

            var emptyBracket = new Bracket
            {
                Name = bracket.Name,
            };

            _context.PcieBrackets.Add(emptyBracket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBracket), new { id = emptyBracket.ID }, emptyBracket);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteBracket(int id)
        {
            var bracketToDelete = await _context.PcieBrackets.FirstOrDefaultAsync(m => id == m.ID);

            if (bracketToDelete is null)
            {
                return NotFound();
            };

            _context.PcieBrackets.Remove(bracketToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BracketExists(int id)
        {
            return _context.PcieBrackets.Any(e => e.ID == id);
        }

        private bool BracketIsValid(BracketDbo bracket)
        {
            if (String.IsNullOrWhiteSpace(bracket.Name))
            {
                return false;
            }

            return true;
        }
    }
}
