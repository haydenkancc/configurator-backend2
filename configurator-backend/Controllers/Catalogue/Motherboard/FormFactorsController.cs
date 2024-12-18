﻿using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.Motherboard;
using ConfiguratorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Motherboard
{
    [Route("api/Motherboard/[controller]")]
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
                _context.MotherboardFormFactors
                .AsNoTracking()
                .Select(formFactor => new FormFactorListItem(formFactor)),
                pageIndex,
                pageSize
            );
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<FormFactorDto>> GetFormFactor(int id)
        {
            var formFactor = await _context.MotherboardFormFactors
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
            var formFactorToUpdate = await _context.MotherboardFormFactors.FirstOrDefaultAsync(m => id == m.ID);

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

            _context.MotherboardFormFactors.Add(emptyFormFactor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFormFactor), new { id = emptyFormFactor.ID }, emptyFormFactor);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteFormFactor(int id)
        {
            var formFactorToDelete = await _context.MotherboardFormFactors.FirstOrDefaultAsync(m => id == m.ID);

            if (formFactorToDelete is null)
            {
                return NotFound();
            };

            _context.MotherboardFormFactors.Remove(formFactorToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormFactorExists(int id)
        {
            return _context.MotherboardFormFactors.Any(e => id == e.ID);
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