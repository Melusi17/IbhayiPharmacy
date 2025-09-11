using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Custormer_Allergies");

            migrationBuilder.RenameColumn(
                name: "Allergy",
                table: "Customers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "CustormerID",
                table: "Customers",
                newName: "CustomerID");

            migrationBuilder.AddColumn<string>(
                name: "Cellphone",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Active_IngredientID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    AllergyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.AllergyId);
                });

            migrationBuilder.CreateTable(
                name: "Customer_Allergies",
                columns: table => new
                {
                    Customer_AllergyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AllergyId = table.Column<int>(type: "int", nullable: false),
                    Active_IngredientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Allergies", x => x.Customer_AllergyId);
                    table.ForeignKey(
                        name: "FK_Customer_Allergies_Allergies_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "Allergies",
                        principalColumn: "AllergyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_Allergies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Allergies_AllergyId",
                table: "Customer_Allergies",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Allergies_CustomerId",
                table: "Customer_Allergies",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer_Allergies");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropColumn(
                name: "Cellphone",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Active_IngredientID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Customers",
                newName: "Allergy");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Customers",
                newName: "CustormerID");

            migrationBuilder.CreateTable(
                name: "Custormer_Allergies",
                columns: table => new
                {
                    Custormer_AllergyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active_IngredientID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Custormer_Allergies", x => x.Custormer_AllergyID);
                });

            migrationBuilder.InsertData(
                table: "Custormer_Allergies",
                columns: new[] { "Custormer_AllergyID", "Active_IngredientID", "CustomerID" },
                values: new object[,]
                {
                    { 1, 4, 2 },
                    { 2, 7, 3 }
                });
        }
    }
}
