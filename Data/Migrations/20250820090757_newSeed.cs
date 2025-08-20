using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbhayiPharmacy.Data.Migrations
{
    /// <inheritdoc />
    public partial class newSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustormerID", "Allergy", "UserId" },
                values: new object[,]
                {
                    { 1, "None", 6 },
                    { 2, "Pollen", 7 },
                    { 3, "Dust", 8 }
                });

            migrationBuilder.InsertData(
                table: "Custormer_Allergies",
                columns: new[] { "Custormer_AllergyID", "Active_IngredientID", "CustormerID" },
                values: new object[,]
                {
                    { 1, 4, 2 },
                    { 2, 7, 3 }
                });

            migrationBuilder.InsertData(
                table: "Pharmacists",
                columns: new[] { "PharmacistID", "HealthCouncilRegNo", "UserId" },
                values: new object[,]
                {
                    { 1, "123456", 1 },
                    { 2, "234567", 2 },
                    { 3, "345678", 3 },
                    { 4, "190406", 4 }
                });

            migrationBuilder.InsertData(
                table: "PharmacyManagers",
                columns: new[] { "PharmacyManagerID", "HealthCouncilRegNo", "UserId" },
                values: new object[] { 1, "134679", 5 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CellphoneNumber", "Email", "IDNumber", "Name", "Password", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, 612345678, "lindile@example.com", 123456, "Lindile", "password1", "Pharmacist", "Hadebe" },
                    { 2, 622345678, "dorothy@example.com", 234567, "Lindile Dorothy", "password2", "Pharmacist", "Daniels" },
                    { 3, 632345678, "marcel@example.com", 345678, "Marcel", "password3", "Pharmacist", "Van Niekerk" },
                    { 4, 721234567, "nicky.mostert@mandela.ac.za", 190406, "Nicky", "password4", "Pharmacist", "Mostert" },
                    { 5, 123456789, "Pharmacy Manager Group Member E-mail", 134679, "Pharmacy Manager Group Member Name", "managerpass", "PharmacyManager", "Pharmacy Manager Group Member Surname" },
                    { 6, 831234567, "john@example.com", 500101, "John", "customer1", "Customer", "Doe" },
                    { 7, 841234567, "jane@example.com", 600202, "Jane", "customer2", "Customer", "Smith" },
                    { 8, 851234567, "bob@example.com", 700303, "Bob", "customer3", "Customer", "Johnson" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustormerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustormerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustormerID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Custormer_Allergies",
                keyColumn: "Custormer_AllergyID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Custormer_Allergies",
                keyColumn: "Custormer_AllergyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pharmacists",
                keyColumn: "PharmacistID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pharmacists",
                keyColumn: "PharmacistID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pharmacists",
                keyColumn: "PharmacistID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pharmacists",
                keyColumn: "PharmacistID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PharmacyManagers",
                keyColumn: "PharmacyManagerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8);
        }
    }
}
