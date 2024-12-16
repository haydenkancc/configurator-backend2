using Configurator.Data;
using configurator_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using configurator_backend.Models.Catalogue.Pcie;
using PcieModels = configurator_backend.Models.Catalogue.Pcie;

namespace configurator_backend.Controllers.Catalogue.Pcie
{
    [Route("api/Pcie/[controller]")]
    [ApiController]
    public class VersionsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public VersionsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<VersionListItem>>> GetVersions(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<VersionListItem>.CreateAsync(
                _context.PcieVersions
                .AsNoTracking()
                .Select(version => new VersionListItem(version)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<VersionDto>> GetVersion(int id)
        {
            var version = await _context.PcieVersions
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (version is null)
            {
                return NotFound();
            }

            return new VersionDto(version);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutVersion(int id, VersionDbo version)
        {
            var versionToUpdate = await _context.PcieVersions.FirstOrDefaultAsync(m => id == m.ID);

            if (versionToUpdate is null)
            {
                return NotFound();
            }

            if (!VersionIsValid(version))
            {
                return BadRequest();
            }

            versionToUpdate.Name = version.Name;
            _context.Entry(versionToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersionExists(id))
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
        public async Task<ActionResult<PcieModels.Version>> PostVersion(VersionDbo version)
        {

            if (!VersionIsValid(version))
            {
                return BadRequest();
            }

            var emptyVersion = new PcieModels.Version
            {
                Name = version.Name,
            };

            _context.PcieVersions.Add(emptyVersion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVersion), new { id = emptyVersion.ID }, emptyVersion);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteVersion(int id)
        {
            var versionToDelete = await _context.PcieVersions.FirstOrDefaultAsync(m => id == m.ID);

            if (versionToDelete is null)
            {
                return NotFound();
            };

            _context.PcieVersions.Remove(versionToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VersionExists(int id)
        {
            return _context.PcieVersions.Any(e => id == e.ID);
        }

        private bool VersionIsValid(VersionDbo version)
        {
            if (String.IsNullOrWhiteSpace(version.Name))
            {
                return false;
            }

            return true;
        }
    }
}
