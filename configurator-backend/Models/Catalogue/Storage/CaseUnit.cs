using ConfiguratorBackend.Models.Catalogue.General;
using System.ComponentModel.DataAnnotations;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public class CaseUnitDto : BaseUnitDto 
    {
        public FormFactorDto FormFactor { get; set; }
        public IO.ConnectorDtoSimple IOConnector { get; set; }
        public PowerSupply.ConnectorDtoSimple PowerSupplyConnector { get; set; }

        public CaseUnitDto(ComponentDto component, DriveDto drive, CaseUnit unit) : base(component, drive, unit)
        {
            FormFactor = new FormFactorDto(unit.FormFactor);
            IOConnector = new IO.ConnectorDtoSimple(unit.IOConnector);
            PowerSupplyConnector = new PowerSupply.ConnectorDtoSimple(unit.PowerSupplyConnector);
        }
    }

    public class CaseUnitDbo : BaseUnitDbo
    {
        [Required]
        public required int FormFactorID { get; set; }
        [Required]
        public required int IOConnectorID { get; set; }
        [Required]
        public required int PowerSupplyConnectorID { get; set; }
    }

    public class CaseUnit : Unit
    {
        public required int FormFactorID { get; set; }
        public required int IOConnectorID { get; set; }
        public required int PowerSupplyConnectorID { get; set; }

        public FormFactor FormFactor { get; set; } = null!;
        public IO.Connector IOConnector { get; set; } = null!;
        public PowerSupply.Connector PowerSupplyConnector { get; set; } = null!;
    }
}
