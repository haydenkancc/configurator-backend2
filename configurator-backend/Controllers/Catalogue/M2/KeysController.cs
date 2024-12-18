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
    public class KeysController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public KeysController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<KeyListItem>>> GetKeys(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<KeyListItem>.CreateAsync(
                _context.M2Keys
                .AsNoTracking()
                .Select(key => new KeyListItem(key)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<KeyDto>> GetKey(int id)
        {
            var key = await _context.M2Keys
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (key is null)
            {
                return NotFound();
            }

            return new KeyDto(key);
        }

        [HttpGet("params/{params}")]
        public async Task<ActionResult<KeyParams>> GetKeyParams()
        {
            return new KeyParams
            {
                Keys = await _context.M2Keys.AsNoTracking().Select(e => new KeyDtoSimple(e)).ToListAsync(),
            };
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutKey(int id, KeyDbo key)
        {
            var keyToUpdate = await _context.M2Keys.FirstOrDefaultAsync(m => id == m.ID);

            if (keyToUpdate is null)
            {
                return NotFound();
            }

            if (!await KeyIsValid(key))
            {
                return BadRequest(ModelState);
            }

            keyToUpdate.Name = key.Name;
            keyToUpdate.CompatibleKeys = await _context.M2Keys.Where(e => key.CompatibleKeyIDs.Contains(e.ID)).ToListAsync();


            _context.Entry(keyToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyExists(id))
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
        public async Task<ActionResult<Key>> PostKey(KeyDbo key)
        {

            if (!await KeyIsValid(key))
            {
                return BadRequest(ModelState);
            }

            var emptyKey = new Key
            {
                Name = key.Name,
                CompatibleKeys = await _context.M2Keys.Where(e => key.CompatibleKeyIDs.Contains(e.ID)).ToListAsync(),
            };

            _context.M2Keys.Add(emptyKey);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKey), new { id = emptyKey.ID }, emptyKey);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteKey(int id)
        {
            var keyToDelete = await _context.M2Keys.FirstOrDefaultAsync(m => id == m.ID);

            if (keyToDelete is null)
            {
                return NotFound();
            };

            _context.M2Keys.Remove(keyToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KeyExists(int id)
        {
            return _context.M2Keys.Any(e => id == e.ID);
        }

        private async Task<bool> KeyIsValid(KeyDbo key)
        {

            if (String.IsNullOrWhiteSpace(key.Name))
            {
                return false;
            }

            if (key.CompatibleKeyIDs is not null)
            {
                foreach (var keyID in key.CompatibleKeyIDs)
                {
                    if (!await _context.M2Keys.AnyAsync(e => keyID == e.ID))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
