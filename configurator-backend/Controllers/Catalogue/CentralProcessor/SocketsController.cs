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
    public class SocketsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public SocketsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<SocketListItem>>> GetSockets(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<SocketListItem>.CreateAsync(
                _context.CentralProcessorSockets
                .AsNoTracking()
                .Select(socket => new SocketListItem(socket)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<SocketDto>> GetSocket(int id)
        {
            var socket = await _context.CentralProcessorSockets
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (socket is null)
            {
                return NotFound();
            }

            return new SocketDto(socket);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutSocket(int id, SocketDbo socket)
        {
            var socketToUpdate = await _context.CentralProcessorSockets.FirstOrDefaultAsync(m => id == m.ID);

            if (socketToUpdate is null)
            {
                return NotFound();
            }

            if (!SocketIsValid(socket))
            {
                return BadRequest();
            }

            socketToUpdate.Name = socket.Name;
            _context.Entry(socketToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocketExists(id))
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
        public async Task<ActionResult<Socket>> PostSocket(SocketDbo socket)
        {

            if (!SocketIsValid(socket))
            {
                return BadRequest();
            }

            var emptySocket = new Socket
            {
                Name = socket.Name,
            };

            _context.CentralProcessorSockets.Add(emptySocket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSocket), new { id = emptySocket.ID }, emptySocket);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteSocket(int id)
        {
            var socketToDelete = await _context.CentralProcessorSockets.FirstOrDefaultAsync(m => id == m.ID);

            if (socketToDelete is null)
            {
                return NotFound();
            };

            _context.CentralProcessorSockets.Remove(socketToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SocketExists(int id)
        {
            return _context.CentralProcessorSockets.Any(e => id == e.ID);
        }

        private bool SocketIsValid(SocketDbo socket)
        {
            if (String.IsNullOrWhiteSpace(socket.Name))
            {
                return false;
            }

            return true;
        }
    }
}
