using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfiguratorBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCentralProcessorChannels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralProcessorUnits_CentralProcessorChannels_ChannelID",
                table: "CentralProcessorUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_MotherboardUnits_CentralProcessorChannels_ChannelID",
                table: "MotherboardUnits");

            migrationBuilder.DropTable(
                name: "CentralProcessorChannels");

            migrationBuilder.DropIndex(
                name: "IX_MotherboardUnits_ChannelID",
                table: "MotherboardUnits");

            migrationBuilder.DropIndex(
                name: "IX_CentralProcessorUnits_ChannelID",
                table: "CentralProcessorUnits");

            migrationBuilder.RenameColumn(
                name: "ChannelID",
                table: "MotherboardUnits",
                newName: "ChannelCount");

            migrationBuilder.RenameColumn(
                name: "ChannelID",
                table: "CentralProcessorUnits",
                newName: "ChannelCount");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Components",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChannelCount",
                table: "MotherboardUnits",
                newName: "ChannelID");

            migrationBuilder.RenameColumn(
                name: "ChannelCount",
                table: "CentralProcessorUnits",
                newName: "ChannelID");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Components",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardUnits_ChannelID",
                table: "MotherboardUnits",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorUnits_ChannelID",
                table: "CentralProcessorUnits",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_CentralProcessorChannels_Name",
                table: "CentralProcessorChannels",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CentralProcessorUnits_CentralProcessorChannels_ChannelID",
                table: "CentralProcessorUnits",
                column: "ChannelID",
                principalTable: "CentralProcessorChannels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotherboardUnits_CentralProcessorChannels_ChannelID",
                table: "MotherboardUnits",
                column: "ChannelID",
                principalTable: "CentralProcessorChannels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
