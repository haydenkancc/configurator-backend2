using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.CentralProcessor;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.CentralProcessor
{
    [Route("api/CentralProcessor/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public SeriesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<SeriesListItem>>> GetSeries(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<SeriesListItem>.CreateAsync(
                _context.CentralProcessorSeries
                .AsNoTracking()
                .Select(series => new SeriesListItem(series)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<SeriesDto>> GetSeries(int id)
        {
            var series = await _context.CentralProcessorSeries
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (series is null)
            {
                return NotFound();
            }

            return new SeriesDto(series);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutSeries(int id, SeriesDbo series)
        {
            var seriesToUpdate = await _context.CentralProcessorSeries.FirstOrDefaultAsync(m => id == m.ID);

            if (seriesToUpdate is null)
            {
                return NotFound();
            }

            if (!SeriesIsValid(series))
            {
                return BadRequest();
            }

            seriesToUpdate.Name = series.Name;
            _context.Entry(seriesToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
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
        public async Task<ActionResult<Series>> PostSeries(SeriesDbo series)
        {

            if (!SeriesIsValid(series))
            {
                return BadRequest();
            }

            var emptySeries = new Series
            {
                Name = series.Name,
            };

            _context.CentralProcessorSeries.Add(emptySeries);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeries), new { id = emptySeries.ID }, emptySeries);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var seriesToDelete = await _context.CentralProcessorSeries.FirstOrDefaultAsync(m => id == m.ID);

            if (seriesToDelete is null)
            {
                return NotFound();
            };

            _context.CentralProcessorSeries.Remove(seriesToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeriesExists(int id)
        {
            return _context.CentralProcessorSeries.Any(e => id == e.ID);
        }

        private bool SeriesIsValid(SeriesDbo series)
        {
            if (String.IsNullOrWhiteSpace(series.Name))
            {
                return false;
            }

            return true;
        }
    }
}
