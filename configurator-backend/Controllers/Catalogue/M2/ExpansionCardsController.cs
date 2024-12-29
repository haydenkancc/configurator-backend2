using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.M2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.M2
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
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (expansionCard is null)
            {
                return NotFound();
            }

            return new ExpansionCardDto(expansionCard);
        }

        public async Task<ExpansionCardParams> GetExpansionCardParams()
        {
            return new ExpansionCardParams
            {
                Keys = await _context.M2Keys.AsNoTracking().Select(e => new KeyDtoSimple(e)).ToListAsync(),
                FormFactors = await _context.M2FormFactors.AsNoTracking().Select(e => new FormFactorDto(e)).ToListAsync(),
                LaneSizes = await _context.PcieSizes.AsNoTracking().Select(e => new Models.Catalogue.Pcie.SizeDto(e)).ToListAsync(),
                Versions = await _context.PcieVersions.AsNoTracking().Select(e => new Models.Catalogue.Pcie.VersionDto(e)).ToListAsync(),
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
                return BadRequest(ModelState);
            }

            expansionCardToUpdate.KeyID = expansionCard.KeyID;
            expansionCardToUpdate.FormFactorID = expansionCard.FormFactorID;
            expansionCardToUpdate.LaneSizeID = expansionCard.LaneSizeID;
            expansionCardToUpdate.VersionID = expansionCard.VersionID;

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
                return BadRequest(ModelState);
            }

            var emptyExpansionCard = new ExpansionCard
            {
                KeyID = expansionCard.KeyID,
                FormFactorID = expansionCard.FormFactorID,
                LaneSizeID = expansionCard.LaneSizeID,
                VersionID = expansionCard.VersionID,
            };

            _context.M2ExpansionCards.Add(emptyExpansionCard);

            return emptyExpansionCard;
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
