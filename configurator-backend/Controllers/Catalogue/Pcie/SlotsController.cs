using Configurator.Data;
using configurator_backend.Models.Catalogue.Pcie;
using configurator_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace configurator_backend.Controllers.Catalogue.Pcie
{
    [Route("api/Pcie/[controller]")]
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
                _context.PcieSlots
                .AsNoTracking()
                .Select(slot => new SlotListItem(slot)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<SlotDto>> GetSlot(int id)
        {
            var slot = await _context.PcieSlots
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
            return new SlotParams {
                Sizes = await _context.PcieSizes.AsNoTracking().ToListAsync(),
                Versions = await _context.PcieVersions.AsNoTracking().ToListAsync(),
            };
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutSlot(int id, SlotDbo slot)
        {
            var slotToUpdate = await _context.PcieSlots.FirstOrDefaultAsync(m => id == m.ID);

            if (slotToUpdate is null)
            {
                return NotFound();
            }

            if (!await SlotIsValid(slot))
            {
                return BadRequest();
            }

            var physicalSize = await _context.PcieSizes.FirstOrDefaultAsync(e => slot.PhysicalSizeID == e.ID);
            var laneSize = await _context.PcieSizes.FirstOrDefaultAsync(e => slot.LaneSizeID == e.ID);
            var version = await _context.PcieVersions.FirstOrDefaultAsync(e => slot.VersionID == e.ID);

            if ((physicalSize is null) || (laneSize is null) || (version is null))
            {
                return BadRequest();
            }

            slotToUpdate.PhysicalSize = physicalSize;
            slotToUpdate.LaneSize = laneSize;
            slotToUpdate.Version = version;

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

            var physicalSize = await _context.PcieSizes.FirstOrDefaultAsync(e => slot.PhysicalSizeID == e.ID);
            var laneSize = await _context.PcieSizes.FirstOrDefaultAsync(e => slot.LaneSizeID == e.ID);
            var version = await _context.PcieVersions.FirstOrDefaultAsync(e => slot.VersionID == e.ID);

            if ((physicalSize is null) || (laneSize is null) || (version is null))
            {
                return BadRequest();
            }

            var emptySlot = new Slot
            {
                PhysicalSize = physicalSize,
                LaneSize = laneSize,
                Version = version,
            };

            _context.PcieSlots.Add(emptySlot);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSlot), new { id = emptySlot.ID }, emptySlot);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var slotToDelete = await _context.PcieSlots.FirstOrDefaultAsync(m => id == m.ID);

            if (slotToDelete is null)
            {
                return NotFound();
            };

            _context.PcieSlots.Remove(slotToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotExists(int id)
        {
            return _context.PcieSlots.Any(e => id == e.ID);
        }

        private async Task<bool> SlotIsValid(SlotDbo slot)
        {
            if (!await _context.PcieSizes.AnyAsync(e => slot.LaneSizeID == e.ID) ||
                !await _context.PcieSizes.AnyAsync(e => slot.PhysicalSizeID == e.ID) ||
                !await _context.PcieVersions.AnyAsync(e => slot.VersionID == e.ID)
                )
            {
                return false;
            }

            return true;
        }
    }
}
