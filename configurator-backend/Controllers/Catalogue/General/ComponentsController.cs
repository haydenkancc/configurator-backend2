using Configurator.Data;
using configurator_backend.Models.Catalogue.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace configurator_backend.Controllers.Catalogue.General
{
    public class ComponentsController : ControllerBase
    {
        private readonly CatalogueContext _context;

        protected ComponentsController(CatalogueContext context)
        {
            _context = context;
        }

        protected async Task<ComponentParams> GetComponentParams()
        {
            return new ComponentParams
            {
                Manufacturers = await _context.Manufacturers
                .AsNoTracking()
                .ToListAsync()
            };
        }

        public async Task<ActionResult<ComponentDto>> GetComponent(int id)
        {
            var component = await _context.Components
                .AsNoTracking()
                .Where(e => id == e.ID)
                .Include(e => e.Manufacturer)
                .FirstOrDefaultAsync();

            if (component is null)
            {
                return NotFound();
            }

            return new ComponentDto(component);
        }

        public async Task<IActionResult> PutComponent(int id, ComponentDbo component)
        {
            var componentToUpdate = await _context.Components.FirstOrDefaultAsync(e => e.ID == id);

            if (componentToUpdate is null)
            {
                return NotFound();
            }

            if (!await ComponentIsValid(component))
            {
                return BadRequest();
            }

            componentToUpdate.SKU = component.SKU;
            componentToUpdate.ManufacturerID = component.ManufacturerID;
            componentToUpdate.Name = component.Name;
            componentToUpdate.PartNumber = component.PartNumber;
            componentToUpdate.RegularPrice = component.RegularPrice;
            componentToUpdate.SalePrice = component.SalePrice;
            componentToUpdate.OnSale = component.OnSale;
            componentToUpdate.Saleable = component.Saleable;

            _context.Entry(componentToUpdate).State = EntityState.Modified;

            return NoContent();
        }

        protected async Task<ActionResult<Component>> PostComponent(ComponentDbo component)
        {

            if(!await ComponentIsValid(component))
            {
                return BadRequest();
            }

            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(e => e.ID == component.ManufacturerID);

            if (manufacturer is null) {
                return BadRequest();
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
                Manufacturer = manufacturer,
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
