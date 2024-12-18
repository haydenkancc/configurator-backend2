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
    public class ChannelsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ChannelsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ChannelListItem>>> GetChannels(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<ChannelListItem>.CreateAsync(
                _context.CentralProcessorChannels
                .AsNoTracking()
                .Select(channel => new ChannelListItem(channel)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<ChannelDto>> GetChannel(int id)
        {
            var channel = await _context.CentralProcessorChannels
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (channel is null)
            {
                return NotFound();
            }

            return new ChannelDto(channel);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutChannel(int id, ChannelDbo channel)
        {
            var channelToUpdate = await _context.CentralProcessorChannels.FirstOrDefaultAsync(m => id == m.ID);

            if (channelToUpdate is null)
            {
                return NotFound();
            }

            if (!ChannelIsValid(channel))
            {
                return BadRequest();
            }

            channelToUpdate.Name = channel.Name;
            _context.Entry(channelToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(id))
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
        public async Task<ActionResult<Channel>> PostChannel(ChannelDbo channel)
        {

            if (!ChannelIsValid(channel))
            {
                return BadRequest();
            }

            var emptyChannel = new Channel
            {
                Name = channel.Name,
            };

            _context.CentralProcessorChannels.Add(emptyChannel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChannel), new { id = emptyChannel.ID }, emptyChannel);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            var channelToDelete = await _context.CentralProcessorChannels.FirstOrDefaultAsync(m => id == m.ID);

            if (channelToDelete is null)
            {
                return NotFound();
            };

            _context.CentralProcessorChannels.Remove(channelToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChannelExists(int id)
        {
            return _context.CentralProcessorChannels.Any(e => id == e.ID);
        }

        private bool ChannelIsValid(ChannelDbo channel)
        {
            if (String.IsNullOrWhiteSpace(channel.Name))
            {
                return false;
            }

            return true;
        }
    }
}
