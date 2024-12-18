using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.CentralProcessor;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConfiguratorBackend.Controllers.Catalogue.General;

namespace ConfiguratorBackend.Controllers.Catalogue.CentralProcessor
{
    [Route("api/CentralProcessor/[controller]")]
    [ApiController]
    public class CoreFamiliesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public CoreFamiliesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<CoreFamilyListItem>>> GetCoreFamilies(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<CoreFamilyListItem>.CreateAsync(
                _context.CentralProcessorCoreFamilies
                .AsNoTracking()
                .Select(coreFamily => new CoreFamilyListItem(coreFamily)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<CoreFamilyDto>> GetCoreFamily(int id)
        {
            var coreFamily = await _context.CentralProcessorCoreFamilies
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (coreFamily is null)
            {
                return NotFound();
            }

            return new CoreFamilyDto(coreFamily);
        }

        [HttpGet("params")]
        public async Task<ActionResult<CoreFamilyParams>> GetCoreFamilyParams()
        {
            var coreFamilyParams = new CoreFamilyParams
            {
                Microarchitectures = await _context.CentralProcessorMicroarchitectures.AsNoTracking().Select(e => new MicroarchitectureDto(e)).ToListAsync(),
            };

            return coreFamilyParams;
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutCoreFamily(int id, CoreFamilyDbo coreFamily)
        {
            var coreFamilyToUpdate = await _context.CentralProcessorCoreFamilies.FirstOrDefaultAsync(m => id == m.ID);

            if (coreFamilyToUpdate is null)
            {
                return NotFound();
            }

            if (!await CoreFamilyIsValid(coreFamily))
            {
                return BadRequest();
            }

            coreFamilyToUpdate.MicroarchitectureID = coreFamily.MicroarchitectureID;
            coreFamilyToUpdate.CodeName = coreFamily.CodeName;
            coreFamilyToUpdate.AlternateName = coreFamily.AlternateName;


            _context.Entry(coreFamilyToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreFamilyExists(id))
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
        public async Task<ActionResult<CoreFamily>> PostCoreFamily(CoreFamilyDbo coreFamily)
        {

            if (!await CoreFamilyIsValid(coreFamily))
            {
                return BadRequest();
            }

            var emptyCoreFamily = new CoreFamily
            {
                CodeName = coreFamily.CodeName,
                AlternateName = coreFamily.AlternateName,
                MicroarchitectureID = coreFamily.MicroarchitectureID,
            };

            _context.CentralProcessorCoreFamilies.Add(emptyCoreFamily);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCoreFamily), new { id = emptyCoreFamily.ID }, emptyCoreFamily);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteCoreFamily(int id)
        {
            var coreFamilyToDelete = await _context.CentralProcessorCoreFamilies.FirstOrDefaultAsync(m => id == m.ID);

            if (coreFamilyToDelete is null)
            {
                return NotFound();
            };

            _context.CentralProcessorCoreFamilies.Remove(coreFamilyToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoreFamilyExists(int id)
        {
            return _context.CentralProcessorCoreFamilies.Any(e => id == e.ID);
        }

        private async Task<bool> CoreFamilyIsValid(CoreFamilyDbo coreFamily)
        {
            if (String.IsNullOrWhiteSpace(coreFamily.CodeName) || String.IsNullOrWhiteSpace(coreFamily.AlternateName))
            {
                return false;
            }

            if (!await _context.CentralProcessorMicroarchitectures.AnyAsync(e => coreFamily.MicroarchitectureID == e.ID))
            {
                return false;
            }

            return true;
        }
    }
}
