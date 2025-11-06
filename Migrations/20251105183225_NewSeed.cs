using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class NewSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Active_Ingredients",
                columns: table => new
                {
                    Active_IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Active_Ingredients", x => x.Active_IngredientID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellphoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active_IngredientID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DosageForms",
                columns: table => new
                {
                    DosageFormID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosageFormName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosageForms", x => x.DosageFormID);
                });

            migrationBuilder.CreateTable(
                name: "NewScripts",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Script = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DispenseUponApproval = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewScripts", x => x.PrescriptionID);
                });

            migrationBuilder.CreateTable(
                name: "StockOrders",
                columns: table => new
                {
                    StockOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrders", x => x.StockOrderID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "UnprocessedScripts",
                columns: table => new
                {
                    UnprocessedScriptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dispense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnprocessedScripts", x => x.UnprocessedScriptID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustormerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustormerID);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    PharmacistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HealthCouncilRegNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.PharmacistID);
                    table.ForeignKey(
                        name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyManagers",
                columns: table => new
                {
                    PharmacyManagerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HealthCouncilRegNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyManagers", x => x.PharmacyManagerID);
                    table.ForeignKey(
                        name: "FK_PharmacyManagers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    MedcationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosageFormID = table.Column<int>(type: "int", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPrice = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    ReOrderLevel = table.Column<int>(type: "int", nullable: false),
                    QuantityOnHand = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.MedcationID);
                    table.ForeignKey(
                        name: "FK_Medications_DosageForms_DosageFormID",
                        column: x => x.DosageFormID,
                        principalTable: "DosageForms",
                        principalColumn: "DosageFormID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medications_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Custormer_Allergies",
                columns: table => new
                {
                    Custormer_AllergyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Active_IngredientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Custormer_Allergies", x => x.Custormer_AllergyID);
                    table.ForeignKey(
                        name: "FK_Custormer_Allergies_Active_Ingredients_Active_IngredientID",
                        column: x => x.Active_IngredientID,
                        principalTable: "Active_Ingredients",
                        principalColumn: "Active_IngredientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Custormer_Allergies_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustormerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    PharmacyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacistID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthCouncilRegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.PharmacyID);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Pharmacists_PharmacistID",
                        column: x => x.PharmacistID,
                        principalTable: "Pharmacists",
                        principalColumn: "PharmacistID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medication_Ingredients",
                columns: table => new
                {
                    Medication_IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    Active_IngredientID = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication_Ingredients", x => x.Medication_IngredientID);
                    table.ForeignKey(
                        name: "FK_Medication_Ingredients_Active_Ingredients_Active_IngredientID",
                        column: x => x.Active_IngredientID,
                        principalTable: "Active_Ingredients",
                        principalColumn: "Active_IngredientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medication_Ingredients_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "MedcationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockOrderDetails",
                columns: table => new
                {
                    StockOrderDetail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockOrderID = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    OrderQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrderDetails", x => x.StockOrderDetail_ID);
                    table.ForeignKey(
                        name: "FK_StockOrderDetails_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "MedcationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthCouncilRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PharmacistID = table.Column<int>(type: "int", nullable: true),
                    DoctorID = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VAT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustormerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID");
                    table.ForeignKey(
                        name: "FK_Orders_Pharmacists_PharmacistID",
                        column: x => x.PharmacistID,
                        principalTable: "Pharmacists",
                        principalColumn: "PharmacistID");
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Script = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: true),
                    DispenseUponApproval = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID");
                });

            migrationBuilder.CreateTable(
                name: "PresScriptLines",
                columns: table => new
                {
                    ScriptLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionID = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DispenseStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repeats = table.Column<int>(type: "int", nullable: false),
                    RepeatsLeft = table.Column<int>(type: "int", nullable: false),
                    NewScriptPrescriptionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresScriptLines", x => x.ScriptLineID);
                    table.ForeignKey(
                        name: "FK_PresScriptLines_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "MedcationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PresScriptLines_NewScripts_NewScriptPrescriptionID",
                        column: x => x.NewScriptPrescriptionID,
                        principalTable: "NewScripts",
                        principalColumn: "PrescriptionID");
                    table.ForeignKey(
                        name: "FK_PresScriptLines_Prescriptions_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScriptLines",
                columns: table => new
                {
                    ScriptLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionID = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repeats = table.Column<int>(type: "int", nullable: false),
                    RepeatsLeft = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptLines", x => x.ScriptLineID);
                    table.ForeignKey(
                        name: "FK_ScriptLines_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "MedcationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScriptLines_Prescriptions_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    OrderLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    ScriptLineID = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.OrderLineID);
                    table.ForeignKey(
                        name: "FK_OrderLines_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "MedcationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_ScriptLines_ScriptLineID",
                        column: x => x.ScriptLineID,
                        principalTable: "ScriptLines",
                        principalColumn: "ScriptLineID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Active_Ingredients",
                columns: new[] { "Active_IngredientID", "Name" },
                values: new object[,]
                {
                    { 1, "Pylorazine" },
                    { 2, "Vaspril" },
                    { 3, "Zentropine" },
                    { 4, "Histarelin" },
                    { 5, "Lorvexamine" },
                    { 6, "Aterolazine" },
                    { 7, "Bronchomid" },
                    { 8, "Alveclear" },
                    { 9, "Epidraxol" },
                    { 10, "Cortizane" },
                    { 11, "Glycetrol" },
                    { 12, "Somnexil" },
                    { 13, "Calcitrine" },
                    { 14, "Phospholax" },
                    { 15, "Virocelin" },
                    { 16, "Immubrine" },
                    { 17, "Trosamine" },
                    { 18, "Velocidine" },
                    { 19, "Nexorin" },
                    { 20, "Zyphralex" },
                    { 21, "Cardionol" },
                    { 22, "Alveretol" },
                    { 23, "Xylogran" },
                    { 24, "Fematrix" },
                    { 25, "Plastorin" },
                    { 26, "Seralox" },
                    { 27, "Quantrel" },
                    { 28, "Myvetrin" },
                    { 29, "Draxolene" },
                    { 30, "Veltraxin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-customer", null, "Customer", "CUSTOMER" },
                    { "role-manager", null, "Pharmacy Manager", "PHARMACY MANAGER" },
                    { "role-pharmacist", null, "Pharmacist", "PHARMACIST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active_IngredientID", "CellphoneNumber", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IDNumber", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "customer-1", 0, null, "082 345 6789", "94c99fbe-8d89-475e-945b-69ad61f66f04", "ApplicationUser", "thabo.mokoena@example.com", true, "8805125123087", false, null, "Thabo", "THABO.MOKOENA@EXAMPLE.COM", "THABO.MOKOENA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJQjHTfCFwslCLeDwrr0/flQFbFG1VZSsyuo/yJRPBrPQk6vHlDANx6gHY08P828pA==", null, false, "17e86267-d1ee-4405-803e-4d6e0bad62ba", "Mokoena", false, "thabo.mokoena@example.com" },
                    { "customer-10", 0, null, "073 456 7890", "929b016f-10bb-4ee4-97ba-9809ed0ec53d", "ApplicationUser", "michelle.pretorius@example.com", true, "8508232023088", false, null, "Michelle", "MICHELLE.PRETORIUS@EXAMPLE.COM", "MICHELLE.PRETORIUS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEND8cPaRGN7phfUkbPU7mHmqarALwc5Ma1YuRxpLd0x4/GLJqdg17c9kdS4IMcQXnw==", null, false, "2f4c5e87-710a-493e-a4bd-b8992c0dcbaf", "Pretorius", false, "michelle.pretorius@example.com" },
                    { "customer-11", 0, null, "082 987 6543", "b12a0ded-0e90-49c4-815e-90feac0aecee", "ApplicationUser", "vusi.zulu@example.com", true, "8803115123089", false, null, "Vusi", "VUSI.ZULU@EXAMPLE.COM", "VUSI.ZULU@EXAMPLE.COM", "AQAAAAIAAYagAAAAECkXkkG/RcLk5dqESH9yjstKAN/mpYggrrGlZUKxKF26ncYF+gsJ8PNYRNweYBWo1A==", null, false, "f21f542e-355a-4e1c-af84-2f60b44d71f5", "Zulu", false, "vusi.zulu@example.com" },
                    { "customer-12", 0, null, "079 123 4567", "ea7d506f-fcb8-448f-8648-19ce45ac964b", "ApplicationUser", "aisha.jacobs@example.com", true, "9909020323082", false, null, "Aisha", "AISHA.JACOBS@EXAMPLE.COM", "AISHA.JACOBS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPK8Rc+B2CwVf4KnX4ds60Ug5cvKDvqaNXSJoY3q4jmNFw1lyXc4f/YKsIdMkY+cPg==", null, false, "49fd8f70-1d97-4b17-938b-0ae3b17c86db", "Jacobs", false, "aisha.jacobs@example.com" },
                    { "customer-13", 0, null, "074 567 8901", "e978c259-216d-41e4-a99e-de7d36e8361d", "ApplicationUser", "johan.deklerk@example.com", true, "8702054023087", false, null, "Johan", "JOHAN.DEKLERK@EXAMPLE.COM", "JOHAN.DEKLERK@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHzEEb2hd/W9PRNSLqbQFlA6bnfd6LgepwNotqDkHjM6KeZBAlaSP9GNbesNoy9L1Q==", null, false, "204021e3-6ccd-493f-83f5-845264a42e5b", "de Klerk", false, "johan.deklerk@example.com" },
                    { "customer-14", 0, null, "078 987 6543", "0a7a0d84-12cb-42c3-aa96-787e58ec05c8", "ApplicationUser", "thandiwe.sithole@example.com", true, "9306203123086", false, null, "Thandiwe", "THANDIWE.SITHOLE@EXAMPLE.COM", "THANDIWE.SITHOLE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOe0+5uyTQB/vLg5xxY4E/R96HyUiWEKaHzvJ4YReYCp6CQ5OuBwG0bfXuVM/UDv+w==", null, false, "80cde0c7-e4a5-4697-b53e-35afbf70d17a", "Sithole", false, "thandiwe.sithole@example.com" },
                    { "customer-15", 0, null, "071 345 6789", "b80facf0-3e8b-4430-b87e-6a06d64406ac", "ApplicationUser", "riaan.vw@example.com", true, "8108305023081", false, null, "Riaan", "RIAAN.VW@EXAMPLE.COM", "RIAAN.VW@EXAMPLE.COM", "AQAAAAIAAYagAAAAEF/E2GgkXWghtOrZHErM0jsc1WD0l3Df1z50XeobAap0KusL+MQq+o5SOENey0RfaA==", null, false, "71c1bdec-6972-435b-88a6-036020ccacbe", "van Wyk", false, "riaan.vw@example.com" },
                    { "customer-16", 0, null, "083 234 5678", "a1f03dfd-38e9-48d5-9d41-c424ed27d051", "ApplicationUser", "palesa.molefe@example.com", true, "9501151023084", false, null, "Palesa", "PALESA.MOLEFE@EXAMPLE.COM", "PALESA.MOLEFE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBblJx/TxToBl1VpmU8x+iiCSlVrCEltoU7P4EoVH6wV7L/v8wVdOZdAAcvVxCUmZA==", null, false, "93181532-c3ab-4b89-8cc7-524e47643680", "Molefe", false, "palesa.molefe@example.com" },
                    { "customer-17", 0, null, "072 987 1234", "fc7494a3-e57a-4067-a580-03e89616561b", "ApplicationUser", "kobus.smit@example.com", true, "7909094023083", false, null, "Kobus", "KOBUS.SMIT@EXAMPLE.COM", "KOBUS.SMIT@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDWpvE6PTZXtylgSaO9dbKWuqrt9F4My+AeGoaw1LNWG2slNLLtXs9wpXN4WxOnxug==", null, false, "1d54dc9f-d9a3-4607-97a7-ef7a32518c13", "Smit", false, "kobus.smit@example.com" },
                    { "customer-18", 0, null, "079 456 7890", "3976d562-05fd-4d30-a8d1-a0f0d5dde234", "ApplicationUser", "zanele.mthembu@example.com", true, "9604232123085", false, null, "Zanele", "ZANELE.MTHEMBU@EXAMPLE.COM", "ZANELE.MTHEMBU@EXAMPLE.COM", "AQAAAAIAAYagAAAAENHETxwm17R1a/i3X/P+Vv3mJ1j4YnAZTnUGZH3idi3bhhfQY0T6wjhQW52GQDIKJw==", null, false, "3b89beb8-c2af-41b7-9bd3-d6c0766d8673", "Mthembu", false, "zanele.mthembu@example.com" },
                    { "customer-19", 0, null, "074 123 4567", "043c94ee-3da1-47fc-8334-309c10621d5d", "ApplicationUser", "annelise@example.com", true, "8407125023080", false, null, "Annelise", "ANNELISE@EXAMPLE.COM", "ANNELISE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEG+eL0rZqRAF0tddpk0bWclOTqj7j21SNqA80YTG/ciYCWQUqPrCBaPzMLxJMVootw==", null, false, "c17e4f6c-8af4-4d60-920d-42c9aaeca41c", "Botha", false, "annelise@example.com" },
                    { "customer-2", 0, null, "083 567 8910", "ec8ae957-bff8-442a-8f64-8d9473197fd7", "ApplicationUser", "lerato.khumalo@example.com", true, "9503140321089", false, null, "Lerato", "LERATO.KHUMALO@EXAMPLE.COM", "LERATO.KHUMALO@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJAV2HsxrGKOFzTp0biOvD4sarAY+Je/TGZhbDYveMNven9c07i+ftteKxGLXdBKBw==", null, false, "c2e564bb-b629-40a2-ba74-62c1b3c8a9e9", "Khumalo", false, "lerato.khumalo@example.com" },
                    { "customer-20", 0, null, "081 567 8901", "438235d1-050b-4a2a-9a61-69518a0ff332", "ApplicationUser", "nomsa.ndlovu@example.com", true, "9703280123088", false, null, "Nomsa", "NOMSA.NDLOVU@EXAMPLE.COM", "NOMSA.NDLOVU@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAME1Yovr8qEkQloxXEe5ma3qxR5EhJACiHjSa2IxiFdOyUzEI3ZcrQ3lQG4zVoLZg==", null, false, "28c8906e-3d4b-4b81-8a04-986ebbcd4894", "Ndlovu", false, "nomsa.ndlovu@example.com" },
                    { "customer-3", 0, null, "084 123 4567", "9fb1a01b-9008-4875-96dc-5ff1463cf399", "ApplicationUser", "sipho.dlamini@example.com", true, "7902106123081", false, null, "Sipho", "SIPHO.DLAMINI@EXAMPLE.COM", "SIPHO.DLAMINI@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOwEJzi/R8Xz2eG9tLQ3y7Ub7U3Moa82AYGWYDN+c7qtihLx+9ce1X3M2wOrlIhgbw==", null, false, "a3cb3cec-e8b1-44cb-9a81-4b2dd8075ed6", "Dlamini", false, "sipho.dlamini@example.com" },
                    { "customer-4", 0, null, "081 234 9876", "0da4e439-9221-491f-9d5f-f0d8cce94b99", "ApplicationUser", "naledi.maseko@example.com", true, "9107280023085", false, null, "Naledi", "NALEDI.MASEKO@EXAMPLE.COM", "NALEDI.MASEKO@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHwsqxJfElzcK7Gb4Nb46yKbejX3vbcMmsvbEDAyp4hzbMETQlZ7Nn8n0dPuEBX6+A==", null, false, "19bd3af7-cdd1-4e3e-b693-9ef70441f448", "Maseko", false, "naledi.maseko@example.com" },
                    { "customer-5", 0, null, "072 345 1234", "e30276ae-6913-40e4-bc7c-52269d4378e1", "ApplicationUser", "pieter.vdm@example.com", true, "8306195023082", false, null, "Pieter", "PIETER.VDM@EXAMPLE.COM", "PIETER.VDM@EXAMPLE.COM", "AQAAAAIAAYagAAAAECHke7V83wKV2s9S3iM7TkWRh3kHkSpAjSROVpXjPbFTBQUreR6p/78UDNJOxqGXBg==", null, false, "58f02152-3ed0-4c50-8462-6fea267086d0", "van der Merwe", false, "pieter.vdm@example.com" },
                    { "customer-6", 0, null, "079 876 5432", "694d4f88-a868-4197-8a7b-b390e13f7596", "ApplicationUser", "karabo.nkosi@example.com", true, "9701063123084", false, null, "Karabo", "KARABO.NKOSI@EXAMPLE.COM", "KARABO.NKOSI@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJXBQlfYorR1rn7rpcDW0bDOQBxyY/Qp3eLjU+p/nAbiIAeCj1m4EuXqpJhh6xmr1Q==", null, false, "d7a34aab-a1ba-43fe-a158-c5abd59bcdaf", "Nkosi", false, "karabo.nkosi@example.com" },
                    { "customer-7", 0, null, "078 234 5678", "7fd609b7-fe1f-4c4d-a2d1-9fe83af26250", "ApplicationUser", "annelise.botha@example.com", true, "8609031023086", false, null, "Annelise", "ANNELISE.BOTHA@EXAMPLE.COM", "ANNELISE.BOTHA@EXAMPLE.COM", "AQAAAAIAAYagAAAAELtJv0RF4wj7HslquIyqBn+2E3Mdd2zAMU+o4TnC5RpywmLyNB1UcqPFWSs72faP0A==", null, false, "77a7653f-f9eb-478e-8ead-277375ae19e2", "Botha", false, "annelise.botha@example.com" },
                    { "customer-8", 0, null, "084 567 8901", "a5c4d228-33c2-474e-967d-1b494ca2b73a", "ApplicationUser", "kagiso.motloung@example.com", true, "0005016123080", false, null, "Kagiso", "KAGISO.MOTLOUNG@EXAMPLE.COM", "KAGISO.MOTLOUNG@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHHnVrA08RsjsFGlZmFv02JqgOIlqoslPNSE/KRrQxCOJ4z+3NxrpcOfX+5efuTE5w==", null, false, "1eefbdca-644a-4f5a-9f89-cc84f66e9a05", "Motloung", false, "kagiso.motloung@example.com" },
                    { "customer-9", 0, null, "071 234 8901", "62d6128f-adf4-48b5-8185-5ce7ce309c73", "ApplicationUser", "sibusiso.gumede@example.com", true, "9204174123083", false, null, "Sibusiso", "SIBUSISO.GUMEDE@EXAMPLE.COM", "SIBUSISO.GUMEDE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOZ7bgCYzPr132ucrIa2tGO1R2E0c6BmYBd2Bqbi1al0qLBqPmVPBKViqK0MJekGlg==", null, false, "50fb8a17-b7a8-409d-a91e-9b9b65a53ce3", "Gumede", false, "sibusiso.gumede@example.com" },
                    { "manager-1", 0, null, "082 289 4758", "e2a0ce87-5ae1-4660-8ddb-e5dc8af50e79", "ApplicationUser", "s224378449@mandela.ac.za", true, "1234567890126", false, null, "Mbasa", "S224378449@MANDELA.AC.ZA", "S224378449@MANDELA.AC.ZA", "AQAAAAIAAYagAAAAELsYFibqYD20XcL6FFtexOBQNDfQnU0H960gJGYjME/HLuMwfB7Lhxwbm5oOaO7ACA==", null, false, "648c81ce-2134-4504-a469-cf4087b91844", "Majila", false, "s224378449@mandela.ac.za" },
                    { "pharmacist-1", 0, null, "061 2345 678", "700df2ec-7980-481c-ad76-5452d17f2827", "ApplicationUser", "lindile@example.com", true, "1234567890123", false, null, "Lindile", "LINDILE@EXAMPLE.COM", "LINDILE@EXAMPLE.COM", "AQAAAAIAAYagAAAAENDkgmZDhpmKf3+C9hjktAuYMe2J9UDuAv/7XF14kc7XB4d3HcLLF3U+Sfn2NvbBEg==", null, false, "c2fbf304-c05b-459d-99b7-eabf3c75fbaf", "Hadebe", false, "lindile@example.com" },
                    { "pharmacist-2", 0, null, "062 2345 678", "81fc76db-63be-4705-b60d-1f66a4b8a80b", "ApplicationUser", "dorothy@example.com", true, "1234567890124", false, null, "Dorothy", "DOROTHY@EXAMPLE.COM", "DOROTHY@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ7QTHIJmX4ac9Wld9cr0Dea2TxgZp3VJMeAfuOh4eOWbTZrtGE94to9VxRXQNeTqQ==", null, false, "9941f066-6f2f-45e0-963c-8c93990a8960", "Daniels", false, "dorothy@example.com" },
                    { "pharmacist-3", 0, null, "063 2345 678", "9d17935c-bd7a-41f2-a16a-f1611e789c4a", "ApplicationUser", "marcel@example.com", true, "1234567890125", false, null, "Marcel", "MARCEL@EXAMPLE.COM", "MARCEL@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGUQSlMu51ABVonsepg0nLNu8gQ5tbeZxo6eoXxjoa8A195Sh3es3g2dSOliCdtyDg==", null, false, "61b1e2b5-1ff0-44aa-bb9e-fc0c2ba9ee45", "Van Niekerk", false, "marcel@example.com" },
                    { "pharmacist-4", 0, null, "063 2345 678", "5f141b70-0ec4-45f2-9073-bd3e9fc4dd2e", "ApplicationUser", "s224113038@mandela.ac.za", true, "2201461182425424", false, null, "Melusi", "S224113038@MANDELA.AC.ZA", "S224113038@MANDELA.AC.ZA", "AQAAAAIAAYagAAAAEDnt37LvMM0Q9+cltmO+Dy+6qad4VdHkxYqHKiaB+ReB9RSSjasqnk+O8jHUmb3h1w==", null, false, "65b206db-32a8-483c-9bbc-752cce6aca1f", "Mamba", false, "s224113038@mandela.ac.za" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorID", "ContactNumber", "Email", "HealthCouncilRegistrationNumber", "Name", "OrderID", "Surname" },
                values: new object[,]
                {
                    { 1, "071 234 5678", "charmaine@example.com", "976431", "Charmaine", null, "Meintjies" },
                    { 2, "072 234 5678", "jacob@example.com", "316497", "Jacob", null, "Moloi" },
                    { 3, "073 234 5678", "david@gmail.example", "718293", "David", null, "Greeff" },
                    { 4, "075 234 5678", "karien@example.com", "159753", "Karien", null, "Momberg" },
                    { 5, "076 234 5678", "felicity@example.com", "951357", "Felicity", null, "Daniels" },
                    { 6, "078 234 5678", "errol@example.com", "852456", "Errol", null, "Pieterse" },
                    { 7, "079 234 5678", "alyce@example.com", "654852", "Alyce", null, "Morapedi" }
                });

            migrationBuilder.InsertData(
                table: "DosageForms",
                columns: new[] { "DosageFormID", "DosageFormName" },
                values: new object[,]
                {
                    { 1, "Tablet" },
                    { 2, "Capsule" },
                    { 3, "Suspension" },
                    { 4, "Syrup" },
                    { 5, "Lotion" },
                    { 6, "Spray" },
                    { 7, "Gel" },
                    { 8, "Suppository" },
                    { 9, "Injectable" },
                    { 10, "Drops" },
                    { 11, "IV Drip" },
                    { 12, "Powder" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierID", "ContactName", "ContactSurname", "EmailAddress", "SupplierName" },
                values: new object[,]
                {
                    { 1, "Davie", "Jones", "davie@example.com", "NovaCure" },
                    { 2, "Nicky", "Mostert", "nmostert@mandela.ac.za", "HelixMed" },
                    { 3, "Matimu", "Vuqa", "matimu@example.com", "VitaGenix" },
                    { 4, "Lulu", "Ndhambi", "lulu@example.com", "Apex Biomed" },
                    { 5, "Mbasa", "Majila", "s224378449@mandela.ac.za", "CuraNova" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-customer", "customer-1" },
                    { "role-customer", "customer-10" },
                    { "role-customer", "customer-11" },
                    { "role-customer", "customer-12" },
                    { "role-customer", "customer-13" },
                    { "role-customer", "customer-14" },
                    { "role-customer", "customer-15" },
                    { "role-customer", "customer-16" },
                    { "role-customer", "customer-17" },
                    { "role-customer", "customer-18" },
                    { "role-customer", "customer-19" },
                    { "role-customer", "customer-2" },
                    { "role-customer", "customer-20" },
                    { "role-customer", "customer-3" },
                    { "role-customer", "customer-4" },
                    { "role-customer", "customer-5" },
                    { "role-customer", "customer-6" },
                    { "role-customer", "customer-7" },
                    { "role-customer", "customer-8" },
                    { "role-customer", "customer-9" },
                    { "role-manager", "manager-1" },
                    { "role-pharmacist", "pharmacist-1" },
                    { "role-pharmacist", "pharmacist-2" },
                    { "role-pharmacist", "pharmacist-3" },
                    { "role-pharmacist", "pharmacist-4" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustormerID", "ApplicationUserId" },
                values: new object[,]
                {
                    { 1, "customer-1" },
                    { 2, "customer-2" },
                    { 3, "customer-3" },
                    { 4, "customer-4" },
                    { 5, "customer-5" },
                    { 6, "customer-6" },
                    { 7, "customer-7" },
                    { 8, "customer-8" },
                    { 9, "customer-9" },
                    { 10, "customer-10" },
                    { 11, "customer-11" },
                    { 12, "customer-12" },
                    { 13, "customer-13" },
                    { 14, "customer-14" },
                    { 15, "customer-15" },
                    { 16, "customer-16" },
                    { 17, "customer-17" },
                    { 18, "customer-18" },
                    { 19, "customer-19" },
                    { 20, "customer-20" }
                });

            migrationBuilder.InsertData(
                table: "Medications",
                columns: new[] { "MedcationID", "CurrentPrice", "DosageFormID", "MedicationName", "QuantityOnHand", "ReOrderLevel", "Schedule", "SupplierID" },
                values: new object[,]
                {
                    { 1, 2999, 1, "CardioVex", 90, 100, "6", 1 },
                    { 2, 1550, 1, "Neurocalm", 100, 110, "2", 2 },
                    { 3, 799, 12, "Allerfree Duo", 100, 150, "0", 3 },
                    { 4, 1999, 1, "GastroEase", 470, 400, "3", 4 },
                    { 5, 1000, 1, "Respivent", 490, 300, "3", 5 },
                    { 6, 5150, 1, "Dermagard", 790, 600, "3", 2 },
                    { 7, 3699, 1, "Metaborex", 250, 200, "4", 2 },
                    { 8, 1990, 1, "Sleeptraze", 110, 100, "2", 2 },
                    { 9, 2350, 3, "OsteoFlex", 210, 200, "3", 2 },
                    { 10, 6799, 9, "Immunexin", 190, 200, "6", 2 },
                    { 11, 4450, 11, "CardioPlus", 600, 500, "6", 2 },
                    { 12, 3499, 11, "AllerCalm", 410, 400, "6", 2 },
                    { 13, 4150, 9, "RespirAid", 100, 100, "6", 2 },
                    { 14, 6799, 5, "DermaClear", 200, 100, "6", 2 },
                    { 15, 1999, 2, "OsteoPrime", 400, 100, "6", 2 }
                });

            migrationBuilder.InsertData(
                table: "Pharmacists",
                columns: new[] { "PharmacistID", "ApplicationUserId", "HealthCouncilRegNo" },
                values: new object[,]
                {
                    { 1, "pharmacist-1", "123456" },
                    { 2, "pharmacist-2", "234567" },
                    { 3, "pharmacist-3", "345678" },
                    { 4, "pharmacist-4", "146220" }
                });

            migrationBuilder.InsertData(
                table: "PharmacyManagers",
                columns: new[] { "PharmacyManagerID", "ApplicationUserId", "HealthCouncilRegNo" },
                values: new object[] { 1, "manager-1", "134679" });

            migrationBuilder.InsertData(
                table: "Custormer_Allergies",
                columns: new[] { "Custormer_AllergyID", "Active_IngredientID", "CustomerID" },
                values: new object[,]
                {
                    { 1, 11, 2 },
                    { 2, 19, 7 },
                    { 3, 28, 7 },
                    { 4, 30, 7 },
                    { 5, 14, 12 },
                    { 6, 6, 12 }
                });

            migrationBuilder.InsertData(
                table: "Medication_Ingredients",
                columns: new[] { "Medication_IngredientID", "Active_IngredientID", "MedicationID", "Strength" },
                values: new object[,]
                {
                    { 1, 6, 1, "18mg" },
                    { 2, 2, 2, "2mg" },
                    { 3, 3, 2, "50mg" },
                    { 4, 4, 3, "325mg" },
                    { 5, 5, 3, "453.6g" },
                    { 6, 1, 4, "5mg" },
                    { 7, 7, 5, "100mg" },
                    { 8, 8, 5, "25mg" },
                    { 9, 9, 6, "1mg" },
                    { 10, 10, 6, "2mg" },
                    { 11, 11, 7, "10mg" },
                    { 12, 12, 8, "45mg" },
                    { 13, 13, 9, "200mg" },
                    { 14, 14, 9, "250mg" },
                    { 15, 15, 10, "20mg" },
                    { 16, 16, 10, "30mg" },
                    { 17, 13, 11, "50mg" },
                    { 18, 6, 11, "30mg" },
                    { 19, 4, 12, "50mg" },
                    { 20, 7, 13, "20mg" },
                    { 21, 9, 14, "20mg" },
                    { 22, 13, 15, "20mg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Custormer_Allergies_Active_IngredientID",
                table: "Custormer_Allergies",
                column: "Active_IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Custormer_Allergies_CustomerID",
                table: "Custormer_Allergies",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_OrderID",
                table: "Doctors",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_Ingredients_Active_IngredientID",
                table: "Medication_Ingredients",
                column: "Active_IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_Ingredients_MedicationID",
                table: "Medication_Ingredients",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DosageFormID",
                table: "Medications",
                column: "DosageFormID");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_SupplierID",
                table: "Medications",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_MedicationID",
                table: "OrderLines",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderID",
                table: "OrderLines",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ScriptLineID",
                table: "OrderLines",
                column: "ScriptLineID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DoctorID",
                table: "Orders",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PharmacistID",
                table: "Orders",
                column: "PharmacistID");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_PharmacistID",
                table: "Pharmacies",
                column: "PharmacistID");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_ApplicationUserId",
                table: "Pharmacists",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyManagers_ApplicationUserId",
                table: "PharmacyManagers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ApplicationUserId",
                table: "Prescriptions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorID",
                table: "Prescriptions",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_PresScriptLines_MedicationID",
                table: "PresScriptLines",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PresScriptLines_NewScriptPrescriptionID",
                table: "PresScriptLines",
                column: "NewScriptPrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_PresScriptLines_PrescriptionID",
                table: "PresScriptLines",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_ScriptLines_MedicationID",
                table: "ScriptLines",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_ScriptLines_PrescriptionID",
                table: "ScriptLines",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderDetails_MedicationID",
                table: "StockOrderDetails",
                column: "MedicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Orders_OrderID",
                table: "Doctors",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");
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
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Orders_OrderID",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Custormer_Allergies");

            migrationBuilder.DropTable(
                name: "Medication_Ingredients");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "PharmacyManagers");

            migrationBuilder.DropTable(
                name: "PresScriptLines");

            migrationBuilder.DropTable(
                name: "StockOrderDetails");

            migrationBuilder.DropTable(
                name: "StockOrders");

            migrationBuilder.DropTable(
                name: "UnprocessedScripts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Active_Ingredients");

            migrationBuilder.DropTable(
                name: "ScriptLines");

            migrationBuilder.DropTable(
                name: "NewScripts");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "DosageForms");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Pharmacists");
        }
    }
}
