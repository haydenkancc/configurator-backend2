using Configurator.Data;
using configurator_backend.Models.Catalogue.Pcie;
using configurator_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace configurator_backend.Controllers.Catalogue.Pcie
{
    public class ExpansionCardsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ExpansionCardsController(CatalogueContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<ExpansionCardDto>> GetExpansionCard(int id)
        {
            var expansionCard = await _context.PcieExpansionCards
                .AsNoTracking()
                .Where(e => e.ID == id)
                .FirstOrDefaultAsync();

            if (expansionCard is null)
            {
                return NotFound();
            }

            return new ExpansionCardDto(expansionCard);
        }

        public async Task<ActionResult<ExpansionCardParams>> GetExpansionCardParams()
        {
            return new ExpansionCardParams
            {
                Brackets = await _context.PcieBrackets.AsNoTracking().ToListAsync(),
                Sizes = await _context.PcieSizes.AsNoTracking().ToListAsync(),
                Versions = await _context.PcieVersions.AsNoTracking().ToListAsync(),
            };
        }

        public async Task<IActionResult> PutExpansionCard(int id, ExpansionCardDbo expansionCard)
        {
            var expansionCardToUpdate = await _context.PcieExpansionCards.FirstOrDefaultAsync(m => id == m.ID);

            if (expansionCardToUpdate is null)
            {
                return NotFound();
            }

            if (!await ExpansionCardIsValid(expansionCard))
            {
                return BadRequest();
            }

            var physicalSize = await _context.PcieSizes.FirstOrDefaultAsync(e => expansionCard.PhysicalSizeID == e.ID);
            var laneSize = await _context.PcieSizes.FirstOrDefaultAsync(e => expansionCard.LaneSizeID == e.ID);
            var version = await _context.PcieVersions.FirstOrDefaultAsync(e => expansionCard.VersionID == e.ID);
            var bracket = await _context.PcieBrackets.FirstOrDefaultAsync(e => expansionCard.BracketID == e.ID);

            if ((physicalSize is null) || (laneSize is null) || (version is null) || (bracket is null))
            {
                return BadRequest();
            }

            expansionCardToUpdate.Bracket = bracket;
            expansionCardToUpdate.PhysicalSize = physicalSize;
            expansionCardToUpdate.LaneSize = laneSize;
            expansionCardToUpdate.Version = version;

            _context.Entry(expansionCardToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpansionCardExists(id))
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


        public async Task<ActionResult<ExpansionCard>> PostExpansionCard(ExpansionCardDbo expansionCard)
        {

            if (!await ExpansionCardIsValid(expansionCard))
            {
                return BadRequest();
            }

            var physicalSize = await _context.PcieSizes.FirstOrDefaultAsync(e => expansionCard.PhysicalSizeID == e.ID);
            var laneSize = await _context.PcieSizes.FirstOrDefaultAsync(e => expansionCard.LaneSizeID == e.ID);
            var version = await _context.PcieVersions.FirstOrDefaultAsync(e => expansionCard.VersionID == e.ID);
            var bracket = await _context.PcieBrackets.FirstOrDefaultAsync(e => expansionCard.BracketID == e.ID);

            if ((physicalSize is null) || (laneSize is null) || (version is null) || (bracket is null))
            {
                return BadRequest();
            }

            var emptyExpansionCard = new ExpansionCard
            {
                Bracket = bracket,
                PhysicalSize = physicalSize,
                LaneSize = laneSize,
                Version = version,
            };

            _context.PcieExpansionCards.Add(emptyExpansionCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpansionCard), new { id = emptyExpansionCard.ID }, emptyExpansionCard);
        }

        public async Task<IActionResult> DeleteExpansionCard(int id)
        {
            var expansionCardToDelete = await _context.PcieExpansionCards.FirstOrDefaultAsync(m => id == m.ID);

            if (expansionCardToDelete is null)
            {
                return NotFound();
            };

            _context.PcieExpansionCards.Remove(expansionCardToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpansionCardExists(int id)
        {
            return _context.PcieExpansionCards.Any(e => id == e.ID);
        }

        private async Task<bool> ExpansionCardIsValid(ExpansionCardDbo expansionCard)
        {
            if (!await _context.PcieSizes.AnyAsync(e => expansionCard.LaneSizeID == e.ID) ||
                !await _context.PcieSizes.AnyAsync(e => expansionCard.PhysicalSizeID == e.ID) ||
                !await _context.PcieVersions.AnyAsync(e => expansionCard.VersionID == e.ID)
                )
            {
                return false;
            }

            return true;
        }
    }
}
