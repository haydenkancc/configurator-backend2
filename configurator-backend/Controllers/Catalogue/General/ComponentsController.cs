﻿using Configurator.Data;
using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ConfiguratorBackend.Controllers.Catalogue.General
{
    public class ComponentsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ComponentsController(CatalogueContext context)
        {
            _context = context;
        }

        public async Task<ComponentParams> GetComponentParams()
        {
            return new ComponentParams
            {
                Manufacturers = await _context.Manufacturers.AsNoTracking().Select(e => new ManufacturerDto(e)).ToListAsync(),

                Colours = await _context.Colours.AsNoTracking().Select(e => new ColourDto(e)).ToListAsync(),
            };
        }

        public async Task<ActionResult<ComponentDto>> GetComponent(int id)
        {
            var component = await _context.Components
                .AsNoTracking()
                .Where(e => id == e.ID)
                .Include(e => e.Manufacturer)
                .Include(e => e.Colour)
                .FirstOrDefaultAsync();

            if (component is null)
            {
                return NotFound();
            }

            return new ComponentDto(component);
        }

        public async Task<IActionResult> PutComponent(int id, ComponentDbo component)
        {
            var componentToUpdate = await _context.Components.FirstOrDefaultAsync(e => id == e.ID);

            if (componentToUpdate is null)
            {
                return NotFound();
            }

            if (!await ComponentIsValid(component))
            {
                return BadRequest(ModelState);
            }

            componentToUpdate.SKU = component.SKU;
            componentToUpdate.ManufacturerID = component.ManufacturerID;
            componentToUpdate.Name = component.Name;
            componentToUpdate.PartNumber = component.PartNumber;
            componentToUpdate.RegularPrice = component.RegularPrice;
            componentToUpdate.SalePrice = component.SalePrice;
            componentToUpdate.OnSale = component.OnSale;
            componentToUpdate.Saleable = component.Saleable;
            componentToUpdate.IsColoured = component.IsColoured;
            componentToUpdate.ColourID = component.IsColoured ? component.ColourID : null;

            _context.Entry(componentToUpdate).State = EntityState.Modified;

            return NoContent();
        }

        public async Task<ActionResult<Component>> PostComponent(ComponentDbo component)
        {

            if(!await ComponentIsValid(component))
            {
                return BadRequest(ModelState);
            }

            var emptyComponent = new Component
            {
                SKU = component.SKU,
                Name = component.Name,
                PartNumber = component.PartNumber,
                RegularPrice = component.RegularPrice,
                SalePrice = component.SalePrice,
                OnSale = component.OnSale,
                Saleable = component.Saleable,
                ManufacturerID = component.ManufacturerID,
                IsColoured = component.IsColoured,
                ColourID = component.IsColoured ? component.ColourID : null,
            };

            _context.Components.Add(emptyComponent);

            return emptyComponent;
        }

        public async Task<IActionResult> DeleteComponent(int id)
        {
            var componentToDelete = await _context.Components.FirstOrDefaultAsync(component => id == component.ID);

            if (componentToDelete is null)
            {
                return NotFound();
            }

            _context.Components.Remove(componentToDelete);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        public async Task<bool> ComponentIsValid(ComponentDbo component)
        {
            if (component is null)
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(component.SKU) ||
                String.IsNullOrWhiteSpace(component.Name) ||
                String.IsNullOrWhiteSpace(component.PartNumber) ||
                component.RegularPrice <= 0)
            {
                return false;
            }

            if (!await _context.Manufacturers.AnyAsync(e => component.ManufacturerID == e.ID))
            {
                return false;
            }

            return true;
        }
    }
}
