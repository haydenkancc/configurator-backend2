using Configurator.Data;
using configurator_backend.Models;
using configurator_backend.Models.Catalogue.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace configurator_backend.Controllers.Catalogue.General
{
    [Route("api/General/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ManufacturersController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ManufacturerListItem>>> GetManufacturers(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<ManufacturerListItem>.CreateAsync(
                _context.Manufacturers
                .AsNoTracking()
                .Select(manufacturer => new ManufacturerListItem(manufacturer)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ManufacturerDto>> GetManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturers
                .AsNoTracking()
                .Where(e => e.ID == id)
                .FirstOrDefaultAsync();

            if (manufacturer is null)
            {
                return NotFound();
            }

            return new ManufacturerDto(manufacturer);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutManufacturer(int id, ManufacturerDbo manufacturer)
        {
            var manufacturerToUpdate = await _context.Manufacturers.FirstOrDefaultAsync(m => id == m.ID);

            if (manufacturerToUpdate is null)
            {
                return NotFound();
            }

            if (!ManufacturerIsValid(manufacturer))
            {
                return BadRequest();
            }

            manufacturerToUpdate.Name = manufacturer.Name;
            _context.Entry(manufacturerToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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
        public async Task<ActionResult<Manufacturer>> PostManufacturer(ManufacturerDbo manufacturer)
        {

            if (!ManufacturerIsValid(manufacturer))
            {
                return BadRequest();
            }

            var emptyManufacturer = new Manufacturer
            {
                Name = manufacturer.Name,
            };

            _context.Manufacturers.Add(emptyManufacturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetManufacturer), new { id = emptyManufacturer.ID }, emptyManufacturer);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            var manufacturerToDelete = await _context.Manufacturers.FirstOrDefaultAsync(m => id == m.ID);

            if (manufacturerToDelete is null)
            {
                return NotFound();
            };

            _context.Manufacturers.Remove(manufacturerToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManufacturerExists(int id)
        {
            return _context.Manufacturers.Any(e => e.ID == id);
        }

        private bool ManufacturerIsValid(ManufacturerDbo manufacturer)
        {
            if (String.IsNullOrWhiteSpace(manufacturer.Name))
            {
                return false;
            }

            return true;
        }
    }
}
