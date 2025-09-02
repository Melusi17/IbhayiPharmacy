using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbhayiPharmacy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

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

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PharmacyManagers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PharmacyManagers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Pharmacists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "CellphoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyManagers_ApplicationUserId",
                table: "PharmacyManagers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_ApplicationUserId",
                table: "Pharmacists",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                table: "Pharmacists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacyManagers_AspNetUsers_ApplicationUserId",
                table: "PharmacyManagers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_ApplicationUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                table: "Pharmacists");

            migrationBuilder.DropForeignKey(
                name: "FK_PharmacyManagers_AspNetUsers_ApplicationUserId",
                table: "PharmacyManagers");

            migrationBuilder.DropIndex(
                name: "IX_PharmacyManagers_ApplicationUserId",
                table: "PharmacyManagers");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacists_ApplicationUserId",
                table: "Pharmacists");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PharmacyManagers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CellphoneNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PharmacyManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pharmacists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellphoneNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

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
    }
}
