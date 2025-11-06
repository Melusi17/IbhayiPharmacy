using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class pharm : Migration
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94c99fbe-8d89-475e-945b-69ad61f66f04", "AQAAAAIAAYagAAAAEJQjHTfCFwslCLeDwrr0/flQFbFG1VZSsyuo/yJRPBrPQk6vHlDANx6gHY08P828pA==", "17e86267-d1ee-4405-803e-4d6e0bad62ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "929b016f-10bb-4ee4-97ba-9809ed0ec53d", "AQAAAAIAAYagAAAAEND8cPaRGN7phfUkbPU7mHmqarALwc5Ma1YuRxpLd0x4/GLJqdg17c9kdS4IMcQXnw==", "2f4c5e87-710a-493e-a4bd-b8992c0dcbaf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-11",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b12a0ded-0e90-49c4-815e-90feac0aecee", "AQAAAAIAAYagAAAAECkXkkG/RcLk5dqESH9yjstKAN/mpYggrrGlZUKxKF26ncYF+gsJ8PNYRNweYBWo1A==", "f21f542e-355a-4e1c-af84-2f60b44d71f5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea7d506f-fcb8-448f-8648-19ce45ac964b", "AQAAAAIAAYagAAAAEPK8Rc+B2CwVf4KnX4ds60Ug5cvKDvqaNXSJoY3q4jmNFw1lyXc4f/YKsIdMkY+cPg==", "49fd8f70-1d97-4b17-938b-0ae3b17c86db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-13",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e978c259-216d-41e4-a99e-de7d36e8361d", "AQAAAAIAAYagAAAAEHzEEb2hd/W9PRNSLqbQFlA6bnfd6LgepwNotqDkHjM6KeZBAlaSP9GNbesNoy9L1Q==", "204021e3-6ccd-493f-83f5-845264a42e5b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-14",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a7a0d84-12cb-42c3-aa96-787e58ec05c8", "AQAAAAIAAYagAAAAEOe0+5uyTQB/vLg5xxY4E/R96HyUiWEKaHzvJ4YReYCp6CQ5OuBwG0bfXuVM/UDv+w==", "80cde0c7-e4a5-4697-b53e-35afbf70d17a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b80facf0-3e8b-4430-b87e-6a06d64406ac", "AQAAAAIAAYagAAAAEF/E2GgkXWghtOrZHErM0jsc1WD0l3Df1z50XeobAap0KusL+MQq+o5SOENey0RfaA==", "71c1bdec-6972-435b-88a6-036020ccacbe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-16",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1f03dfd-38e9-48d5-9d41-c424ed27d051", "AQAAAAIAAYagAAAAEBblJx/TxToBl1VpmU8x+iiCSlVrCEltoU7P4EoVH6wV7L/v8wVdOZdAAcvVxCUmZA==", "93181532-c3ab-4b89-8cc7-524e47643680" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-17",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc7494a3-e57a-4067-a580-03e89616561b", "AQAAAAIAAYagAAAAEDWpvE6PTZXtylgSaO9dbKWuqrt9F4My+AeGoaw1LNWG2slNLLtXs9wpXN4WxOnxug==", "1d54dc9f-d9a3-4607-97a7-ef7a32518c13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3976d562-05fd-4d30-a8d1-a0f0d5dde234", "AQAAAAIAAYagAAAAENHETxwm17R1a/i3X/P+Vv3mJ1j4YnAZTnUGZH3idi3bhhfQY0T6wjhQW52GQDIKJw==", "3b89beb8-c2af-41b7-9bd3-d6c0766d8673" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-19",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "043c94ee-3da1-47fc-8334-309c10621d5d", "AQAAAAIAAYagAAAAEG+eL0rZqRAF0tddpk0bWclOTqj7j21SNqA80YTG/ciYCWQUqPrCBaPzMLxJMVootw==", "c17e4f6c-8af4-4d60-920d-42c9aaeca41c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec8ae957-bff8-442a-8f64-8d9473197fd7", "AQAAAAIAAYagAAAAEJAV2HsxrGKOFzTp0biOvD4sarAY+Je/TGZhbDYveMNven9c07i+ftteKxGLXdBKBw==", "c2e564bb-b629-40a2-ba74-62c1b3c8a9e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-20",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "438235d1-050b-4a2a-9a61-69518a0ff332", "AQAAAAIAAYagAAAAEAME1Yovr8qEkQloxXEe5ma3qxR5EhJACiHjSa2IxiFdOyUzEI3ZcrQ3lQG4zVoLZg==", "28c8906e-3d4b-4b81-8a04-986ebbcd4894" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fb1a01b-9008-4875-96dc-5ff1463cf399", "AQAAAAIAAYagAAAAEOwEJzi/R8Xz2eG9tLQ3y7Ub7U3Moa82AYGWYDN+c7qtihLx+9ce1X3M2wOrlIhgbw==", "a3cb3cec-e8b1-44cb-9a81-4b2dd8075ed6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0da4e439-9221-491f-9d5f-f0d8cce94b99", "AQAAAAIAAYagAAAAEHwsqxJfElzcK7Gb4Nb46yKbejX3vbcMmsvbEDAyp4hzbMETQlZ7Nn8n0dPuEBX6+A==", "19bd3af7-cdd1-4e3e-b693-9ef70441f448" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e30276ae-6913-40e4-bc7c-52269d4378e1", "AQAAAAIAAYagAAAAECHke7V83wKV2s9S3iM7TkWRh3kHkSpAjSROVpXjPbFTBQUreR6p/78UDNJOxqGXBg==", "58f02152-3ed0-4c50-8462-6fea267086d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "694d4f88-a868-4197-8a7b-b390e13f7596", "AQAAAAIAAYagAAAAEJXBQlfYorR1rn7rpcDW0bDOQBxyY/Qp3eLjU+p/nAbiIAeCj1m4EuXqpJhh6xmr1Q==", "d7a34aab-a1ba-43fe-a158-c5abd59bcdaf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fd609b7-fe1f-4c4d-a2d1-9fe83af26250", "AQAAAAIAAYagAAAAELtJv0RF4wj7HslquIyqBn+2E3Mdd2zAMU+o4TnC5RpywmLyNB1UcqPFWSs72faP0A==", "77a7653f-f9eb-478e-8ead-277375ae19e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5c4d228-33c2-474e-967d-1b494ca2b73a", "AQAAAAIAAYagAAAAEHHnVrA08RsjsFGlZmFv02JqgOIlqoslPNSE/KRrQxCOJ4z+3NxrpcOfX+5efuTE5w==", "1eefbdca-644a-4f5a-9f89-cc84f66e9a05" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer-9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62d6128f-adf4-48b5-8185-5ce7ce309c73", "AQAAAAIAAYagAAAAEOZ7bgCYzPr132ucrIa2tGO1R2E0c6BmYBd2Bqbi1al0qLBqPmVPBKViqK0MJekGlg==", "50fb8a17-b7a8-409d-a91e-9b9b65a53ce3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2a0ce87-5ae1-4660-8ddb-e5dc8af50e79", "AQAAAAIAAYagAAAAELsYFibqYD20XcL6FFtexOBQNDfQnU0H960gJGYjME/HLuMwfB7Lhxwbm5oOaO7ACA==", "648c81ce-2134-4504-a469-cf4087b91844" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "700df2ec-7980-481c-ad76-5452d17f2827", "AQAAAAIAAYagAAAAENDkgmZDhpmKf3+C9hjktAuYMe2J9UDuAv/7XF14kc7XB4d3HcLLF3U+Sfn2NvbBEg==", "c2fbf304-c05b-459d-99b7-eabf3c75fbaf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81fc76db-63be-4705-b60d-1f66a4b8a80b", "AQAAAAIAAYagAAAAEJ7QTHIJmX4ac9Wld9cr0Dea2TxgZp3VJMeAfuOh4eOWbTZrtGE94to9VxRXQNeTqQ==", "9941f066-6f2f-45e0-963c-8c93990a8960" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d17935c-bd7a-41f2-a16a-f1611e789c4a", "AQAAAAIAAYagAAAAEGUQSlMu51ABVonsepg0nLNu8gQ5tbeZxo6eoXxjoa8A195Sh3es3g2dSOliCdtyDg==", "61b1e2b5-1ff0-44aa-bb9e-fc0c2ba9ee45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pharmacist-4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f141b70-0ec4-45f2-9073-bd3e9fc4dd2e", "AQAAAAIAAYagAAAAEDnt37LvMM0Q9+cltmO+Dy+6qad4VdHkxYqHKiaB+ReB9RSSjasqnk+O8jHUmb3h1w==", "65b206db-32a8-483c-9bbc-752cce6aca1f" });

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
