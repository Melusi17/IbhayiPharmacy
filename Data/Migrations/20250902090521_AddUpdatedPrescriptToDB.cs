using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbhayiPharmacy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedPrescriptToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PharmacistID",
                table: "Prescriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PharmacistID",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
