using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class pharm2 : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_StockOrders_SupplierID",
                table: "StockOrders",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderDetails_StockOrderID",
                table: "StockOrderDetails",
                column: "StockOrderID");

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

            migrationBuilder.DropIndex(
                name: "IX_StockOrders_SupplierID",
                table: "StockOrders");

            migrationBuilder.DropIndex(
                name: "IX_StockOrderDetails_StockOrderID",
                table: "StockOrderDetails");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f64383ed-7e6e-42a5-b944-7649da60248b", "AQAAAAIAAYagAAAAEPuV+KSnZf8kyHWU6J/bqPSn1XWmmeVLVIpJueraXiLGisoES5/WCkF9tLB/Kb4BzA==", "2cb950fb-251d-429c-afe8-b138c641e49a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac360323-c8c7-4b86-8271-209ebea97324", "AQAAAAIAAYagAAAAEAoARadq/eEgbjeDx4iJrZ9Wv2LUKKZPKf5HfiR9/PMQTcC4oxdmIeVSouXiW1qGXQ==", "583d1919-a47c-462b-8e16-b5a3efb504d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02662efe-4040-4810-a897-577cf1594231", "AQAAAAIAAYagAAAAEEV9oHmfEpEF0pzX5WHOBPBwmkDCxMABjJf05n8KVlbYKwdaCqEQjhKhO7Sa+/5kLQ==", "e900d84d-4d2b-411c-b071-fadd1a541a76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "340ab432-7c55-410b-8aa7-8d2bdd9a62d8", "AQAAAAIAAYagAAAAELSb8otiklvFMIg1PeEaPIes2N1qWuMFDFsFmxXfTu7l838a5qcxobXqDakxAI2/wA==", "402e18bf-eeb6-4ba5-b4fa-0bd3990fc9b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-13",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cd7e9cb-4ca9-436d-aa00-901678eb0bdd", "AQAAAAIAAYagAAAAECjadXXHj55z5YlMIMgSjIbgClf8uzIGz22KlGTNcTU+4r5t+4gHHV/35L3Km9bTww==", "6828c6f4-c6b2-4378-972d-e67b86bd9959" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-14",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f08bd987-55cf-4a8f-bf74-efed30ea6b62", "AQAAAAIAAYagAAAAEAGQHG2Bnf25FMmd9G+/GSosI0xem3drswIoOsyqSYUlffNaLx8x1PRP1qiugnwF+g==", "a15a66ca-1af8-4bca-b466-cb4018a641f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54fffd45-c0dc-4d4d-81b3-90d59ad22d63", "AQAAAAIAAYagAAAAEJZvgkRMp6QFQdKP9JifKkClw5x+6TxK2sX3a8NjaC8Y9cYoWcoknRocXzUta0tKEg==", "ffffc5aa-521e-438e-a73f-7d637f829ed1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-16",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e7799fb-e270-44cc-b24e-8e0179a0c72d", "AQAAAAIAAYagAAAAEPcPiuYDBXxuSv0T70XSPQEWbrqQ4kHsKUZZnm1kb7B+fadOiJy2a8uQdhg/r4dD/g==", "3ee7ce92-387f-4cab-a49c-425bc4df0cd8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-17",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef3d7c64-601d-41ed-b4bb-4f6fd429e4b6", "AQAAAAIAAYagAAAAEJ/6qbW/kywOrpfi6nEYwdB8/y9i/PTbS18vhB+Br/SZ6NXa78QNKamxJo7hAWlK7w==", "8d2255d8-5c72-4379-a342-9b61225c3c70" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2d54e52-3287-42dd-a1eb-a25b4b155e1e", "AQAAAAIAAYagAAAAECrebCpLfbXQx59orexHset0q7lbtH6WzVeI5E8Zz53Sk6IOS2E8HK+77FEm5afbhA==", "78f72bc3-c8c0-438e-9f09-4fcb171d67fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-19",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "038eb75c-0c6e-4147-b292-fbe1f13bd1a0", "AQAAAAIAAYagAAAAEKgd1gRPBIhGVNhcga55n3kzdKnQG991NiStesbr5AC0xM5vVj+2kUHKoe6SjnWqlQ==", "66977943-c7a5-494b-ace7-f00ba254a718" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a09a9d4c-52d9-4151-9575-6900db77a393", "AQAAAAIAAYagAAAAEFZZ2d9CCCaWXXCCyJfNmMcvAQfsiI0Ki5pwUXI0vW8cIAnnrxejJ942jeVJ87Zhaw==", "e66fe002-72d3-4c4e-868b-b3ae47005eee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-20",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4712f07-a75d-4d2c-be07-64fdce43d117", "AQAAAAIAAYagAAAAEK6OUt8UcY53x/C4XHqLSBuTd6vLBhZo4ygQu7+gbvUwSRcoeV9ZOJL2+vIEHPC4cQ==", "9645625b-9700-4a24-bf0b-776c5f5c2c46" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a115456-da7f-4a78-9a84-2672786e899f", "AQAAAAIAAYagAAAAELf6ltays4NvLSdCjvE11qatf61b9XBkDv6XhniKinJoiB4ppiO90e6YYeEDSO6rIw==", "0632f64e-77c0-41a4-95a5-90a1ec820c94" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18108219-136d-452d-9daf-826751c7de88", "AQAAAAIAAYagAAAAEHmMueiHhyv5nsHYJGGFXGa3q6Z5rOlvwXvGDn/UjYGIsO9ceosIJI/uYAOcBSb/4A==", "9c5671c9-5445-4c12-a0ca-50e297b2945b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c86a1d5-d3d4-4df3-b205-09bb5b8d60b3", "AQAAAAIAAYagAAAAEL4jn1vOMoeS3fFRQ8Ck+ftUJ3xOw4wJ5y0Rp+UwK3KwDKJkNQmXYkwhnxDcaLNnMg==", "9c8617a9-3efb-411e-89b9-dc4069aa44af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb1f8a9e-1754-4a22-b20d-4ec29d460688", "AQAAAAIAAYagAAAAECfJ8ZJVh30gvbKItIxmx7DZWdw3WYNS1eD4XpvwKcMHC0CQU8i1yERa+X+bZDjK9A==", "6b27009b-515e-4d9a-847c-c2fd00202fb7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48216355-e52a-4fce-9b26-0e570f4ed4a3", "AQAAAAIAAYagAAAAEB5vNWznh3vtsZvLv6SiiLXi+ENRhXYwUYNe+dbxC84t0vakYe40FkUi2zPqFaAPKg==", "5c6c9d50-d4bd-4858-bf2e-799c235dbfd2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92c284ce-2c92-4ed2-b35a-72dedb3d1494", "AQAAAAIAAYagAAAAEF/hDzXw3X31GrXKfOYxP8GM9oEWHdQSORg1m2IwM6hLjAKMWM9NrttDwb99NW7yow==", "80c9fb45-9131-4a78-a2e9-514394da182e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a3f5cea-e5f6-42c4-b9b0-0e0c466fc3a2", "AQAAAAIAAYagAAAAEMUwsKvM4Nxicdi6IGcc6rklPYMuvxworlx2NrYTrs+TCYFQzE9HKFcq85PcXFsf0Q==", "d309e84c-b1ae-4370-a9ea-8458b999f120" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ad0e9e3-291b-47d3-ab90-450c9ba7b685", "AQAAAAIAAYagAAAAEB/7gA5hg+/sOEcer9lVwCL4DY0l7gXzTb1R7SoMwfw26UFWCBLFFJNsvCUjwXIQCQ==", "54865811-6fe3-4443-ae5e-1ed0deb6e8e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e0a9fec-2c53-4eca-aa6f-de87a1065959", "AQAAAAIAAYagAAAAEPBgO7rG0NTNLZtnSrkhviHw02q1eMgJDn30btIdeW6+tF0T7i/Pz6U70am9l0RxpQ==", "caf796c3-bf16-4e4b-9d29-bf9f42503eb7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "173c5c1a-7d07-48fc-90f6-7eddb2e76b20", "AQAAAAIAAYagAAAAEHjZFRzuTI67JItciOW6nQGK75yPx/epJJuIJv0P7tTsFt75gi+Wjx0hUl2NR0TefQ==", "4e36918d-6de6-4e95-94fe-65d249405c2e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34a23d87-7466-4a1a-b036-eeae042341e1", "AQAAAAIAAYagAAAAEFV409Bv222eqWqq+CMDIGYTR4a14E035eOaVZ0U8MdS9BnmWGCtRlNXTbzGSNRFdg==", "42fdb4db-c9d5-44e0-864a-478da73fcfcd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8048194a-d676-44b2-9776-654192a329f7", "AQAAAAIAAYagAAAAEMqXvOcpy/vowSNI+w/Y+PYwhgwpEP3Y9njhYU8FiyYJzXK5hEhsZuwnRlesJ+uI7w==", "fd5d13b1-7b6b-448c-ab83-cb06bddb5129" });

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
        }
    }
}
