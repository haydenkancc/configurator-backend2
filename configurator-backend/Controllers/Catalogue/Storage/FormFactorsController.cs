﻿using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Storage;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class FormFactorsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public FormFactorsController(CatalogueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<FormFactorListItem>>> GetFormFactors(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<FormFactorListItem>.CreateAsync(
                _context.StorageFormFactors
                .AsNoTracking()
                .Select(formFactor => new FormFactorListItem(formFactor)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<FormFactorDto>> GetFormFactor(int id)
        {
            var formFactor = await _context.StorageFormFactors
                .AsNoTracking()
                .Where(e => id == e.ID)
                .FirstOrDefaultAsync();

            if (formFactor is null)
            {
                return NotFound();
            }

            return new FormFactorDto(formFactor);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutFormFactor(int id, FormFactorDbo formFactor)
        {
            var formFactorToUpdate = await _context.StorageFormFactors.FirstOrDefaultAsync(m => id == m.ID);

            if (formFactorToUpdate is null)
            {
                return NotFound();
            }

            if (!FormFactorIsValid(formFactor))
            {
                return BadRequest(ModelState);
            }

            formFactorToUpdate.Name = formFactor.Name;
            _context.Entry(formFactorToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormFactorExists(id))
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
        public async Task<ActionResult<FormFactor>> PostFormFactor(FormFactorDbo formFactor)
        {

            if (!FormFactorIsValid(formFactor))
            {
                return BadRequest(ModelState);
            }

            var emptyFormFactor = new FormFactor
            {
                Name = formFactor.Name,
            };

            _context.StorageFormFactors.Add(emptyFormFactor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFormFactor), new { id = emptyFormFactor.ID }, emptyFormFactor);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteFormFactor(int id)
        {
            var formFactorToDelete = await _context.StorageFormFactors.FirstOrDefaultAsync(m => id == m.ID);

            if (formFactorToDelete is null)
            {
                return NotFound();
            };

            _context.StorageFormFactors.Remove(formFactorToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormFactorExists(int id)
        {
            return _context.StorageFormFactors.Any(e => id == e.ID);
        }

        private bool FormFactorIsValid(FormFactorDbo formFactor)
        {
            if (String.IsNullOrWhiteSpace(formFactor.Name))
            {
                return false;
            }

            return true;
        }
    }
}