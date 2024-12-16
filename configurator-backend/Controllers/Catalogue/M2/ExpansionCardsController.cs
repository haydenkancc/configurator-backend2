using Configurator.Data;
using configurator_backend.Models.Catalogue.M2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace configurator_backend.Controllers.Catalogue.M2
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
            var expansionCard = await _context.M2ExpansionCards
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
                Keys = await _context.M2Keys.AsNoTracking().ToListAsync(),
                FormFactors = await _context.M2FormFactors.AsNoTracking().ToListAsync(),
                LaneSizes = await _context.PcieSizes.AsNoTracking().ToListAsync(),
                Versions = await _context.PcieVersions.AsNoTracking().ToListAsync(),
            };
        }

        public async Task<IActionResult> PutExpansionCard(int id, ExpansionCardDbo expansionCard)
        {
            var expansionCardToUpdate = await _context.M2ExpansionCards.FirstOrDefaultAsync(m => id == m.ID);

            if (expansionCardToUpdate is null)
            {
                return NotFound();
            }

            if (!await ExpansionCardIsValid(expansionCard))
            {
                return BadRequest();
            }

            var key = await _context.M2Keys.FirstOrDefaultAsync(e => expansionCard.KeyID == e.ID);
            var formFactor = await _context.M2FormFactors.FirstOrDefaultAsync(e => expansionCard.FormFactorID == e.ID);
            var laneSize = await _context.PcieSizes.FirstOrDefaultAsync(e => expansionCard.LaneSizeID == e.ID);
            var version = await _context.PcieVersions.FirstOrDefaultAsync(e => expansionCard.VersionID == e.ID);

            if ((key is null) || (laneSize is null) || (version is null) || (formFactor is null))
            {
                return BadRequest();
            }

            expansionCardToUpdate.Key = key;
            expansionCardToUpdate.FormFactor = formFactor;
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


            var key = await _context.M2Keys.FirstOrDefaultAsync(e => expansionCard.KeyID == e.ID);
            var formFactor = await _context.M2FormFactors.FirstOrDefaultAsync(e => expansionCard.FormFactorID == e.ID);
            var laneSize = await _context.PcieSizes.FirstOrDefaultAsync(e => expansionCard.LaneSizeID == e.ID);
            var version = await _context.PcieVersions.FirstOrDefaultAsync(e => expansionCard.VersionID == e.ID);

            if ((key is null) || (laneSize is null) || (version is null) || (formFactor is null))
            {
                return BadRequest();
            }

            var emptyExpansionCard = new ExpansionCard
            {
                Key = key,
                FormFactor = formFactor,
                LaneSize = laneSize,
                Version = version,
            };

            _context.M2ExpansionCards.Add(emptyExpansionCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpansionCard), new { id = emptyExpansionCard.ID }, emptyExpansionCard);
        }

        public async Task<IActionResult> DeleteExpansionCard(int id)
        {
            var expansionCardToDelete = await _context.M2ExpansionCards.FirstOrDefaultAsync(m => id == m.ID);

            if (expansionCardToDelete is null)
            {
                return NotFound();
            };

            _context.M2ExpansionCards.Remove(expansionCardToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpansionCardExists(int id)
        {
            return _context.M2ExpansionCards.Any(e => id == e.ID);
        }

        private async Task<bool> ExpansionCardIsValid(ExpansionCardDbo expansionCard)
        {
            if (!await _context.PcieSizes.AnyAsync(e => expansionCard.LaneSizeID == e.ID) ||
                !await _context.M2FormFactors.AnyAsync(e => expansionCard.FormFactorID == e.ID) ||
                !await _context.M2Keys.AnyAsync(e => expansionCard.KeyID == e.ID) ||
                !await _context.PcieVersions.AnyAsync(e => expansionCard.VersionID == e.ID)
                )
            {
                return false;
            }

            return true;
        }
    }
}
