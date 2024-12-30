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

        /* done */
        public bool CentralProcessorIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.CentralProcessor.Unit unit)
        {
            if(configuration.Motherboard is not null)
            {
                if (unit.SocketID != configuration.Motherboard.Unit.Chipset.SocketID)
                {
                    return false;
                }
            }

            if (!configuration.Memory.IsNullOrEmpty())
            {
                var memoryCapacity = 0;

                foreach (var memory in configuration.Memory)
                {
                    memoryCapacity += memory.Kit.Capacity;

                    if (memory.Kit.IsECC && !unit.SupportECCMemory)
                    {
                        return false;
                    }

                    if (memory.Kit.IsBuffered && !unit.SupportBufferedMemory)
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
                if (!unit.MotherboardFormFactors.Any(formFactor => configuration.Motherboard.Unit.FormFactorID == formFactor.ID))
                {
                    return false;
                }

                /* For each required connector, check if # of available connector is greater than # of required connector*/
                foreach (var connector in unit.IOConnectors)
                {
                    var requiredConnectorCount = connector.ConnectorCount;

                    var availableConnectorCount = configuration.Motherboard.IOConnectors.GetValueOrDefault(connector.Connector.ID);

                    if (availableConnectorCount is not null)
                    {
                        requiredConnectorCount -= availableConnectorCount.Sum();

                        if (requiredConnectorCount <= 0) 
                        { 
                            continue; 
                        }
                    }

                    foreach (var physicalConnector in connector.Connector.PhysicalConnectors)
                    {
                        var physicalConnectorCount = configuration.Motherboard.IOConnectors.GetValueOrDefault(physicalConnector.ID);

                        if (physicalConnectorCount is not null)
                        {
                            requiredConnectorCount -= physicalConnectorCount.Sum();

                            if (requiredConnectorCount <= 0)
                            {
                                break;
                            }
                        }
                    }

                    if (requiredConnectorCount > 0)
                    {
                        return false;
                    }
                }
            }

            if (configuration.PowerSupply is not null)
            {
                foreach (var connector in unit.PowerSupplyConnectors)
                {
                    var requiredConnectorCount = connector.ConnectorCount;

                    var availableConnectorCount = configuration.PowerSupply.Connectors.GetValueOrDefault(connector.Connector.ID);

                    if (availableConnectorCount is not null)
                    {
                        requiredConnectorCount -= availableConnectorCount.Sum();

                        if (requiredConnectorCount <= 0)
                        {
                            continue;
                        }
                    }

                    foreach (var physicalConnector in connector.Connector.PhysicalConnectors)
                    {
                        var physicalConnectorCount = configuration.PowerSupply.Connectors.GetValueOrDefault(physicalConnector.ID);

                        if (physicalConnectorCount is not null)
                        {
                            requiredConnectorCount -= physicalConnectorCount.Sum();

                            if (requiredConnectorCount <= 0)
                            {
                                break;
                            }
                        }
                    }

                    if (requiredConnectorCount > 0)
                    {
                        return false;
                    }
                }

                if (configuration.GraphicsCards is not null)
                {

                }

                if (configuration.Storage is not null)
                {

                }

                if (configuration.Cooler is not null)
                {
                    if (configuration.Cooler.Unit is Models.Catalogue.Cooler.AirUnit)
                    {

                    }
                }

                if (!configuration.Fans.IsNullOrEmpty())
                {

                }

            }
            return true;
        }

        public bool CoolerIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Cooler.Unit unit)
        {
            if (configuration.Motherboard is not null)
            {
                if (!unit.Sockets.Any(socket => configuration.Motherboard.Unit.Chipset.SocketID == socket.ID))
                {
                    return false;
                }

                foreach (var connector in unit.Connectors)
                {
                    var requiredConnectorCount = connector.ConnectorCount;

                    var availableConnectorCount = configuration.Motherboard.IOConnectors.GetValueOrDefault(connector.Connector.ID);

                    if (availableConnectorCount is not null)
                    {
                        requiredConnectorCount -= availableConnectorCount.Sum();

                        if (requiredConnectorCount <= 0)
                        {
                            continue;
                        }
                    }

                    foreach (var physicalConnector in connector.Connector.PhysicalConnectors)
                    {
                        var physicalConnectorCount = configuration.Motherboard.IOConnectors.GetValueOrDefault(physicalConnector.ID);

                        if (physicalConnectorCount is not null)
                        {
                            requiredConnectorCount -= physicalConnectorCount.Sum();

                            if (requiredConnectorCount <= 0)
                            {
                                break;
                            }
                        }
                    }

                    if (requiredConnectorCount > 0)
                    {
                        return false;
                    }
                }

            }

            if (configuration.Memory is not null && unit is Models.Catalogue.Cooler.AirUnit)
            {
                foreach (var memory in configuration.Memory)
                {
                    if (((Models.Catalogue.Cooler.AirUnit)unit).LimitsMemoryHeight && ((Models.Catalogue.Cooler.AirUnit)unit).MaximumMemoryHeight < memory.Kit.Height)
                    {
                        return false;
                    }
                }
            }

            if (configuration.Case is not null)
            {
                var compatibleLayoutExists = false;

                foreach (var layout in configuration.Case.Layouts)
                {
                    if (unit is Models.Catalogue.Cooler.AirUnit)
                    {
                        if (layout.Layout.MaxAirCoolerHeight < ((Models.Catalogue.Cooler.AirUnit)unit).Height)
                        {
                            continue;
                        }

                    }
                    if (unit is Models.Catalogue.Cooler.LiquidUnit)
                    {
                        var compatiblePanelRadiatorExists = false; 

                        foreach (var panel in layout.LayoutPanels)
                        {
                            foreach (var panelRadiator in panel.Radiators)
                            {
                                if (panelRadiator.IsAvailable && panelRadiator.PanelRadiator.RadiatorSizeID == ((Models.Catalogue.Cooler.LiquidUnit)unit).RadiatorSizeID)
                                {
                                    compatiblePanelRadiatorExists = true;
                                    break;
                                }
                            }

                            if (compatiblePanelRadiatorExists)
                            {
                                break;
                            }
                        }

                        if (!compatiblePanelRadiatorExists)
                        {
                            continue;
                        }
                    }

                    compatibleLayoutExists = true;
                }

                if (!compatibleLayoutExists)
                {
                    return false;
                }
            }
            return true;
        }

        public bool FanIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Fan.Pack pack)
        {
            if (configuration.Motherboard is not null)
            {
                foreach (var connector in pack.Connectors)
                {
                    var requiredConnectorCount = connector.ConnectorCount;

                    var availableConnectorCount = configuration.Motherboard.IOConnectors.GetValueOrDefault(connector.Connector.ID);

                    if (availableConnectorCount is not null)
                    {
                        requiredConnectorCount -= availableConnectorCount.Sum();

                        if (requiredConnectorCount <= 0)
                        {
                            continue;
                        }
                    }

                    foreach (var physicalConnector in connector.Connector.PhysicalConnectors)
                    {
                        var physicalConnectorCount = configuration.Motherboard.IOConnectors.GetValueOrDefault(physicalConnector.ID);

                        if (physicalConnectorCount is not null)
                        {
                            requiredConnectorCount -= physicalConnectorCount.Sum();

                            if (requiredConnectorCount <= 0)
                            {
                                break;
                            }
                        }
                    }

                    if (requiredConnectorCount > 0)
                    {
                        return false;
                    }
                }
            }

            if (configuration.Case is not null)
            {

            }
            return true;

        }

        public bool GraphicsCardIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.GraphicsCard.Unit unit)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.Case is not null)
            {

            }

            return true;
        }

        public bool MemoryIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Memory.Kit kit)
        {
            if (configuration.Motherboard is not null)
            {
                if (kit.ModuleCount > configuration.Motherboard.AvailableMemorySlotCount)
                {
                    return false;
                }

                if (kit.ModuleCount * kit.Capacity > configuration.Motherboard.AvailableMemoryCapacity)
                {
                    return false;
                }

                if (kit.TypeID != configuration.Motherboard.Unit.MemoryTypeID)
                {
                    return false;
                }

                if (kit.FormFactorID != configuration.Motherboard.Unit.MemoryFormFactorID)
                {
                    return false;
                }

                if (kit.IsECC && !configuration.Motherboard.Unit.SupportECCMemory || !kit.IsECC && configuration.Motherboard.Unit.SupportNonECCMemory)
                {
                    return false;
                }

                if (kit.IsBuffered && !configuration.Motherboard.Unit.SupportBufferedMemory || !kit.IsBuffered && configuration.Motherboard.Unit.SupportUnbufferedMemory)
                {
                    return false;
                }
            }

            if (configuration.CentralProcessor is not null)
            {
                if (kit.ModuleCount * kit.Capacity > configuration.CentralProcessor.AvailableMemoryCapacity)
                {
                    return false;
                }

                if (kit.IsECC && !configuration.CentralProcessor.Unit.SupportECCMemory || !kit.IsECC && configuration.CentralProcessor.Unit.SupportNonECCMemory)
                {
                    return false;
                }

                if (kit.IsBuffered && !configuration.CentralProcessor.Unit.SupportBufferedMemory || !kit.IsBuffered && configuration.CentralProcessor.Unit.SupportUnbufferedMemory)
                {
                    return false;
                }
            }

            if (configuration.Cooler is not null && configuration.Cooler.Unit is Models.Catalogue.Cooler.AirUnit)
            {
                var unit = (Models.Catalogue.Cooler.AirUnit)configuration.Cooler.Unit;
                
                if (unit.LimitsMemoryHeight && kit.Height > unit.MaximumMemoryHeight)
                {
                    return false;
                }
            }

            return true;
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

            return true;
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

            return true;
        }

        public bool StorageIsCompatible(Models.Catalogue.Configuration configuration, Models.Catalogue.Storage.Unit unit)
        {
            if (configuration.Motherboard is not null)
            {

            }

            if (configuration.PowerSupply is not null)
            {

            }

            if (configuration.Case is not null)
            {

            }

            return true;
        }
    }
}
