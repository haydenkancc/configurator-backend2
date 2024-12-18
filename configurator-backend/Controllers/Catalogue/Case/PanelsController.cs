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
    public class PanelsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public PanelsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<PanelListItem>>> GetPanels(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<PanelListItem>.CreateAsync(
                _context.CasePanels
                .AsNoTracking()
                .Select(panel => new PanelListItem(panel)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<PanelDto>> GetPanel(int id)
        {
            var panel = await _context.CasePanels
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (panel is null)
            {
                return NotFound();
            }

            return new PanelDto(panel);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutPanel(int id, PanelDbo panel)
        {
            var panelToUpdate = await _context.CasePanels.FirstOrDefaultAsync(m => id == m.ID);

            if (panelToUpdate is null)
            {
                return NotFound();
            }

            if (!PanelIsValid(panel))
            {
                return BadRequest();
            }

            panelToUpdate.Name = panel.Name;
            _context.Entry(panelToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PanelExists(id))
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
        public async Task<ActionResult<Panel>> PostPanel(PanelDbo panel)
        {

            if (!PanelIsValid(panel))
            {
                return BadRequest();
            }

            var emptyPanel = new Panel
            {
                Name = panel.Name,
            };

            _context.CasePanels.Add(emptyPanel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPanel), new { id = emptyPanel.ID }, emptyPanel);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeletePanel(int id)
        {
            var panelToDelete = await _context.CasePanels.FirstOrDefaultAsync(m => id == m.ID);

            if (panelToDelete is null)
            {
                return NotFound();
            };

            _context.CasePanels.Remove(panelToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PanelExists(int id)
        {
            return _context.CasePanels.Any(e => id == e.ID);
        }

        private bool PanelIsValid(PanelDbo panel)
        {
            if (String.IsNullOrWhiteSpace(panel.Name))
            {
                return false;
            }

            return true;
        }
    }
}
