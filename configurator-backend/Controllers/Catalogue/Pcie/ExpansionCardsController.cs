using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Pcie;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace ConfiguratorBackend.Controllers.Catalogue.Pcie
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
                .Where(e => id == e.ID)
                .Include(e => e.Bracket)
                .Include(e => e.Version)
                .Include(e => e.LaneSize)
                .Include(e => e.PhysicalSize)
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
                Brackets = await _context.PcieBrackets.AsNoTracking().Select(e => new BracketDto(e)).ToListAsync(),
                Sizes = await _context.PcieSizes.AsNoTracking().Select(e => new SizeDto(e)).ToListAsync(),
                Versions = await _context.PcieVersions.AsNoTracking().Select(e => new VersionDto(e)).ToListAsync(),
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

            expansionCardToUpdate.BracketID = expansionCard.BracketID;
            expansionCardToUpdate.PhysicalSizeID = expansionCard.PhysicalSizeID;
            expansionCardToUpdate.LaneSizeID = expansionCard.LaneSizeID;
            expansionCardToUpdate.VersionID = expansionCard.VersionID;
            expansionCardToUpdate.ExpansionSlotWidth = expansionCard.ExpansionSlotWidth;

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

            var emptyExpansionCard = new ExpansionCard
            {
                BracketID = expansionCard.BracketID,
                PhysicalSizeID = expansionCard.PhysicalSizeID,
                LaneSizeID = expansionCard.LaneSizeID,
                VersionID = expansionCard.VersionID,
                ExpansionSlotWidth = expansionCard.ExpansionSlotWidth
            };

            _context.PcieExpansionCards.Add(emptyExpansionCard);

            return emptyExpansionCard;
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
            if (expansionCard.ExpansionSlotWidth <= 0)
            {
                return false;
            }

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
