using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Case;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Case
{
    [Route("api/Case/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public MaterialsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<MaterialListItem>>> GetMaterials(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<MaterialListItem>.CreateAsync(
                _context.CaseMaterials
                .AsNoTracking()
                .Select(material => new MaterialListItem(material)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<MaterialDto>> GetMaterial(int id)
        {
            var material = await _context.CaseMaterials
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (material is null)
            {
                return NotFound();
            }

            return new MaterialDto(material);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutMaterial(int id, MaterialDbo material)
        {
            var materialToUpdate = await _context.CaseMaterials.FirstOrDefaultAsync(m => id == m.ID);

            if (materialToUpdate is null)
            {
                return NotFound();
            }

            if (!MaterialIsValid(material))
            {
                return BadRequest();
            }

            materialToUpdate.Name = material.Name;
            _context.Entry(materialToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(id))
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
        public async Task<ActionResult<Material>> PostMaterial(MaterialDbo material)
        {

            if (!MaterialIsValid(material))
            {
                return BadRequest();
            }

            var emptyMaterial = new Material
            {
                Name = material.Name,
            };

            _context.CaseMaterials.Add(emptyMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMaterial), new { id = emptyMaterial.ID }, emptyMaterial);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var materialToDelete = await _context.CaseMaterials.FirstOrDefaultAsync(m => id == m.ID);

            if (materialToDelete is null)
            {
                return NotFound();
            };

            _context.CaseMaterials.Remove(materialToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialExists(int id)
        {
            return _context.CaseMaterials.Any(e => id == e.ID);
        }

        private bool MaterialIsValid(MaterialDbo material)
        {
            if (String.IsNullOrWhiteSpace(material.Name))
            {
                return false;
            }

            return true;
        }
    }
}
