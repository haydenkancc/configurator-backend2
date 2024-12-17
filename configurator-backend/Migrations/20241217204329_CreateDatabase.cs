using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfiguratorBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseMaterials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseMaterials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CasePanels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasePanels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CaseSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseSizes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CentralProcessorChannels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralProcessorChannels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CentralProcessorMicroarchitectures",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralProcessorMicroarchitectures", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CentralProcessorSockets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralProcessorSockets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CoolerRadiatorSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoolerRadiatorSizes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FanSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SideLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FanSizes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCardChipsets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCardChipsets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IOConnectors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOConnectors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M2FormFactors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2FormFactors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M2Keys",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2Keys", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MemoryFormFactors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryFormFactors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MemoryTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardFormFactors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardFormFactors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PcieBrackets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcieBrackets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PcieSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaneCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcieSizes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PcieVersions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcieVersions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyConnectors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyConnectors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyEfficiencyRatings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyEfficiencyRatings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyFormFactors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyFormFactors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyModularities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyModularities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StorageConnectionInterfaces",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageConnectionInterfaces", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StorageFormFactors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageFormFactors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StorageNandTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageNandTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardChipsets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocketID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardChipsets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MotherboardChipsets_CentralProcessorSockets_SocketID",
                        column: x => x.SocketID,
                        principalTable: "CentralProcessorSockets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConnectorConnector",
                columns: table => new
                {
                    CompatibleConnectorsID = table.Column<int>(type: "int", nullable: false),
                    PhysicalConnectorsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorConnector", x => new { x.CompatibleConnectorsID, x.PhysicalConnectorsID });
                    table.ForeignKey(
                        name: "FK_ConnectorConnector_IOConnectors_CompatibleConnectorsID",
                        column: x => x.CompatibleConnectorsID,
                        principalTable: "IOConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectorConnector_IOConnectors_PhysicalConnectorsID",
                        column: x => x.PhysicalConnectorsID,
                        principalTable: "IOConnectors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "KeyKey",
                columns: table => new
                {
                    CompatibleKeysID = table.Column<int>(type: "int", nullable: false),
                    PhysicalKeysID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyKey", x => new { x.CompatibleKeysID, x.PhysicalKeysID });
                    table.ForeignKey(
                        name: "FK_KeyKey_M2Keys_CompatibleKeysID",
                        column: x => x.CompatibleKeysID,
                        principalTable: "M2Keys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyKey_M2Keys_PhysicalKeysID",
                        column: x => x.PhysicalKeysID,
                        principalTable: "M2Keys",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerID = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegularPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    OnSale = table.Column<bool>(type: "bit", nullable: false),
                    Saleable = table.Column<bool>(type: "bit", nullable: false),
                    IsColoured = table.Column<bool>(type: "bit", nullable: false),
                    ColourID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Components_Colours_ColourID",
                        column: x => x.ColourID,
                        principalTable: "Colours",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Components_Manufacturers_ManufacturerID",
                        column: x => x.ManufacturerID,
                        principalTable: "Manufacturers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M2ExpansionCards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyID = table.Column<int>(type: "int", nullable: false),
                    FormFactorID = table.Column<int>(type: "int", nullable: false),
                    VersionID = table.Column<int>(type: "int", nullable: false),
                    LaneSizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2ExpansionCards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M2ExpansionCards_M2FormFactors_FormFactorID",
                        column: x => x.FormFactorID,
                        principalTable: "M2FormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M2ExpansionCards_M2Keys_KeyID",
                        column: x => x.KeyID,
                        principalTable: "M2Keys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M2ExpansionCards_PcieSizes_LaneSizeID",
                        column: x => x.LaneSizeID,
                        principalTable: "PcieSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M2ExpansionCards_PcieVersions_VersionID",
                        column: x => x.VersionID,
                        principalTable: "PcieVersions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M2Slots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyID = table.Column<int>(type: "int", nullable: false),
                    LaneSizeID = table.Column<int>(type: "int", nullable: false),
                    VersionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2Slots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M2Slots_M2Keys_KeyID",
                        column: x => x.KeyID,
                        principalTable: "M2Keys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M2Slots_PcieSizes_LaneSizeID",
                        column: x => x.LaneSizeID,
                        principalTable: "PcieSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M2Slots_PcieVersions_VersionID",
                        column: x => x.VersionID,
                        principalTable: "PcieVersions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcieExpansionCards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BracketID = table.Column<int>(type: "int", nullable: false),
                    VersionID = table.Column<int>(type: "int", nullable: false),
                    LaneSizeID = table.Column<int>(type: "int", nullable: false),
                    PhysicalSizeID = table.Column<int>(type: "int", nullable: false),
                    ExpansionSlotWidth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcieExpansionCards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PcieExpansionCards_PcieBrackets_BracketID",
                        column: x => x.BracketID,
                        principalTable: "PcieBrackets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcieExpansionCards_PcieSizes_LaneSizeID",
                        column: x => x.LaneSizeID,
                        principalTable: "PcieSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcieExpansionCards_PcieSizes_PhysicalSizeID",
                        column: x => x.PhysicalSizeID,
                        principalTable: "PcieSizes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PcieExpansionCards_PcieVersions_VersionID",
                        column: x => x.VersionID,
                        principalTable: "PcieVersions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcieSlots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaneSizeID = table.Column<int>(type: "int", nullable: false),
                    PhysicalSizeID = table.Column<int>(type: "int", nullable: false),
                    VersionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcieSlots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PcieSlots_PcieSizes_LaneSizeID",
                        column: x => x.LaneSizeID,
                        principalTable: "PcieSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcieSlots_PcieSizes_PhysicalSizeID",
                        column: x => x.PhysicalSizeID,
                        principalTable: "PcieSizes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PcieSlots_PcieVersions_VersionID",
                        column: x => x.VersionID,
                        principalTable: "PcieVersions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConnectorConnector1",
                columns: table => new
                {
                    CompatibleConnectorsID = table.Column<int>(type: "int", nullable: false),
                    PhysicalConnectorsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorConnector1", x => new { x.CompatibleConnectorsID, x.PhysicalConnectorsID });
                    table.ForeignKey(
                        name: "FK_ConnectorConnector1_PowerSupplyConnectors_CompatibleConnectorsID",
                        column: x => x.CompatibleConnectorsID,
                        principalTable: "PowerSupplyConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectorConnector1_PowerSupplyConnectors_PhysicalConnectorsID",
                        column: x => x.PhysicalConnectorsID,
                        principalTable: "PowerSupplyConnectors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CaseUnits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    PowerSupplyFormFactorID = table.Column<int>(type: "int", nullable: false),
                    PrimaryFormFactorID = table.Column<int>(type: "int", nullable: false),
                    SizeID = table.Column<int>(type: "int", nullable: false),
                    SidePanelMaterialID = table.Column<int>(type: "int", nullable: false),
                    ExternalVolume = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseUnits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_CaseUnits_CaseMaterials_SidePanelMaterialID",
                        column: x => x.SidePanelMaterialID,
                        principalTable: "CaseMaterials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseUnits_CaseSizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "CaseSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseUnits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseUnits_MotherboardFormFactors_PrimaryFormFactorID",
                        column: x => x.PrimaryFormFactorID,
                        principalTable: "MotherboardFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseUnits_PowerSupplyFormFactors_PowerSupplyFormFactorID",
                        column: x => x.PowerSupplyFormFactorID,
                        principalTable: "PowerSupplyFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoolerUnits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsPassive = table.Column<bool>(type: "bit", nullable: false),
                    FanCount = table.Column<int>(type: "int", nullable: true),
                    FanRpm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FanAirflow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FanNoiseLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FanStaticPressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirUnit_Height = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    LimitsMemoryHeight = table.Column<bool>(type: "bit", nullable: true),
                    MaximumMemoryHeight = table.Column<int>(type: "int", nullable: true),
                    RadiatorSizeID = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoolerUnits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_CoolerUnits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoolerUnits_CoolerRadiatorSizes_RadiatorSizeID",
                        column: x => x.RadiatorSizeID,
                        principalTable: "CoolerRadiatorSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FanPacks",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SizeID = table.Column<int>(type: "int", nullable: false),
                    Rpm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Airflow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiseLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaticPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pwm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FanPacks", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_FanPacks_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FanPacks_FanSizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "FanSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoryKits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    FormFactorID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    ClockFrequency = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsECC = table.Column<bool>(type: "bit", nullable: false),
                    IsBuffered = table.Column<bool>(type: "bit", nullable: false),
                    ModuleCount = table.Column<int>(type: "int", nullable: false),
                    CASLatency = table.Column<int>(type: "int", nullable: false),
                    FirstWordLatency = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    Voltage = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    Timing = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryKits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_MemoryKits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemoryKits_MemoryFormFactors_FormFactorID",
                        column: x => x.FormFactorID,
                        principalTable: "MemoryFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemoryKits_MemoryTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "MemoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardUnits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    ChipsetID = table.Column<int>(type: "int", nullable: false),
                    FormFactorID = table.Column<int>(type: "int", nullable: false),
                    ChannelID = table.Column<int>(type: "int", nullable: false),
                    MemoryFormFactorID = table.Column<int>(type: "int", nullable: false),
                    MemoryTypeID = table.Column<int>(type: "int", nullable: false),
                    MemoryTotalCapacity = table.Column<int>(type: "int", nullable: false),
                    MemorySlotCount = table.Column<int>(type: "int", nullable: false),
                    SupportECCMemory = table.Column<bool>(type: "bit", nullable: false),
                    SupportNonECCMemory = table.Column<bool>(type: "bit", nullable: false),
                    SupportBufferedMemory = table.Column<bool>(type: "bit", nullable: false),
                    SupportUnbufferedMemory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardUnits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_MotherboardUnits_CentralProcessorChannels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "CentralProcessorChannels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnits_MemoryFormFactors_MemoryFormFactorID",
                        column: x => x.MemoryFormFactorID,
                        principalTable: "MemoryFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnits_MemoryTypes_MemoryTypeID",
                        column: x => x.MemoryTypeID,
                        principalTable: "MemoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnits_MotherboardChipsets_ChipsetID",
                        column: x => x.ChipsetID,
                        principalTable: "MotherboardChipsets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnits_MotherboardFormFactors_FormFactorID",
                        column: x => x.FormFactorID,
                        principalTable: "MotherboardFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyUnits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    FormFactorID = table.Column<int>(type: "int", nullable: false),
                    ModularityID = table.Column<int>(type: "int", nullable: false),
                    EfficiencyRatingID = table.Column<int>(type: "int", nullable: false),
                    TotalPower = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Fanless = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyUnits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_PowerSupplyUnits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplyUnits_PowerSupplyEfficiencyRatings_EfficiencyRatingID",
                        column: x => x.EfficiencyRatingID,
                        principalTable: "PowerSupplyEfficiencyRatings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplyUnits_PowerSupplyFormFactors_FormFactorID",
                        column: x => x.FormFactorID,
                        principalTable: "PowerSupplyFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplyUnits_PowerSupplyModularities_ModularityID",
                        column: x => x.ModularityID,
                        principalTable: "PowerSupplyModularities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageUnits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    ConnectionInterfaceID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Cache = table.Column<int>(type: "int", nullable: false),
                    ReadSpeed = table.Column<int>(type: "int", nullable: false),
                    WriteSpeed = table.Column<int>(type: "int", nullable: false),
                    FormFactorID = table.Column<int>(type: "int", nullable: true),
                    IOConnectorID = table.Column<int>(type: "int", nullable: true),
                    PowerSupplyConnectorID = table.Column<int>(type: "int", nullable: true),
                    ExpansionCardID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageUnits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_StorageUnits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageUnits_IOConnectors_IOConnectorID",
                        column: x => x.IOConnectorID,
                        principalTable: "IOConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageUnits_M2ExpansionCards_ExpansionCardID",
                        column: x => x.ExpansionCardID,
                        principalTable: "M2ExpansionCards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageUnits_PowerSupplyConnectors_PowerSupplyConnectorID",
                        column: x => x.PowerSupplyConnectorID,
                        principalTable: "PowerSupplyConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageUnits_StorageConnectionInterfaces_ConnectionInterfaceID",
                        column: x => x.ConnectionInterfaceID,
                        principalTable: "StorageConnectionInterfaces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageUnits_StorageFormFactors_FormFactorID",
                        column: x => x.FormFactorID,
                        principalTable: "StorageFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormFactorSlot",
                columns: table => new
                {
                    FormFactorsID = table.Column<int>(type: "int", nullable: false),
                    SlotsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFactorSlot", x => new { x.FormFactorsID, x.SlotsID });
                    table.ForeignKey(
                        name: "FK_FormFactorSlot_M2FormFactors_FormFactorsID",
                        column: x => x.FormFactorsID,
                        principalTable: "M2FormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormFactorSlot_M2Slots_SlotsID",
                        column: x => x.SlotsID,
                        principalTable: "M2Slots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCardUnits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    ExpansionCardID = table.Column<int>(type: "int", nullable: false),
                    ChipsetID = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    TotalSlotWidth = table.Column<int>(type: "int", nullable: false),
                    TotalPower = table.Column<int>(type: "int", nullable: false),
                    RecommendedPower = table.Column<int>(type: "int", nullable: false),
                    MemoryCapacity = table.Column<int>(type: "int", nullable: false),
                    MemoryTypeID = table.Column<int>(type: "int", nullable: false),
                    CoreClock = table.Column<int>(type: "int", nullable: false),
                    BoostClock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCardUnits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_GraphicsCardUnits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraphicsCardUnits_GraphicsCardChipsets_ChipsetID",
                        column: x => x.ChipsetID,
                        principalTable: "GraphicsCardChipsets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraphicsCardUnits_MemoryTypes_MemoryTypeID",
                        column: x => x.MemoryTypeID,
                        principalTable: "MemoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraphicsCardUnits_PcieExpansionCards_ExpansionCardID",
                        column: x => x.ExpansionCardID,
                        principalTable: "PcieExpansionCards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseExpansionSlotAreas",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    BracketID = table.Column<int>(type: "int", nullable: false),
                    SlotCount = table.Column<int>(type: "int", nullable: false),
                    RiserRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseExpansionSlotAreas", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_CaseExpansionSlotAreas_CaseUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "CaseUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseExpansionSlotAreas_PcieBrackets_BracketID",
                        column: x => x.BracketID,
                        principalTable: "PcieBrackets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseLayouts",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    MaxPowerSupplyLength = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    MaxAirCoolerHeight = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    MaxGraphicsProcessorUnitLength = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseLayouts", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_CaseLayouts_CaseUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "CaseUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseUnitIOConnectors",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseUnitIOConnectors", x => new { x.UnitID, x.ConnectorID });
                    table.ForeignKey(
                        name: "FK_CaseUnitIOConnectors_CaseUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "CaseUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseUnitIOConnectors_IOConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "IOConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseUnitPowerSupplyConnectors",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseUnitPowerSupplyConnectors", x => new { x.UnitID, x.ConnectorID });
                    table.ForeignKey(
                        name: "FK_CaseUnitPowerSupplyConnectors_CaseUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "CaseUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseUnitPowerSupplyConnectors_PowerSupplyConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "PowerSupplyConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormFactorUnit",
                columns: table => new
                {
                    CasesComponentID = table.Column<int>(type: "int", nullable: false),
                    MotherboardFormFactorsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFactorUnit", x => new { x.CasesComponentID, x.MotherboardFormFactorsID });
                    table.ForeignKey(
                        name: "FK_FormFactorUnit_CaseUnits_CasesComponentID",
                        column: x => x.CasesComponentID,
                        principalTable: "CaseUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormFactorUnit_MotherboardFormFactors_MotherboardFormFactorsID",
                        column: x => x.MotherboardFormFactorsID,
                        principalTable: "MotherboardFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoolerUnitConnectors",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoolerUnitConnectors", x => new { x.UnitID, x.ConnectorID });
                    table.ForeignKey(
                        name: "FK_CoolerUnitConnectors_CoolerUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "CoolerUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoolerUnitConnectors_IOConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "IOConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocketUnit",
                columns: table => new
                {
                    CoolersComponentID = table.Column<int>(type: "int", nullable: false),
                    SocketsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocketUnit", x => new { x.CoolersComponentID, x.SocketsID });
                    table.ForeignKey(
                        name: "FK_SocketUnit_CentralProcessorSockets_SocketsID",
                        column: x => x.SocketsID,
                        principalTable: "CentralProcessorSockets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SocketUnit_CoolerUnits_CoolersComponentID",
                        column: x => x.CoolersComponentID,
                        principalTable: "CoolerUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackConnector",
                columns: table => new
                {
                    PackID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackConnector", x => new { x.PackID, x.ConnectorID });
                    table.ForeignKey(
                        name: "FK_PackConnector_FanPacks_PackID",
                        column: x => x.PackID,
                        principalTable: "FanPacks",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackConnector_IOConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "IOConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardUnitIOConnectors",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardUnitIOConnectors", x => new { x.UnitID, x.ConnectorID });
                    table.ForeignKey(
                        name: "FK_MotherboardUnitIOConnectors_IOConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "IOConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnitIOConnectors_MotherboardUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "MotherboardUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardUnitM2Slots",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    SlotID = table.Column<int>(type: "int", nullable: false),
                    SlotPosition = table.Column<int>(type: "int", nullable: false),
                    ConfigurationNumber = table.Column<int>(type: "int", nullable: false),
                    HasConfigurationNumber = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardUnitM2Slots", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_MotherboardUnitM2Slots_M2Slots_SlotID",
                        column: x => x.SlotID,
                        principalTable: "M2Slots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnitM2Slots_MotherboardUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "MotherboardUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardUnitPcieSlots",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    SlotID = table.Column<int>(type: "int", nullable: false),
                    SlotPosition = table.Column<int>(type: "int", nullable: false),
                    ConfigurationNumber = table.Column<int>(type: "int", nullable: false),
                    HasConfigurationNumber = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardUnitPcieSlots", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_MotherboardUnitPcieSlots_MotherboardUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "MotherboardUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnitPcieSlots_PcieSlots_SlotID",
                        column: x => x.SlotID,
                        principalTable: "PcieSlots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardUnitPowerSupplyConnectors",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardUnitPowerSupplyConnectors", x => new { x.UnitID, x.ConnectorID });
                    table.ForeignKey(
                        name: "FK_MotherboardUnitPowerSupplyConnectors_MotherboardUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "MotherboardUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardUnitPowerSupplyConnectors_PowerSupplyConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "PowerSupplyConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyUnitConnectors",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    SplitCount = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyUnitConnectors", x => new { x.UnitID, x.ConnectorID, x.SplitCount });
                    table.ForeignKey(
                        name: "FK_PowerSupplyUnitConnectors_PowerSupplyConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "PowerSupplyConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplyUnitConnectors_PowerSupplyUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "PowerSupplyUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageDrives",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Rpm = table.Column<int>(type: "int", nullable: true),
                    NandTypeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageDrives", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_StorageDrives_StorageNandTypes_NandTypeID",
                        column: x => x.NandTypeID,
                        principalTable: "StorageNandTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageDrives_StorageUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "StorageUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCardConfigurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCardConfigurations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GraphicsCardConfigurations_GraphicsCardUnits_UnitID",
                        column: x => x.UnitID,
                        principalTable: "GraphicsCardUnits",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseLayoutPanels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LayoutID = table.Column<int>(type: "int", nullable: false),
                    PanelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseLayoutPanels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CaseLayoutPanels_CaseLayouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "CaseLayouts",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseLayoutPanels_CasePanels_PanelID",
                        column: x => x.PanelID,
                        principalTable: "CasePanels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseStorageAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LayoutID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStorageAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CaseStorageAreas_CaseLayouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "CaseLayouts",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralProcessorCoreFamilies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MicroarchitectureID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitM2SlotUnitID = table.Column<int>(type: "int", nullable: true),
                    UnitPcieSlotUnitID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralProcessorCoreFamilies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CentralProcessorCoreFamilies_CentralProcessorMicroarchitectures_MicroarchitectureID",
                        column: x => x.MicroarchitectureID,
                        principalTable: "CentralProcessorMicroarchitectures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralProcessorCoreFamilies_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                        column: x => x.UnitM2SlotUnitID,
                        principalTable: "MotherboardUnitM2Slots",
                        principalColumn: "UnitID");
                    table.ForeignKey(
                        name: "FK_CentralProcessorCoreFamilies_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                        column: x => x.UnitPcieSlotUnitID,
                        principalTable: "MotherboardUnitPcieSlots",
                        principalColumn: "UnitID");
                });

            migrationBuilder.CreateTable(
                name: "CentralProcessorSeries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitM2SlotUnitID = table.Column<int>(type: "int", nullable: true),
                    UnitPcieSlotUnitID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralProcessorSeries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CentralProcessorSeries_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                        column: x => x.UnitM2SlotUnitID,
                        principalTable: "MotherboardUnitM2Slots",
                        principalColumn: "UnitID");
                    table.ForeignKey(
                        name: "FK_CentralProcessorSeries_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                        column: x => x.UnitPcieSlotUnitID,
                        principalTable: "MotherboardUnitPcieSlots",
                        principalColumn: "UnitID");
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCardConfigurationConnectors",
                columns: table => new
                {
                    ConfigurationID = table.Column<int>(type: "int", nullable: false),
                    ConnectorID = table.Column<int>(type: "int", nullable: false),
                    ConnectorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCardConfigurationConnectors", x => new { x.ConfigurationID, x.ConnectorID });
                    table.ForeignKey(
                        name: "FK_GraphicsCardConfigurationConnectors_GraphicsCardConfigurations_ConfigurationID",
                        column: x => x.ConfigurationID,
                        principalTable: "GraphicsCardConfigurations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraphicsCardConfigurationConnectors_PowerSupplyConnectors_ConnectorID",
                        column: x => x.ConnectorID,
                        principalTable: "PowerSupplyConnectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseLayoutPanelFans",
                columns: table => new
                {
                    LayoutPanelID = table.Column<int>(type: "int", nullable: false),
                    FanSizeID = table.Column<int>(type: "int", nullable: false),
                    FanCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseLayoutPanelFans", x => new { x.LayoutPanelID, x.FanSizeID });
                    table.ForeignKey(
                        name: "FK_CaseLayoutPanelFans_CaseLayoutPanels_LayoutPanelID",
                        column: x => x.LayoutPanelID,
                        principalTable: "CaseLayoutPanels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseLayoutPanelFans_FanSizes_FanSizeID",
                        column: x => x.FanSizeID,
                        principalTable: "FanSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseLayoutPanelRadiators",
                columns: table => new
                {
                    LayoutPanelID = table.Column<int>(type: "int", nullable: false),
                    RadiatorSizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseLayoutPanelRadiators", x => new { x.LayoutPanelID, x.RadiatorSizeID });
                    table.ForeignKey(
                        name: "FK_CaseLayoutPanelRadiators_CaseLayoutPanels_LayoutPanelID",
                        column: x => x.LayoutPanelID,
                        principalTable: "CaseLayoutPanels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseLayoutPanelRadiators_CoolerRadiatorSizes_RadiatorSizeID",
                        column: x => x.RadiatorSizeID,
                        principalTable: "CoolerRadiatorSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseDriveBays",
                columns: table => new
                {
                    StorageAreaID = table.Column<int>(type: "int", nullable: false),
                    FormFactorID = table.Column<int>(type: "int", nullable: false),
                    DriveCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseDriveBays", x => new { x.StorageAreaID, x.FormFactorID });
                    table.ForeignKey(
                        name: "FK_CaseDriveBays_CaseStorageAreas_StorageAreaID",
                        column: x => x.StorageAreaID,
                        principalTable: "CaseStorageAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseDriveBays_StorageFormFactors_FormFactorID",
                        column: x => x.FormFactorID,
                        principalTable: "StorageFormFactors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralProcessorUnits",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    SocketID = table.Column<int>(type: "int", nullable: false),
                    SeriesID = table.Column<int>(type: "int", nullable: false),
                    ChannelID = table.Column<int>(type: "int", nullable: false),
                    CoreFamilyID = table.Column<int>(type: "int", nullable: false),
                    MaxTotalMemoryCapacity = table.Column<int>(type: "int", nullable: false),
                    TotalPower = table.Column<int>(type: "int", nullable: false),
                    HasIntegratedGraphics = table.Column<bool>(type: "bit", nullable: false),
                    CoolerIncluded = table.Column<bool>(type: "bit", nullable: false),
                    SupportECCMemory = table.Column<bool>(type: "bit", nullable: false),
                    SupportNonECCMemory = table.Column<bool>(type: "bit", nullable: false),
                    SupportBufferedMemory = table.Column<bool>(type: "bit", nullable: false),
                    SupportUnbufferedMemory = table.Column<bool>(type: "bit", nullable: false),
                    CoreCount = table.Column<int>(type: "int", nullable: false),
                    ThreadCount = table.Column<int>(type: "int", nullable: false),
                    PerformanceCoreClock = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    PerformanceCoreBoostClock = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HasEfficiencyCores = table.Column<bool>(type: "bit", nullable: false),
                    EfficiencyCoreClock = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    EfficiencyCoreBoostClock = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    L2Cache = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    L3Cache = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SimultaneousMultithreading = table.Column<bool>(type: "bit", nullable: false),
                    MicroarchitectureID = table.Column<int>(type: "int", nullable: true),
                    SizeID = table.Column<int>(type: "int", nullable: true),
                    UnitM2SlotUnitID = table.Column<int>(type: "int", nullable: true),
                    UnitPcieSlotUnitID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralProcessorUnits", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_CentralProcessorChannels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "CentralProcessorChannels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_CentralProcessorCoreFamilies_CoreFamilyID",
                        column: x => x.CoreFamilyID,
                        principalTable: "CentralProcessorCoreFamilies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_CentralProcessorMicroarchitectures_MicroarchitectureID",
                        column: x => x.MicroarchitectureID,
                        principalTable: "CentralProcessorMicroarchitectures",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_CentralProcessorSeries_SeriesID",
                        column: x => x.SeriesID,
                        principalTable: "CentralProcessorSeries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_CentralProcessorSockets_SocketID",
                        column: x => x.SocketID,
                        principalTable: "CentralProcessorSockets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_Components_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Components",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_FanSizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "FanSizes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                        column: x => x.UnitM2SlotUnitID,
                        principalTable: "MotherboardUnitM2Slots",
                        principalColumn: "UnitID");
                    table.ForeignKey(
                        name: "FK_CentralProcessorUnits_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                        column: x => x.UnitPcieSlotUnitID,
                        principalTable: "MotherboardUnitPcieSlots",
                        principalColumn: "UnitID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseDriveBays_FormFactorID",
                table: "CaseDriveBays",
                column: "FormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseExpansionSlotAreas_BracketID",
                table: "CaseExpansionSlotAreas",
                column: "BracketID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseLayoutPanelFans_FanSizeID",
                table: "CaseLayoutPanelFans",
                column: "FanSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseLayoutPanelRadiators_RadiatorSizeID",
                table: "CaseLayoutPanelRadiators",
                column: "RadiatorSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseLayoutPanels_LayoutID_PanelID",
                table: "CaseLayoutPanels",
                columns: new[] { "LayoutID", "PanelID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseLayoutPanels_PanelID",
                table: "CaseLayoutPanels",
                column: "PanelID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseMaterials_Name",
                table: "CaseMaterials",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CasePanels_Name",
                table: "CasePanels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseSizes_Name",
                table: "CaseSizes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseStorageAreas_LayoutID",
                table: "CaseStorageAreas",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseUnitIOConnectors_ConnectorID",
                table: "CaseUnitIOConnectors",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseUnitPowerSupplyConnectors_ConnectorID",
                table: "CaseUnitPowerSupplyConnectors",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseUnits_PowerSupplyFormFactorID",
                table: "CaseUnits",
                column: "PowerSupplyFormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseUnits_PrimaryFormFactorID",
                table: "CaseUnits",
                column: "PrimaryFormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseUnits_SidePanelMaterialID",
                table: "CaseUnits",
                column: "SidePanelMaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseUnits_SizeID",
                table: "CaseUnits",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorChannels_Name",
                table: "CentralProcessorChannels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorCoreFamilies_MicroarchitectureID",
                table: "CentralProcessorCoreFamilies",
                column: "MicroarchitectureID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorCoreFamilies_Name",
                table: "CentralProcessorCoreFamilies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorCoreFamilies_UnitM2SlotUnitID",
                table: "CentralProcessorCoreFamilies",
                column: "UnitM2SlotUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorCoreFamilies_UnitPcieSlotUnitID",
                table: "CentralProcessorCoreFamilies",
                column: "UnitPcieSlotUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorMicroarchitectures_Name",
                table: "CentralProcessorMicroarchitectures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorSeries_Name",
                table: "CentralProcessorSeries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorSeries_UnitM2SlotUnitID",
                table: "CentralProcessorSeries",
                column: "UnitM2SlotUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorSeries_UnitPcieSlotUnitID",
                table: "CentralProcessorSeries",
                column: "UnitPcieSlotUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorSockets_Name",
                table: "CentralProcessorSockets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_ChannelID",
                table: "CentralProcessorUnits",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_CoreFamilyID",
                table: "CentralProcessorUnits",
                column: "CoreFamilyID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_MicroarchitectureID",
                table: "CentralProcessorUnits",
                column: "MicroarchitectureID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_SeriesID",
                table: "CentralProcessorUnits",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_SizeID",
                table: "CentralProcessorUnits",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_SocketID",
                table: "CentralProcessorUnits",
                column: "SocketID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_UnitM2SlotUnitID",
                table: "CentralProcessorUnits",
                column: "UnitM2SlotUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_UnitPcieSlotUnitID",
                table: "CentralProcessorUnits",
                column: "UnitPcieSlotUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Colours_Name",
                table: "Colours",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_ColourID",
                table: "Components",
                column: "ColourID");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ManufacturerID",
                table: "Components",
                column: "ManufacturerID");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorConnector_PhysicalConnectorsID",
                table: "ConnectorConnector",
                column: "PhysicalConnectorsID");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorConnector1_PhysicalConnectorsID",
                table: "ConnectorConnector1",
                column: "PhysicalConnectorsID");

            migrationBuilder.CreateIndex(
                name: "IX_CoolerRadiatorSizes_Length",
                table: "CoolerRadiatorSizes",
                column: "Length",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoolerUnitConnectors_ConnectorID",
                table: "CoolerUnitConnectors",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_CoolerUnits_RadiatorSizeID",
                table: "CoolerUnits",
                column: "RadiatorSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_FanPacks_SizeID",
                table: "FanPacks",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_FanSizes_SideLength",
                table: "FanSizes",
                column: "SideLength",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormFactorSlot_SlotsID",
                table: "FormFactorSlot",
                column: "SlotsID");

            migrationBuilder.CreateIndex(
                name: "IX_FormFactorUnit_MotherboardFormFactorsID",
                table: "FormFactorUnit",
                column: "MotherboardFormFactorsID");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardChipsets_Name",
                table: "GraphicsCardChipsets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardConfigurationConnectors_ConnectorID",
                table: "GraphicsCardConfigurationConnectors",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardConfigurations_UnitID",
                table: "GraphicsCardConfigurations",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardUnits_ChipsetID",
                table: "GraphicsCardUnits",
                column: "ChipsetID");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardUnits_ExpansionCardID",
                table: "GraphicsCardUnits",
                column: "ExpansionCardID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardUnits_MemoryTypeID",
                table: "GraphicsCardUnits",
                column: "MemoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_IOConnectors_Name",
                table: "IOConnectors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyKey_PhysicalKeysID",
                table: "KeyKey",
                column: "PhysicalKeysID");

            migrationBuilder.CreateIndex(
                name: "IX_M2ExpansionCards_FormFactorID",
                table: "M2ExpansionCards",
                column: "FormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_M2ExpansionCards_KeyID",
                table: "M2ExpansionCards",
                column: "KeyID");

            migrationBuilder.CreateIndex(
                name: "IX_M2ExpansionCards_LaneSizeID",
                table: "M2ExpansionCards",
                column: "LaneSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_M2ExpansionCards_VersionID",
                table: "M2ExpansionCards",
                column: "VersionID");

            migrationBuilder.CreateIndex(
                name: "IX_M2FormFactors_Name",
                table: "M2FormFactors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_M2Keys_Name",
                table: "M2Keys",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_M2Slots_KeyID",
                table: "M2Slots",
                column: "KeyID");

            migrationBuilder.CreateIndex(
                name: "IX_M2Slots_LaneSizeID",
                table: "M2Slots",
                column: "LaneSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_M2Slots_VersionID",
                table: "M2Slots",
                column: "VersionID");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_Name",
                table: "Manufacturers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemoryFormFactors_Name",
                table: "MemoryFormFactors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemoryKits_FormFactorID",
                table: "MemoryKits",
                column: "FormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryKits_TypeID",
                table: "MemoryKits",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryTypes_Name",
                table: "MemoryTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardChipsets_Name",
                table: "MotherboardChipsets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardChipsets_SocketID",
                table: "MotherboardChipsets",
                column: "SocketID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardFormFactors_Name",
                table: "MotherboardFormFactors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnitIOConnectors_ConnectorID",
                table: "MotherboardUnitIOConnectors",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnitM2Slots_SlotID",
                table: "MotherboardUnitM2Slots",
                column: "SlotID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnitM2Slots_UnitID_SlotID_SlotPosition_ConfigurationNumber_HasConfigurationNumber",
                table: "MotherboardUnitM2Slots",
                columns: new[] { "UnitID", "SlotID", "SlotPosition", "ConfigurationNumber", "HasConfigurationNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnitPcieSlots_SlotID",
                table: "MotherboardUnitPcieSlots",
                column: "SlotID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnitPcieSlots_UnitID_SlotID_SlotPosition_ConfigurationNumber_HasConfigurationNumber",
                table: "MotherboardUnitPcieSlots",
                columns: new[] { "UnitID", "SlotID", "SlotPosition", "ConfigurationNumber", "HasConfigurationNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnitPowerSupplyConnectors_ConnectorID",
                table: "MotherboardUnitPowerSupplyConnectors",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnits_ChannelID",
                table: "MotherboardUnits",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnits_ChipsetID",
                table: "MotherboardUnits",
                column: "ChipsetID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnits_FormFactorID",
                table: "MotherboardUnits",
                column: "FormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnits_MemoryFormFactorID",
                table: "MotherboardUnits",
                column: "MemoryFormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnits_MemoryTypeID",
                table: "MotherboardUnits",
                column: "MemoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PackConnector_ConnectorID",
                table: "PackConnector",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieBrackets_Name",
                table: "PcieBrackets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PcieExpansionCards_BracketID",
                table: "PcieExpansionCards",
                column: "BracketID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieExpansionCards_LaneSizeID",
                table: "PcieExpansionCards",
                column: "LaneSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieExpansionCards_PhysicalSizeID",
                table: "PcieExpansionCards",
                column: "PhysicalSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieExpansionCards_VersionID",
                table: "PcieExpansionCards",
                column: "VersionID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieSlots_LaneSizeID",
                table: "PcieSlots",
                column: "LaneSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieSlots_PhysicalSizeID",
                table: "PcieSlots",
                column: "PhysicalSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieSlots_VersionID",
                table: "PcieSlots",
                column: "VersionID");

            migrationBuilder.CreateIndex(
                name: "IX_PcieVersions_Name",
                table: "PcieVersions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyConnectors_Name",
                table: "PowerSupplyConnectors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyEfficiencyRatings_Name",
                table: "PowerSupplyEfficiencyRatings",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyFormFactors_Name",
                table: "PowerSupplyFormFactors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyModularities_Name",
                table: "PowerSupplyModularities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyUnitConnectors_ConnectorID",
                table: "PowerSupplyUnitConnectors",
                column: "ConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyUnits_EfficiencyRatingID",
                table: "PowerSupplyUnits",
                column: "EfficiencyRatingID");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyUnits_FormFactorID",
                table: "PowerSupplyUnits",
                column: "FormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyUnits_ModularityID",
                table: "PowerSupplyUnits",
                column: "ModularityID");

            migrationBuilder.CreateIndex(
                name: "IX_SocketUnit_SocketsID",
                table: "SocketUnit",
                column: "SocketsID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageConnectionInterfaces_Name",
                table: "StorageConnectionInterfaces",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageDrives_NandTypeID",
                table: "StorageDrives",
                column: "NandTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageFormFactors_Name",
                table: "StorageFormFactors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageNandTypes_Name",
                table: "StorageNandTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageUnits_ConnectionInterfaceID",
                table: "StorageUnits",
                column: "ConnectionInterfaceID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageUnits_ExpansionCardID",
                table: "StorageUnits",
                column: "ExpansionCardID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageUnits_FormFactorID",
                table: "StorageUnits",
                column: "FormFactorID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageUnits_IOConnectorID",
                table: "StorageUnits",
                column: "IOConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_StorageUnits_PowerSupplyConnectorID",
                table: "StorageUnits",
                column: "PowerSupplyConnectorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseDriveBays");

            migrationBuilder.DropTable(
                name: "CaseExpansionSlotAreas");

            migrationBuilder.DropTable(
                name: "CaseLayoutPanelFans");

            migrationBuilder.DropTable(
                name: "CaseLayoutPanelRadiators");

            migrationBuilder.DropTable(
                name: "CaseUnitIOConnectors");

            migrationBuilder.DropTable(
                name: "CaseUnitPowerSupplyConnectors");

            migrationBuilder.DropTable(
                name: "CentralProcessorUnits");

            migrationBuilder.DropTable(
                name: "ConnectorConnector");

            migrationBuilder.DropTable(
                name: "ConnectorConnector1");

            migrationBuilder.DropTable(
                name: "CoolerUnitConnectors");

            migrationBuilder.DropTable(
                name: "FormFactorSlot");

            migrationBuilder.DropTable(
                name: "FormFactorUnit");

            migrationBuilder.DropTable(
                name: "GraphicsCardConfigurationConnectors");

            migrationBuilder.DropTable(
                name: "KeyKey");

            migrationBuilder.DropTable(
                name: "MemoryKits");

            migrationBuilder.DropTable(
                name: "MotherboardUnitIOConnectors");

            migrationBuilder.DropTable(
                name: "MotherboardUnitPowerSupplyConnectors");

            migrationBuilder.DropTable(
                name: "PackConnector");

            migrationBuilder.DropTable(
                name: "PowerSupplyUnitConnectors");

            migrationBuilder.DropTable(
                name: "SocketUnit");

            migrationBuilder.DropTable(
                name: "StorageDrives");

            migrationBuilder.DropTable(
                name: "CaseStorageAreas");

            migrationBuilder.DropTable(
                name: "CaseLayoutPanels");

            migrationBuilder.DropTable(
                name: "CentralProcessorCoreFamilies");

            migrationBuilder.DropTable(
                name: "CentralProcessorSeries");

            migrationBuilder.DropTable(
                name: "GraphicsCardConfigurations");

            migrationBuilder.DropTable(
                name: "FanPacks");

            migrationBuilder.DropTable(
                name: "PowerSupplyUnits");

            migrationBuilder.DropTable(
                name: "CoolerUnits");

            migrationBuilder.DropTable(
                name: "StorageNandTypes");

            migrationBuilder.DropTable(
                name: "StorageUnits");

            migrationBuilder.DropTable(
                name: "CaseLayouts");

            migrationBuilder.DropTable(
                name: "CasePanels");

            migrationBuilder.DropTable(
                name: "CentralProcessorMicroarchitectures");

            migrationBuilder.DropTable(
                name: "MotherboardUnitM2Slots");

            migrationBuilder.DropTable(
                name: "MotherboardUnitPcieSlots");

            migrationBuilder.DropTable(
                name: "GraphicsCardUnits");

            migrationBuilder.DropTable(
                name: "FanSizes");

            migrationBuilder.DropTable(
                name: "PowerSupplyEfficiencyRatings");

            migrationBuilder.DropTable(
                name: "PowerSupplyModularities");

            migrationBuilder.DropTable(
                name: "CoolerRadiatorSizes");

            migrationBuilder.DropTable(
                name: "IOConnectors");

            migrationBuilder.DropTable(
                name: "M2ExpansionCards");

            migrationBuilder.DropTable(
                name: "PowerSupplyConnectors");

            migrationBuilder.DropTable(
                name: "StorageConnectionInterfaces");

            migrationBuilder.DropTable(
                name: "StorageFormFactors");

            migrationBuilder.DropTable(
                name: "CaseUnits");

            migrationBuilder.DropTable(
                name: "M2Slots");

            migrationBuilder.DropTable(
                name: "MotherboardUnits");

            migrationBuilder.DropTable(
                name: "PcieSlots");

            migrationBuilder.DropTable(
                name: "GraphicsCardChipsets");

            migrationBuilder.DropTable(
                name: "PcieExpansionCards");

            migrationBuilder.DropTable(
                name: "M2FormFactors");

            migrationBuilder.DropTable(
                name: "CaseMaterials");

            migrationBuilder.DropTable(
                name: "CaseSizes");

            migrationBuilder.DropTable(
                name: "PowerSupplyFormFactors");

            migrationBuilder.DropTable(
                name: "M2Keys");

            migrationBuilder.DropTable(
                name: "CentralProcessorChannels");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "MemoryFormFactors");

            migrationBuilder.DropTable(
                name: "MemoryTypes");

            migrationBuilder.DropTable(
                name: "MotherboardChipsets");

            migrationBuilder.DropTable(
                name: "MotherboardFormFactors");

            migrationBuilder.DropTable(
                name: "PcieBrackets");

            migrationBuilder.DropTable(
                name: "PcieSizes");

            migrationBuilder.DropTable(
                name: "PcieVersions");

            migrationBuilder.DropTable(
                name: "Colours");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "CentralProcessorSockets");
        }
    }
}
