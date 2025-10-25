using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class add2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmacyManagerID",
                table: "Pharmacies");

            migrationBuilder.AddColumn<int>(
                name: "MedicationID",
                table: "StockOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "StockOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_PharmacistID",
                table: "Pharmacies",
                column: "PharmacistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacies_Pharmacists_PharmacistID",
                table: "Pharmacies",
                column: "PharmacistID",
                principalTable: "Pharmacists",
                principalColumn: "PharmacistID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_Pharmacists_PharmacistID",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_PharmacistID",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "MedicationID",
                table: "StockOrders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "StockOrders");

            migrationBuilder.AddColumn<int>(
                name: "PharmacyManagerID",
                table: "Pharmacies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
