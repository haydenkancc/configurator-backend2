using Configurator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConfiguratorBackend.Controllers
{
    public class ConfigurationsController
    {
        private readonly CatalogueContext _context;

        public ConfigurationsController(CatalogueContext context)
        {
            _context = context;
        }

        public bool CentralProcessorIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.CentralProcessor.Unit unit)
        {
            if(configuration.Motherboard is not null)
            {
                if (unit.SocketID != configuration.Motherboard.Chipset.SocketID)
                {
                    return false;
                }
            }

            if (!configuration.Memory.IsNullOrEmpty())
            {
                var memoryCapacity = 0;

                foreach (var memory in configuration.Memory)
                {
                    memoryCapacity += memory.Capacity;

                    if (memory.IsECC && !unit.SupportECCMemory)
                    {
                        return false;
                    }

                    if (memory.IsBuffered && !unit.SupportBufferedMemory)
                    {
                        return false;
                    }
                }

                if (memoryCapacity > unit.MaxTotalMemoryCapacity)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CaseIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Case.Unit unit)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.PowerSupply is not null)
            {

            }

            if (configuration.GraphicsCards is not null)
            {

            }

            if (configuration.Storage is not null)
            {

            }

            if (configuration.Cooler is not null)
            {

            }

            if (!configuration.Fans.IsNullOrEmpty())
            {

            }

            return true;
        }

        public bool CoolerIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Cooler.Unit unit)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.Case is not null)
            {

            }
        }

        public bool FanIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Fan.Pack pack)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.Case is not null)
            {

            }
        }

        public bool GraphicsCardIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.GraphicsCard.Unit unit)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.Case is not null)
            {

            }
        }

        public bool MemoryIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Memory.Kit kit)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.CentralProcessor is not null)
            {

            }

            if (configuration.Cooler is not null)
            {

            }
        }

        public bool MotherboardIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Motherboard.Unit unit)
        {
            if (configuration.CentralProcessor is not null)
            {

            }

            if (configuration.Case is not null)
            {

            }

            if (!configuration.Storage.IsNullOrEmpty())
            {

            }

            if (!configuration.GraphicsCards.IsNullOrEmpty())
            {

            }

            if (configuration.PowerSupply is not null)
            {

            }

            if (!configuration.Fans.IsNullOrEmpty())
            {

            }

            if (configuration.Cooler is not null)
            {

            }

            if (!configuration.Memory.IsNullOrEmpty())
            {

            }
        }

        public bool PowerSupplyIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.PowerSupply.Unit unit)
        {
            if (configuration.Case is not null)
            {

            }

            if (configuration.Motherboard is not null)
            {

            }

            if (!configuration.GraphicsCards.IsNullOrEmpty())
            {

            }

            if (!configuration.Storage.IsNullOrEmpty())
            {

            }
        }

        public bool StorageIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Storage.Unit unit)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.PowerSupply is not null)
            {

            }
        }

        private ICollection<Models.Catalogue.IO.Connector>? GetIOConnectors(Models.Catalogue.Configuration configuration)
        {
            return null;
        }

        private ICollection<Models.Catalogue.PowerSupply.UnitConnector>? GetPowerSupplyConnectors(Models.Catalogue.Configuration configuration)
        {
            if (configuration.PowerSupply is null)
            {
                return null;
            }

            var connectors = new List<Models.Catalogue.PowerSupply.UnitConnector>(configuration.PowerSupply.Connectors);



            return null;
        }
    }
}
