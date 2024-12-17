using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.M2;
using ConfiguratorBackend.Models.Catalogue;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.M2
{
    [Route("api/M2/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public SlotsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<SlotListItem>>> GetSlots(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<SlotListItem>.CreateAsync(
                _context.M2Slots
                .AsNoTracking()
                .Select(slot => new SlotListItem(slot)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<SlotDto>> GetSlot(int id)
        {
            var slot = await _context.M2Slots
                .AsNoTracking()
                .Where(e => e.ID == id)
                .FirstOrDefaultAsync();

            if (slot is null)
            {
                return NotFound();
            }

            return new SlotDto(slot);
        }

        [HttpGet("params/{params}")]
        public async Task<ActionResult<SlotParams>> GetSlotParams()
        {
            return new SlotParams
            {
                Keys = await _context.M2Keys.AsNoTracking().ToListAsync(),
                FormFactors = await _context.M2FormFactors.AsNoTracking().ToListAsync(),
                LaneSizes = await _context.PcieSizes.AsNoTracking().ToListAsync(),
                Versions = await _context.PcieVersions.AsNoTracking().ToListAsync(),
            };
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutSlot(int id, SlotDbo slot)
        {
            var slotToUpdate = await _context.M2Slots.FirstOrDefaultAsync(m => id == m.ID);

            if (slotToUpdate is null)
            {
                return NotFound();
            }

            if (!await SlotIsValid(slot))
            {
                return BadRequest();
            }

            _context.M2Slots.Entry(slotToUpdate).Collection(e => e.FormFactors).Load();

            slotToUpdate.KeyID = slot.KeyID;
            slotToUpdate.FormFactors = await _context.M2FormFactors.Where(e => slot.FormFactorIDs.Contains(e.ID)).ToListAsync();
            slotToUpdate.LaneSizeID = slot.LaneSizeID;
            slotToUpdate.VersionID = slot.VersionID;

            _context.Entry(slotToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
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
        public async Task<ActionResult<Slot>> PostSlot(SlotDbo slot)
        {

            if (!await SlotIsValid(slot))
            {
                return BadRequest();
            }

            var emptySlot = new Slot
            {
                KeyID = slot.KeyID,
                FormFactors = await _context.M2FormFactors.Where(e => slot.FormFactorIDs.Contains(e.ID)).ToListAsync(),
                LaneSizeID = slot.LaneSizeID,
                VersionID = slot.VersionID,
            };

            _context.M2Slots.Add(emptySlot);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSlot), new { id = emptySlot.ID }, emptySlot);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var slotToDelete = await _context.M2Slots.FirstOrDefaultAsync(m => id == m.ID);

            if (slotToDelete is null)
            {
                return NotFound();
            };

            _context.M2Slots.Remove(slotToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotExists(int id)
        {
            return _context.M2Slots.Any(e => id == e.ID);
        }

        private async Task<bool> SlotIsValid(SlotDbo slot)
        {
            if (!await _context.M2Keys.AnyAsync(e => slot.KeyID == e.ID) ||
                !await _context.PcieSizes.AnyAsync(e => slot.LaneSizeID == e.ID) ||
                !await _context.PcieVersions.AnyAsync(e => slot.VersionID == e.ID)
                )
            {
                return false;
            }

            foreach (var formFactorID in slot.FormFactorIDs)
            {
                if (!await _context.M2FormFactors.AnyAsync(e => formFactorID == e.ID))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
