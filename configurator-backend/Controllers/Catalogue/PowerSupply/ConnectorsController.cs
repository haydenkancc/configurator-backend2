using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.PowerSupply;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.PowerSupply
{
    [Route("api/PowerSupply/[controller]")]
    [ApiController]
    public class ConnectorsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ConnectorsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ConnectorListItem>>> GetConnectors(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<ConnectorListItem>.CreateAsync(
                _context.PowerSupplyConnectors
                .AsNoTracking()
                .Select(connector => new ConnectorListItem(connector)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ConnectorDto>> GetConnector(int id)
        {
            var connector = await _context.PowerSupplyConnectors
                .AsNoTracking()
                .Where(e => id == e.ID)
                .Include(connector => connector.CompatibleConnectors)
                .FirstOrDefaultAsync();

            if (connector is null)
            {
                return NotFound();
            }

            return new ConnectorDto(connector);
        }

        [HttpGet("params")]
        public async Task<ActionResult<ConnectorParams>> GetConnectorParams()
        {
            return new ConnectorParams
            {
                Connectors = await _context.PowerSupplyConnectors.AsNoTracking().Select(e => new ConnectorDtoSimple(e)).ToListAsync(),
            };
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutConnector(int id, ConnectorDbo connector)
        {
            var connectorToUpdate = await _context.PowerSupplyConnectors.FirstOrDefaultAsync(m => id == m.ID);

            if (connectorToUpdate is null)
            {
                return NotFound();
            }

            if (!await ConnectorIsValid(connector))
            {
                return BadRequest(ModelState);
            }

            connectorToUpdate.Name = connector.Name;

            _context.PowerSupplyConnectors.Entry(connectorToUpdate).Collection(e => e.CompatibleConnectors).Load();
            connectorToUpdate.CompatibleConnectors = await _context.PowerSupplyConnectors.Where(e => connector.CompatibleConnectorIDs.Contains(e.ID)).ToListAsync();

            _context.Entry(connectorToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConnectorExists(id))
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
        public async Task<ActionResult<Connector>> PostConnector(ConnectorDbo connector)
        {

            if (!await ConnectorIsValid(connector))
            {
                return BadRequest(ModelState);
            }

            var emptyConnector = new Connector
            {
                Name = connector.Name,
                CompatibleConnectors = await _context.PowerSupplyConnectors.Where(e => connector.CompatibleConnectorIDs.Contains(e.ID)).ToListAsync(),
            };

            _context.PowerSupplyConnectors.Add(emptyConnector);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConnector), new { id = emptyConnector.ID }, emptyConnector);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteConnector(int id)
        {
            var connectorToDelete = await _context.PowerSupplyConnectors.FirstOrDefaultAsync(m => id == m.ID);

            if (connectorToDelete is null)
            {
                return NotFound();
            };

            _context.PowerSupplyConnectors.Remove(connectorToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConnectorExists(int id)
        {
            return _context.PowerSupplyConnectors.Any(e => id == e.ID);
        }

        private async Task<bool> ConnectorIsValid(ConnectorDbo connector)
        {

            if (String.IsNullOrWhiteSpace(connector.Name))
            {
                return false;
            }

            if (connector.CompatibleConnectorIDs is not null)
            {
                foreach (var connectorID in connector.CompatibleConnectorIDs)
                {
                    if (!await _context.PowerSupplyConnectors.AnyAsync(e => connectorID == e.ID))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
