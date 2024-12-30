using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfiguratorBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKeyForMotherboardSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                table: "CentralProcessorCoreFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                table: "CentralProcessorCoreFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                table: "CentralProcessorSeries");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                table: "CentralProcessorSeries");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                table: "CentralProcessorUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                table: "CentralProcessorUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotherboardUnitPcieSlots",
                table: "MotherboardUnitPcieSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotherboardUnitM2Slots",
                table: "MotherboardUnitM2Slots");

            migrationBuilder.RenameColumn(
                name: "UnitPcieSlotUnitID",
                table: "CentralProcessorUnits",
                newName: "UnitPcieSlotID");

            migrationBuilder.RenameColumn(
                name: "UnitM2SlotUnitID",
                table: "CentralProcessorUnits",
                newName: "UnitM2SlotID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorUnits_UnitPcieSlotUnitID",
                table: "CentralProcessorUnits",
                newName: "IX_CentralProcessorUnits_UnitPcieSlotID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorUnits_UnitM2SlotUnitID",
                table: "CentralProcessorUnits",
                newName: "IX_CentralProcessorUnits_UnitM2SlotID");

            migrationBuilder.RenameColumn(
                name: "UnitPcieSlotUnitID",
                table: "CentralProcessorSeries",
                newName: "UnitPcieSlotID");

            migrationBuilder.RenameColumn(
                name: "UnitM2SlotUnitID",
                table: "CentralProcessorSeries",
                newName: "UnitM2SlotID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorSeries_UnitPcieSlotUnitID",
                table: "CentralProcessorSeries",
                newName: "IX_CentralProcessorSeries_UnitPcieSlotID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorSeries_UnitM2SlotUnitID",
                table: "CentralProcessorSeries",
                newName: "IX_CentralProcessorSeries_UnitM2SlotID");

            migrationBuilder.RenameColumn(
                name: "UnitPcieSlotUnitID",
                table: "CentralProcessorCoreFamilies",
                newName: "UnitPcieSlotID");

            migrationBuilder.RenameColumn(
                name: "UnitM2SlotUnitID",
                table: "CentralProcessorCoreFamilies",
                newName: "UnitM2SlotID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorCoreFamilies_UnitPcieSlotUnitID",
                table: "CentralProcessorCoreFamilies",
                newName: "IX_CentralProcessorCoreFamilies_UnitPcieSlotID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorCoreFamilies_UnitM2SlotUnitID",
                table: "CentralProcessorCoreFamilies",
                newName: "IX_CentralProcessorCoreFamilies_UnitM2SlotID");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "MotherboardUnitPcieSlots",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "MotherboardUnitM2Slots",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotherboardUnitPcieSlots",
                table: "MotherboardUnitPcieSlots",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotherboardUnitM2Slots",
                table: "MotherboardUnitM2Slots",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitM2Slots_UnitM2SlotID",
                table: "CentralProcessorCoreFamilies",
                column: "UnitM2SlotID",
                principalTable: "MotherboardUnitM2Slots",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitPcieSlots_UnitPcieSlotID",
                table: "CentralProcessorCoreFamilies",
                column: "UnitPcieSlotID",
                principalTable: "MotherboardUnitPcieSlots",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitM2Slots_UnitM2SlotID",
                table: "CentralProcessorSeries",
                column: "UnitM2SlotID",
                principalTable: "MotherboardUnitM2Slots",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitPcieSlots_UnitPcieSlotID",
                table: "CentralProcessorSeries",
                column: "UnitPcieSlotID",
                principalTable: "MotherboardUnitPcieSlots",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitM2Slots_UnitM2SlotID",
                table: "CentralProcessorUnits",
                column: "UnitM2SlotID",
                principalTable: "MotherboardUnitM2Slots",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitPcieSlots_UnitPcieSlotID",
                table: "CentralProcessorUnits",
                column: "UnitPcieSlotID",
                principalTable: "MotherboardUnitPcieSlots",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitM2Slots_UnitM2SlotID",
                table: "CentralProcessorCoreFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitPcieSlots_UnitPcieSlotID",
                table: "CentralProcessorCoreFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitM2Slots_UnitM2SlotID",
                table: "CentralProcessorSeries");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitPcieSlots_UnitPcieSlotID",
                table: "CentralProcessorSeries");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitM2Slots_UnitM2SlotID",
                table: "CentralProcessorUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitPcieSlots_UnitPcieSlotID",
                table: "CentralProcessorUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotherboardUnitPcieSlots",
                table: "MotherboardUnitPcieSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotherboardUnitM2Slots",
                table: "MotherboardUnitM2Slots");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "MotherboardUnitPcieSlots");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "MotherboardUnitM2Slots");

            migrationBuilder.RenameColumn(
                name: "UnitPcieSlotID",
                table: "CentralProcessorUnits",
                newName: "UnitPcieSlotUnitID");

            migrationBuilder.RenameColumn(
                name: "UnitM2SlotID",
                table: "CentralProcessorUnits",
                newName: "UnitM2SlotUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorUnits_UnitPcieSlotID",
                table: "CentralProcessorUnits",
                newName: "IX_CentralProcessorUnits_UnitPcieSlotUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorUnits_UnitM2SlotID",
                table: "CentralProcessorUnits",
                newName: "IX_CentralProcessorUnits_UnitM2SlotUnitID");

            migrationBuilder.RenameColumn(
                name: "UnitPcieSlotID",
                table: "CentralProcessorSeries",
                newName: "UnitPcieSlotUnitID");

            migrationBuilder.RenameColumn(
                name: "UnitM2SlotID",
                table: "CentralProcessorSeries",
                newName: "UnitM2SlotUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorSeries_UnitPcieSlotID",
                table: "CentralProcessorSeries",
                newName: "IX_CentralProcessorSeries_UnitPcieSlotUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorSeries_UnitM2SlotID",
                table: "CentralProcessorSeries",
                newName: "IX_CentralProcessorSeries_UnitM2SlotUnitID");

            migrationBuilder.RenameColumn(
                name: "UnitPcieSlotID",
                table: "CentralProcessorCoreFamilies",
                newName: "UnitPcieSlotUnitID");

            migrationBuilder.RenameColumn(
                name: "UnitM2SlotID",
                table: "CentralProcessorCoreFamilies",
                newName: "UnitM2SlotUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorCoreFamilies_UnitPcieSlotID",
                table: "CentralProcessorCoreFamilies",
                newName: "IX_CentralProcessorCoreFamilies_UnitPcieSlotUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_CentralProcessorCoreFamilies_UnitM2SlotID",
                table: "CentralProcessorCoreFamilies",
                newName: "IX_CentralProcessorCoreFamilies_UnitM2SlotUnitID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotherboardUnitPcieSlots",
                table: "MotherboardUnitPcieSlots",
                column: "UnitID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotherboardUnitM2Slots",
                table: "MotherboardUnitM2Slots",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                table: "CentralProcessorCoreFamilies",
                column: "UnitM2SlotUnitID",
                principalTable: "MotherboardUnitM2Slots",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorCoreFamilies_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                table: "CentralProcessorCoreFamilies",
                column: "UnitPcieSlotUnitID",
                principalTable: "MotherboardUnitPcieSlots",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                table: "CentralProcessorSeries",
                column: "UnitM2SlotUnitID",
                principalTable: "MotherboardUnitM2Slots",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorSeries_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                table: "CentralProcessorSeries",
                column: "UnitPcieSlotUnitID",
                principalTable: "MotherboardUnitPcieSlots",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitM2Slots_UnitM2SlotUnitID",
                table: "CentralProcessorUnits",
                column: "UnitM2SlotUnitID",
                principalTable: "MotherboardUnitM2Slots",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorUnits_MotherboardUnitPcieSlots_UnitPcieSlotUnitID",
                table: "CentralProcessorUnits",
                column: "UnitPcieSlotUnitID",
                principalTable: "MotherboardUnitPcieSlots",
                principalColumn: "UnitID");
        }
    }
}
