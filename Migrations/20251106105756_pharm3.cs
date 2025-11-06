using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class pharm3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_ApplicationUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Custormer_Allergies_Active_Ingredients_Active_IngredientID",
                table: "Custormer_Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Custormer_Allergies_Customers_CustomerID",
                table: "Custormer_Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Ingredients_Active_Ingredients_Active_IngredientID",
                table: "Medication_Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Ingredients_Medications_MedicationID",
                table: "Medication_Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_DosageForms_DosageFormID",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Suppliers_SupplierID",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Medications_MedicationID",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderID",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_ScriptLines_ScriptLineID",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_Pharmacists_PharmacistID",
                table: "Pharmacies");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                table: "Pharmacists");

            migrationBuilder.DropForeignKey(
                name: "FK_PharmacyManagers_AspNetUsers_ApplicationUserId",
                table: "PharmacyManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_AspNetUsers_ApplicationUserId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_PresScriptLines_Medications_MedicationID",
                table: "PresScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_PresScriptLines_Prescriptions_PrescriptionID",
                table: "PresScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptLines_Medications_MedicationID",
                table: "ScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptLines_Prescriptions_PrescriptionID",
                table: "ScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderDetails_Medications_MedicationID",
                table: "StockOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderDetails_StockOrders_StockOrderID",
                table: "StockOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrders_Suppliers_SupplierID",
                table: "StockOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07dcdaaa-5f31-4351-8de5-326763efa748", "AQAAAAIAAYagAAAAECTlDBKZrvTikQqbxPZGM76Kh7ei93WWecLMYOO/R+ilGh6SVH18ynK6biDU2Rh5mg==", "b93a5a6d-13df-4bc1-9812-44ddaff95daf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "108d5729-c7ce-4f72-b9e3-799bdc2a31fe", "AQAAAAIAAYagAAAAECwyT0RtQs8gF4PSkb+qksTg+4A8L3MwLmB9qJl42U7l9K23fj6yA079zH78xPx2ag==", "3826a94b-1f6c-4755-b3e6-aa19757fc000" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20db1c6a-bafc-4ecf-b912-022599cad4b5", "AQAAAAIAAYagAAAAEGHh1fzSTd1PaJTmtC3GGzRYDw3s+64a+iVw1TrTaiZJOgwReZ7X7aa3xxHo3aSGwg==", "113c33da-6099-4989-a296-f3a10cc518a5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9696c3b9-1b0c-4901-aa06-6bdac6ee2681", "AQAAAAIAAYagAAAAEKI8SrwmgCPSfOfUp9Y+QoyHei0IcdPV2N2zCIFboJgbs+Y8xvG0kWjXpov/Jo6CDQ==", "23eacc39-20a7-49d1-9226-f19041e70f02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-13",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5687c666-5223-4b10-a7a5-81b0aa38c9a2", "AQAAAAIAAYagAAAAEDnsC3xfOaMoIawiX5qK+BAsTklq4Ho12S6N0Xr9niU11c31MCYoqJcwQRcjrDp9aQ==", "b690dbd0-037c-47a7-a1a9-0a1c84c9b413" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-14",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55f9b0cf-932d-424b-97a2-270e83e4e22f", "AQAAAAIAAYagAAAAEIPK+QgoH7n7x3tFFhRtJwT6TqbDfyKA+optGrmW6m2lKEr43A4wDJuim58z/q7KNg==", "03f221c9-3b64-4f90-af69-e913856a14a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfd59ffa-7843-49d7-9fed-8104000776c2", "AQAAAAIAAYagAAAAEN2fkGlRVw2pJInuA0ZpfMRK5qy8qFXUopTSrbZffyG0WkJCXl6rK/hwEvupC/sWcQ==", "ded3f27c-6910-46fa-9e04-fc3ca1e1a4f5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-16",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a9ed2bf-e2c5-4fc0-9147-ce0d4c2d2f98", "AQAAAAIAAYagAAAAEAIbar0wGJVuEo0c01itveLOqbB9q6Pl7UB3a+GyIGfebyxw0XWacsixwEEId5Ov2Q==", "f07638b3-9908-47e9-b951-582eeda0df50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-17",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "432094ed-1d89-48ae-bd03-803e8745aa25", "AQAAAAIAAYagAAAAEKMm3v9CZI2cHiYd9gmGgz7pH2Wa4GMczs3DRt0yXliohvJ5LvNJbLImEHHf9rqwGg==", "2f2cb0a2-bb92-4f77-8d02-0a0a2db31e23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fc32eb4-75d8-413d-9df3-19251a0bfd03", "AQAAAAIAAYagAAAAEEAUo7zvE07zVbOrNTSPHKpoHd9WUL8OG31xJ7EtU6HLSHzFx9Xo7j6U0oZ8EUnJmA==", "6137134c-f3b0-4a6d-9c02-6a49bd6f6b24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-19",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "475d4354-ad46-4704-a407-c3aafa0e586e", "AQAAAAIAAYagAAAAEPYrSBTNgUFsJtB5GIqRAcREftay21RmDtwnKanKKeBto/d6HKELYgrxFd7GwqV1NA==", "adfcbcf8-0194-44c7-9e27-183e61ed2ca2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "853b810e-e017-4ac4-b256-7bb01430c418", "AQAAAAIAAYagAAAAEBFo4GWnRVYxTresfmwzpfAUswJUzPc0jDvEnqdKKXEsLmbZjFkuzYlOrpKdfRYG4w==", "c4c25753-281a-4fc3-8692-35d3c70e74d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-20",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bc4933d-ace6-4ab8-9390-a1f2975a760a", "AQAAAAIAAYagAAAAEMlz+MSJ1Jogfy9LEYZ0FM6mnUA5qozd8US2QVNbGdrXNCNh0uV/rjG3r7GYh5t5EQ==", "1d38841b-34b0-4a4f-ac78-7ddd272d1829" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26962d93-23bf-45c1-ae0a-ed6b21055293", "AQAAAAIAAYagAAAAEBTmYw/oTEufTlLTXkA538luepM9J3L+Zapf/1kL5myBsdKnRpOcknjmn18Y16glPQ==", "5ac8722e-7bae-4eb0-8fdc-ec7b3f158d05" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b84aa9b-93b2-46a6-a158-13a362874ef4", "AQAAAAIAAYagAAAAEJKE73BKzlApjWDzY/tlV6QIOlCtdlFss1xKnsrpruH+Vw0URg3PEY1otzzdQ/VZGQ==", "88d7e0b7-5344-43ae-9087-392202db4cde" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62131394-9535-403d-9723-600ed0f05841", "AQAAAAIAAYagAAAAEMQRkS5ObCSQS8MMOs0vDJEc6C17ke++iZvaGPYAPpqcjFInNgXb4CscbMmcnNBnVw==", "b29518b5-17b8-4ffb-aef4-3bc57cdd9f5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dadf3cc6-b0ad-40bb-b519-121670b815ea", "AQAAAAIAAYagAAAAEFlMvLazWhj3MZRkgoKc3qQAffHrQw1jYdrsnYvpa+RNLpmv8bJK8vuqSLfkmCxsMA==", "96465aea-3537-4a98-bf22-407fbbd89f35" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71a1dde9-4800-4b02-a126-d52fdec0d2f0", "AQAAAAIAAYagAAAAENnWo+AKgnaEbyKL7BVmsdgLegnhMf63utOBRmimVA1g3/fHxMcojlO1CqnooGRRJQ==", "6d151f9d-218d-42aa-a173-fc8c339264ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06aeaca9-8bd6-4f64-99ff-dea46b4602b7", "AQAAAAIAAYagAAAAEFAKlpCM8JtVYaUzsShIWttNKxYD2ZZWl/XzkyZ+lQeBgd/JrSS52i6pc8mqN7/0Ig==", "3f831d9f-b2e7-4b51-9745-5f8ad3982d65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a850438-eeff-4345-9eae-1c3bd9c88e1d", "AQAAAAIAAYagAAAAEMyDy3CdnDmFTTWFoTCOgIteLdwV2n8tNtcRin9tTfwNNbhI14yBUDvDtPrnPijzwQ==", "eef31eee-0ae9-488d-90f5-17dfbb49986b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7985a0b-13c2-451e-a6a8-6756e1919137", "AQAAAAIAAYagAAAAEDIuz9cM5UG1UQLfJSmfr5WniJoM1oucWZSEfOY35itrJOdk75DJdkd2voEpVzpQAw==", "852dc65b-7aaa-4811-92a5-5222b801c05b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1a6048a-90ae-42bf-bffb-810cc868eb32", "AQAAAAIAAYagAAAAEMYK+miUBWiA7god88bYXEd0yrpidGipb/La5B+XYgXCct9x2aasXWeAI2tD7RTZWw==", "c8b29617-8c7e-43b9-b1fe-a6c8b4c684cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b24ae0e9-e0c1-4a30-b8ce-2bea27ecad49", "AQAAAAIAAYagAAAAEDucA+/p5QHl/mEC9/MiSa4efvEXDIW6Gcch7am6nfrlYE3Gr+ltB/Mqel/F7c63iQ==", "9726c6c0-5917-47a7-9064-48d822b4cf70" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71c98e3d-f116-4f88-ae8b-0e7e3baa645e", "AQAAAAIAAYagAAAAELPXJNTe7haKOfDXC37NhvCTxJd27des6rQMXtn4hIfcr0WzhYd+GJyTKOx+46gLJQ==", "a729fd59-d9a3-424b-b4ff-6d415ecd3f42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "312755fd-ade8-4bf8-9ab4-069bb4936f44", "AQAAAAIAAYagAAAAEHTVhaD5yjlcUVkV76Ht3tgZwPFsytwQs/G/E9GJIOMeZQxQkXv7159CriSzhivRrA==", "145dc039-2e41-4bd7-9f92-b5a057e84677" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Custormer_Allergies_Active_Ingredients_Active_IngredientID",
                table: "Custormer_Allergies",
                column: "Active_IngredientID",
                principalTable: "Active_Ingredients",
                principalColumn: "Active_IngredientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Custormer_Allergies_Customers_CustomerID",
                table: "Custormer_Allergies",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustormerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Ingredients_Active_Ingredients_Active_IngredientID",
                table: "Medication_Ingredients",
                column: "Active_IngredientID",
                principalTable: "Active_Ingredients",
                principalColumn: "Active_IngredientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Ingredients_Medications_MedicationID",
                table: "Medication_Ingredients",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_DosageForms_DosageFormID",
                table: "Medications",
                column: "DosageFormID",
                principalTable: "DosageForms",
                principalColumn: "DosageFormID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Suppliers_SupplierID",
                table: "Medications",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Medications_MedicationID",
                table: "OrderLines",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderID",
                table: "OrderLines",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_ScriptLines_ScriptLineID",
                table: "OrderLines",
                column: "ScriptLineID",
                principalTable: "ScriptLines",
                principalColumn: "ScriptLineID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustormerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacies_Pharmacists_PharmacistID",
                table: "Pharmacies",
                column: "PharmacistID",
                principalTable: "Pharmacists",
                principalColumn: "PharmacistID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                table: "Pharmacists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacyManagers_AspNetUsers_ApplicationUserId",
                table: "PharmacyManagers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_AspNetUsers_ApplicationUserId",
                table: "Prescriptions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PresScriptLines_Medications_MedicationID",
                table: "PresScriptLines",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PresScriptLines_Prescriptions_PrescriptionID",
                table: "PresScriptLines",
                column: "PrescriptionID",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptLines_Medications_MedicationID",
                table: "ScriptLines",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptLines_Prescriptions_PrescriptionID",
                table: "ScriptLines",
                column: "PrescriptionID",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderDetails_Medications_MedicationID",
                table: "StockOrderDetails",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderDetails_StockOrders_StockOrderID",
                table: "StockOrderDetails",
                column: "StockOrderID",
                principalTable: "StockOrders",
                principalColumn: "StockOrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrders_Suppliers_SupplierID",
                table: "StockOrders",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_ApplicationUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Custormer_Allergies_Active_Ingredients_Active_IngredientID",
                table: "Custormer_Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Custormer_Allergies_Customers_CustomerID",
                table: "Custormer_Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Ingredients_Active_Ingredients_Active_IngredientID",
                table: "Medication_Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Ingredients_Medications_MedicationID",
                table: "Medication_Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_DosageForms_DosageFormID",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Suppliers_SupplierID",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Medications_MedicationID",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderID",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_ScriptLines_ScriptLineID",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_Pharmacists_PharmacistID",
                table: "Pharmacies");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                table: "Pharmacists");

            migrationBuilder.DropForeignKey(
                name: "FK_PharmacyManagers_AspNetUsers_ApplicationUserId",
                table: "PharmacyManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_AspNetUsers_ApplicationUserId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_PresScriptLines_Medications_MedicationID",
                table: "PresScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_PresScriptLines_Prescriptions_PrescriptionID",
                table: "PresScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptLines_Medications_MedicationID",
                table: "ScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptLines_Prescriptions_PrescriptionID",
                table: "ScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderDetails_Medications_MedicationID",
                table: "StockOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderDetails_StockOrders_StockOrderID",
                table: "StockOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrders_Suppliers_SupplierID",
                table: "StockOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59e60a3e-54fc-43af-8663-198dfa03083a", "AQAAAAIAAYagAAAAEJjEk/mBs+frdn8gChQddUTm2EEdzMxXQ5Uj1/N9F6C2O8Wt9ZPKog+ZPNnt1+iI3Q==", "9145482e-6d76-4e32-b4a8-ab51dd1ee81b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96a64426-38f3-4b80-ab2b-11831ee10ccd", "AQAAAAIAAYagAAAAEOrLW6xC3CwU9qOciEQ5Sy0jmstnLbvBQ+TETdIuKkcxQybEmPq/oi1THeC0NYnwsA==", "c22617ff-c14d-4d74-875a-d27f721659f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "087f23f3-b213-4c18-96d3-816e3c0d0d9c", "AQAAAAIAAYagAAAAEHL3dHbzO2Plo++xMla7/W5QyYArNiJ8QLf2zCbRMEt5FD3fR6kr2n2egKGdAaX0Ow==", "3d8ea441-bb62-4b22-96b0-f76097941371" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6f851bd-7416-4841-a6a3-4ab6e6cc0402", "AQAAAAIAAYagAAAAEDiOvNgySk6DmKTmY/NVFejIePGf1E2pqlVb+FjLsg+hDPGc04yMTOvHjnNYL5Rwxg==", "2a29c1df-8321-41d7-ad92-9b89109d7ee2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-13",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02a5a56e-b7c5-454d-8555-681250f2d01a", "AQAAAAIAAYagAAAAELLzT0iJ1fGvQzaLdbAPo12r1L6dYHhc7f9fZtImbGAQZG3XtBhEuwLahXfQdOOFag==", "624d46e5-93c5-41c9-bd4c-0ad54554c563" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-14",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d547c1aa-b942-4923-83c6-9cf6083bdd27", "AQAAAAIAAYagAAAAEHm6uKGhxAgvs5wxtaG3zkkINnQQJuVYPREOXHvmp5tG9IBXoUFCfxogcD4FV32E/g==", "10326425-bc9c-4c6d-b7fe-735d2a1090d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88e386ce-8c8c-4191-843f-51562b4c426a", "AQAAAAIAAYagAAAAEOj4iCabR2sAyt3eb06dajr0gFNuEXLOUwDJ7CXIaIhI1r8y+VfC4Fr4/sAPBqGxTA==", "49e45ade-3a26-48fb-9fcf-e57578e16cb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-16",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21dd6282-631a-44b9-b807-c8522852a91d", "AQAAAAIAAYagAAAAEOn0ySvoXTWwENwLuTOAtFS7/xzozZhAiZ1yGRPNYV9CLH+fR9LRh20mDQnAlaS9LA==", "a9cc1298-f5ec-49e8-8965-6ca3194c2227" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-17",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "987a2f89-4a6a-47df-8f9e-3d951f44c6f0", "AQAAAAIAAYagAAAAENQ5piZnow8drAvom5SE4VxtvmaSRqHIK0nAuc21f+0wS9uBeZXO7r8CXDIrmBUgtw==", "3d3c0014-bc59-47e6-bc42-013f4c8c0292" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b0e431b-7b09-4f54-8486-28a1dc35dd93", "AQAAAAIAAYagAAAAEEvW+Enp4lJ5RuxjJZcTOX3olcsiDkJnxOzbtHvn3WdLGt3kp5J11gSHcw7o7DjIvA==", "5b8a605f-c09b-4fd6-9774-86e6d0fd7dc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-19",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3b1aff9-c19f-476f-a706-22fdcf431e4e", "AQAAAAIAAYagAAAAELpuEAF1nzFQ0m4/4Mpy3xopq+709AwctJqA4Pwj6htryMRULQBHrIX7P1RjT0jFng==", "69645c0f-99d0-4fee-ad7b-4319230bf61a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abfe19cc-dc7a-4af1-af38-e9be95411c0c", "AQAAAAIAAYagAAAAEGQbeIgSgrSGk80nDGmgyAktmTZMS8ScibcCsUiGI7Kr6zE32ktkPMvcorHLUE8Prg==", "37e388a8-d6f6-4903-a803-463a06fa3b0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-20",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9854cc1f-859f-42a9-92cf-ed1da226ee98", "AQAAAAIAAYagAAAAEH2nRPQMEcw3ribupibpbiobfUblag67YTCGX9qd4f/djo+KNkhtTyK6QEwYgAS/Aw==", "b289d4a6-99b5-4af4-8404-1d65b9216ba1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4c5cf4e-942e-420c-9fee-5e7eff731ffc", "AQAAAAIAAYagAAAAEGH1KKjPu21QLgQ2Dm2DRDvA1928d0KfPXEUjShx/rt80PqYkA7yPJnt2BcSzS99HQ==", "8e5fd49b-3383-437b-b8e1-b46d6eb79d18" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4c13e90-b76a-43fd-aa0c-9833db16a57f", "AQAAAAIAAYagAAAAEIml9dtCDLoorDwIm/ufFVb5qvcsk3SdzPJikABXEmohCza6qzUMe69kp1yfg/0HxA==", "5633eb67-0c1e-42fa-8566-d62b6c59135c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5fc0b87-40a4-43ef-8e28-6dafae63121c", "AQAAAAIAAYagAAAAECNHbHpH8Nia9Y5V03cTfk3A9GPIsSmCLV0JZPb3M48mwlIMXjZSofXqpPSQ+GJZlA==", "5146d365-cbc2-4100-9bba-8e8f2bd66fdb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8d91338-73ae-4f8c-bf53-5fc941383a99", "AQAAAAIAAYagAAAAEDmncBeBVe6X2XSti5YBPhFTOc5sIKow+dRjcRxzo7LzPuLHoP5kZjfXD8cFhNOWig==", "3113a7cf-242e-4380-b630-d018b74d05a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e13370b2-7eef-4bfd-9c11-8eb7abc448ff", "AQAAAAIAAYagAAAAEKfvOw9cE26Y8hKKH2jnarP2OephthTy6LwsCyt9btcw4CYWGPRsbD8/QPtpOmbdMw==", "d9c5ba74-b3e5-458c-9365-a8214b7bcd4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8668572f-72a4-4d46-a9f8-3180fb9625b9", "AQAAAAIAAYagAAAAEMlNox8yUBitrGHhVeZH9gjJ5ccPkUuO9got1COvWXwB6X9U5EQB9WbBPNDJ6XGbLg==", "80d11750-6d02-4be5-9e3f-d24d01c80678" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "298bd39a-a358-4d37-a908-bc72901e1f15", "AQAAAAIAAYagAAAAEEzLUS/Qgf4TBxTA1lDUtn2ttIKeC7CKo8Tv1znVw6Y/NRCpB4C03BvGjs5YKmZt1Q==", "f24cd8e6-a93d-450c-8890-6a7c13f8b69c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "139ce1b0-3263-4c20-8231-de9f0cc9163a", "AQAAAAIAAYagAAAAEN8IDNlS5bxR5mN2vDWaGJyylaqSZay4K79brQJdLAwucCaTFQ/VmXgFvQSiaf3chA==", "6edea1bd-7398-407f-9271-4b5a0fc673e5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c48b3560-ddad-49eb-a7ad-d6022e062379", "AQAAAAIAAYagAAAAEAmWassDByjgaciVhfVcRKin3Us1Tkt12xv/bnTC8Qkpqaxgljjad1LXGhrNRJzH7g==", "81894cab-ce6c-4996-a081-5abe223cca6f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52ff982d-351c-449b-ab07-dd91305c0685", "AQAAAAIAAYagAAAAEJLTf4iYsWHfV3/x2fFXxdPp9JWM4+jxCZo2qRqSQrNUfyPmxPf+DDdccxkqikf6AA==", "bc99ecf8-164d-4cdf-961d-ada76ae993cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f5eef6f-a112-49ac-830c-c9ee5bab5f9d", "AQAAAAIAAYagAAAAEPIgZKZWWY78GzTTVWDSKWskvTPZapqptbQq0FDziyD5XkAIsRpO4ANtdEQ396e4ZA==", "17f7308d-7ab0-42d4-806c-f39c73665f7e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dddae62-4897-4a4f-bfcc-ec0f0655f996", "AQAAAAIAAYagAAAAEMM7yRyyq7Xl73+PzjV8Piq8VhS+x/HBN8XCviHplTLpCBvzZtUBoP6U5XxAGv/mAQ==", "6a6abf7b-8300-4912-8801-c4eeed9d5bb2" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Custormer_Allergies_Active_Ingredients_Active_IngredientID",
                table: "Custormer_Allergies",
                column: "Active_IngredientID",
                principalTable: "Active_Ingredients",
                principalColumn: "Active_IngredientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Custormer_Allergies_Customers_CustomerID",
                table: "Custormer_Allergies",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustormerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Ingredients_Active_Ingredients_Active_IngredientID",
                table: "Medication_Ingredients",
                column: "Active_IngredientID",
                principalTable: "Active_Ingredients",
                principalColumn: "Active_IngredientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Ingredients_Medications_MedicationID",
                table: "Medication_Ingredients",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_DosageForms_DosageFormID",
                table: "Medications",
                column: "DosageFormID",
                principalTable: "DosageForms",
                principalColumn: "DosageFormID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Suppliers_SupplierID",
                table: "Medications",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Medications_MedicationID",
                table: "OrderLines",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderID",
                table: "OrderLines",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_ScriptLines_ScriptLineID",
                table: "OrderLines",
                column: "ScriptLineID",
                principalTable: "ScriptLines",
                principalColumn: "ScriptLineID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustormerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacies_Pharmacists_PharmacistID",
                table: "Pharmacies",
                column: "PharmacistID",
                principalTable: "Pharmacists",
                principalColumn: "PharmacistID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                table: "Pharmacists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacyManagers_AspNetUsers_ApplicationUserId",
                table: "PharmacyManagers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_AspNetUsers_ApplicationUserId",
                table: "Prescriptions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PresScriptLines_Medications_MedicationID",
                table: "PresScriptLines",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PresScriptLines_Prescriptions_PrescriptionID",
                table: "PresScriptLines",
                column: "PrescriptionID",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptLines_Medications_MedicationID",
                table: "ScriptLines",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptLines_Prescriptions_PrescriptionID",
                table: "ScriptLines",
                column: "PrescriptionID",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderDetails_Medications_MedicationID",
                table: "StockOrderDetails",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedcationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderDetails_StockOrders_StockOrderID",
                table: "StockOrderDetails",
                column: "StockOrderID",
                principalTable: "StockOrders",
                principalColumn: "StockOrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrders_Suppliers_SupplierID",
                table: "StockOrders",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
