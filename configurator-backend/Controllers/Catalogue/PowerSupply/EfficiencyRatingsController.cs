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
    public class EfficiencyRatingsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public EfficiencyRatingsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<EfficiencyRatingListItem>>> GetEfficiencyRatings(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<EfficiencyRatingListItem>.CreateAsync(
                _context.PowerSupplyEfficiencyRatings
                .AsNoTracking()
                .Select(efficiencyRating => new EfficiencyRatingListItem(efficiencyRating)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<EfficiencyRatingDto>> GetEfficiencyRating(int id)
        {
            var efficiencyRating = await _context.PowerSupplyEfficiencyRatings
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (efficiencyRating is null)
            {
                return NotFound();
            }

            return new EfficiencyRatingDto(efficiencyRating);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutEfficiencyRating(int id, EfficiencyRatingDbo efficiencyRating)
        {
            var efficiencyRatingToUpdate = await _context.PowerSupplyEfficiencyRatings.FirstOrDefaultAsync(m => id == m.ID);

            if (efficiencyRatingToUpdate is null)
            {
                return NotFound();
            }

            if (!EfficiencyRatingIsValid(efficiencyRating))
            {
                return BadRequest(ModelState);
            }

            efficiencyRatingToUpdate.Name = efficiencyRating.Name;
            _context.Entry(efficiencyRatingToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EfficiencyRatingExists(id))
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
        public async Task<ActionResult<EfficiencyRating>> PostEfficiencyRating(EfficiencyRatingDbo efficiencyRating)
        {

            if (!EfficiencyRatingIsValid(efficiencyRating))
            {
                return BadRequest(ModelState);
            }

            var emptyEfficiencyRating = new EfficiencyRating
            {
                Name = efficiencyRating.Name,
            };

            _context.PowerSupplyEfficiencyRatings.Add(emptyEfficiencyRating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEfficiencyRating), new { id = emptyEfficiencyRating.ID }, emptyEfficiencyRating);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteEfficiencyRating(int id)
        {
            var efficiencyRatingToDelete = await _context.PowerSupplyEfficiencyRatings.FirstOrDefaultAsync(m => id == m.ID);

            if (efficiencyRatingToDelete is null)
            {
                return NotFound();
            };

            _context.PowerSupplyEfficiencyRatings.Remove(efficiencyRatingToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EfficiencyRatingExists(int id)
        {
            return _context.PowerSupplyEfficiencyRatings.Any(e => id == e.ID);
        }

        private bool EfficiencyRatingIsValid(EfficiencyRatingDbo efficiencyRating)
        {
            if (String.IsNullOrWhiteSpace(efficiencyRating.Name))
            {
                return false;
            }

            return true;
        }
    }
}
