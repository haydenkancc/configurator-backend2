using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Storage;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class ConnectionInterfacesController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ConnectionInterfacesController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ConnectionInterfaceListItem>>> GetConnectionInterfaces(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<ConnectionInterfaceListItem>.CreateAsync(
                _context.StorageConnectionInterfaces
                .AsNoTracking()
                .Select(connectionInterface => new ConnectionInterfaceListItem(connectionInterface)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ConnectionInterfaceDto>> GetConnectionInterface(int id)
        {
            var connectionInterface = await _context.StorageConnectionInterfaces
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (connectionInterface is null)
            {
                return NotFound();
            }

            return new ConnectionInterfaceDto(connectionInterface);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutConnectionInterface(int id, ConnectionInterfaceDbo connectionInterface)
        {
            var connectionInterfaceToUpdate = await _context.StorageConnectionInterfaces.FirstOrDefaultAsync(m => id == m.ID);

            if (connectionInterfaceToUpdate is null)
            {
                return NotFound();
            }

            if (!ConnectionInterfaceIsValid(connectionInterface))
            {
                return BadRequest(ModelState);
            }

            connectionInterfaceToUpdate.Name = connectionInterface.Name;
            _context.Entry(connectionInterfaceToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConnectionInterfaceExists(id))
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
        public async Task<ActionResult<ConnectionInterface>> PostConnectionInterface(ConnectionInterfaceDbo connectionInterface)
        {

            if (!ConnectionInterfaceIsValid(connectionInterface))
            {
                return BadRequest(ModelState);
            }

            var emptyConnectionInterface = new ConnectionInterface
            {
                Name = connectionInterface.Name,
            };

            _context.StorageConnectionInterfaces.Add(emptyConnectionInterface);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConnectionInterface), new { id = emptyConnectionInterface.ID }, emptyConnectionInterface);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteConnectionInterface(int id)
        {
            var connectionInterfaceToDelete = await _context.StorageConnectionInterfaces.FirstOrDefaultAsync(m => id == m.ID);

            if (connectionInterfaceToDelete is null)
            {
                return NotFound();
            };

            _context.StorageConnectionInterfaces.Remove(connectionInterfaceToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConnectionInterfaceExists(int id)
        {
            return _context.StorageConnectionInterfaces.Any(e => id == e.ID);
        }

        private bool ConnectionInterfaceIsValid(ConnectionInterfaceDbo connectionInterface)
        {
            if (String.IsNullOrWhiteSpace(connectionInterface.Name))
            {
                return false;
            }

            return true;
        }
    }
}
