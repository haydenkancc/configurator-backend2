using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Motherboard;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConfiguratorBackend.Controllers.Catalogue.General;

namespace ConfiguratorBackend.Controllers.Catalogue.Motherboard
{
    [Route("api/Motherboard/[controller]")]
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
                _context.MotherboardChipsets
                .AsNoTracking()
                .Select(chipset => new ChipsetListItem(chipset)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ChipsetDto>> GetChipset(int id)
        {
            var chipset = await _context.MotherboardChipsets
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (chipset is null)
            {
                return NotFound();
            }

            return new ChipsetDto(chipset);
        }

        [HttpGet("params")]
        public async Task<ActionResult<ChipsetParams>> GetChipsetParams()
        {
            var chipsetParams = new ChipsetParams
            {
                Sockets = await _context.CentralProcessorSockets.AsNoTracking().Select(e => new Models.Catalogue.CentralProcessor.SocketDto(e)).ToListAsync(),
            };

            return chipsetParams;
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutChipset(int id, ChipsetDbo chipset)
        {
            var chipsetToUpdate = await _context.MotherboardChipsets.FirstOrDefaultAsync(m => id == m.ID);

            if (chipsetToUpdate is null)
            {
                return NotFound();
            }

            if (!await ChipsetIsValid(chipset))
            {
                return BadRequest();
            }

            chipsetToUpdate.SocketID = chipset.SocketID;
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

            if (!await ChipsetIsValid(chipset))
            {
                return BadRequest();
            }

            var emptyChipset = new Chipset
            {
                SocketID = chipset.SocketID,
                Name = chipset.Name,
            };

            _context.MotherboardChipsets.Add(emptyChipset);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChipset), new { id = emptyChipset.ID }, emptyChipset);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteChipset(int id)
        {
            var chipsetToDelete = await _context.MotherboardChipsets.FirstOrDefaultAsync(m => id == m.ID);

            if (chipsetToDelete is null)
            {
                return NotFound();
            };

            _context.MotherboardChipsets.Remove(chipsetToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChipsetExists(int id)
        {
            return _context.MotherboardChipsets.Any(e => id == e.ID);
        }

        private async Task<bool> ChipsetIsValid(ChipsetDbo chipset)
        {
            if (String.IsNullOrWhiteSpace(chipset.Name))
            {
                return false;
            }

            if (!await _context.CentralProcessorSockets.AnyAsync(e => chipset.SocketID == e.ID))
            {
                return false;
            }

            return true;
        }
    }
}
