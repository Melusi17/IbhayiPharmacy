using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class Phase1 : Migration
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
                    { "customer-1", 0, null, "082 345 6789", "66d7b3c7-ea7d-4ec5-8532-8aad357f8db2", "ApplicationUser", "thabo.mokoena@example.com", true, "8805125123087", false, null, "Thabo", "THABO.MOKOENA@EXAMPLE.COM", "THABO.MOKOENA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEM9Cf4/0N7PWPdmk4Yti0+OMhTDHNMKbAAygvxM9+bb8TWhkci+PFNwLSr+JuVP27Q==", null, false, "a8fceca5-c7be-4abf-8ff8-99dc5d04c15c", "Mokoena", false, "thabo.mokoena@example.com" },
                    { "customer-10", 0, null, "073 456 7890", "61d95747-4855-427b-a679-45aeadcb129b", "ApplicationUser", "michelle.pretorius@example.com", true, "8508232023088", false, null, "Michelle", "MICHELLE.PRETORIUS@EXAMPLE.COM", "MICHELLE.PRETORIUS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKYQJiEembzdW61Mn9SXymStN1j6G14ne2XQlPLU4T8T5NUmUPdw8Kegqp2jk29giw==", null, false, "f11891e0-f7e5-4834-abc0-62a1c25e8eba", "Pretorius", false, "michelle.pretorius@example.com" },
                    { "customer-11", 0, null, "082 987 6543", "5e0397a2-a3e6-47d8-91ee-d019d748b5d4", "ApplicationUser", "vusi.zulu@example.com", true, "8803115123089", false, null, "Vusi", "VUSI.ZULU@EXAMPLE.COM", "VUSI.ZULU@EXAMPLE.COM", "AQAAAAIAAYagAAAAEC7XWOLI7tgUUqQPQnWbF/YrWHkuJPmwplIcIHUfJeyxFCnGnsfgc4v2LV10huX1rg==", null, false, "cb42810d-109b-4219-b177-814d2d6b4a90", "Zulu", false, "vusi.zulu@example.com" },
                    { "customer-12", 0, null, "079 123 4567", "e394aa81-197e-470d-8a28-d6969a242cf0", "ApplicationUser", "aisha.jacobs@example.com", true, "9909020323082", false, null, "Aisha", "AISHA.JACOBS@EXAMPLE.COM", "AISHA.JACOBS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEE3Qio4bVwcnZintuqckUcEk/q1P0BBD7LyU5tn8G9c7C/gb3quUfMV8sYSct5SYDw==", null, false, "109b6463-4ccc-4325-9ffd-cc88b08c9c4c", "Jacobs", false, "aisha.jacobs@example.com" },
                    { "customer-13", 0, null, "074 567 8901", "9bffe39b-efc6-4dd4-950d-5e8d496c7f73", "ApplicationUser", "johan.deklerk@example.com", true, "8702054023087", false, null, "Johan", "JOHAN.DEKLERK@EXAMPLE.COM", "JOHAN.DEKLERK@EXAMPLE.COM", "AQAAAAIAAYagAAAAEM7YbPpasuF2MYtYaucu1kUl3YLKihYR30Dct3ajzpmWu9Yah2pykiimABv2/6MrqQ==", null, false, "54487ef6-07ea-4d16-a9c4-3290a8de9a9e", "de Klerk", false, "johan.deklerk@example.com" },
                    { "customer-14", 0, null, "078 987 6543", "a25dbe09-0910-4fd0-922c-d5b335329f8c", "ApplicationUser", "thandiwe.sithole@example.com", true, "9306203123086", false, null, "Thandiwe", "THANDIWE.SITHOLE@EXAMPLE.COM", "THANDIWE.SITHOLE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHNao3+w8JcNdAzS3HyJQsadsxmYQlO/ZcsMNytYcpMdE+Zg9PlqokB5px+pNq4J0w==", null, false, "c4e1a844-6e9d-4b65-a569-29e77fe6fcb1", "Sithole", false, "thandiwe.sithole@example.com" },
                    { "customer-15", 0, null, "071 345 6789", "e498b27c-fb7e-435c-abde-95d6bdc7a79c", "ApplicationUser", "riaan.vw@example.com", true, "8108305023081", false, null, "Riaan", "RIAAN.VW@EXAMPLE.COM", "RIAAN.VW@EXAMPLE.COM", "AQAAAAIAAYagAAAAECzcoANnB/1QRMkv1ZJNGrcz6lGcUg7CWhmOkuSzKel3NxCnewoXROtddzCvUausyw==", null, false, "028427b7-2c76-4229-8c14-826bc5d55d94", "van Wyk", false, "riaan.vw@example.com" },
                    { "customer-16", 0, null, "083 234 5678", "0bdf7245-0d15-4c1b-8c62-8b12b9e0e18a", "ApplicationUser", "palesa.molefe@example.com", true, "9501151023084", false, null, "Palesa", "PALESA.MOLEFE@EXAMPLE.COM", "PALESA.MOLEFE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEI1uDi2W15opNgFhCLICXSvdOvMzze/jN+VYd1X1h7XwqEt1dvl/nHAvmwivv9YvaA==", null, false, "048c8205-5aa4-441c-8ac9-d8bfc766fa07", "Molefe", false, "palesa.molefe@example.com" },
                    { "customer-17", 0, null, "072 987 1234", "9e789960-02d8-4e0a-bf57-fdc756494373", "ApplicationUser", "kobus.smit@example.com", true, "7909094023083", false, null, "Kobus", "KOBUS.SMIT@EXAMPLE.COM", "KOBUS.SMIT@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGO/5pkeMpkX7davwHW4nNipLowTIqm2gDuWPqMJPc9cldTNNIDO9H0AsOQXglwdkg==", null, false, "dc600c70-6a12-4e31-bf50-adf97dac7127", "Smit", false, "kobus.smit@example.com" },
                    { "customer-18", 0, null, "079 456 7890", "1d7bf62f-523b-4ee6-9bf7-ede4d592b434", "ApplicationUser", "zanele.mthembu@example.com", true, "9604232123085", false, null, "Zanele", "ZANELE.MTHEMBU@EXAMPLE.COM", "ZANELE.MTHEMBU@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKQoJKvXjBZgTsZRIiybcU4jieEdikbggKjO/tgS/IkcFfYEUb0QZzSn7uznDNZ4RQ==", null, false, "b48a45f3-3dbc-4401-803a-5bb6144bac12", "Mthembu", false, "zanele.mthembu@example.com" },
                    { "customer-19", 0, null, "074 123 4567", "faa04dc5-e483-46cc-a9d9-295461f820b0", "ApplicationUser", "annelise@example.com", true, "8407125023080", false, null, "Annelise", "ANNELISE@EXAMPLE.COM", "ANNELISE@EXAMPLE.COM", "AQAAAAIAAYagAAAAENwHuK8NfiGrhb7VAEepcc6/yLOzhpgIGQD/ZtqAsF01RN8Dt2CLTE5C1zZjx5xCjg==", null, false, "c467b834-ab67-46f7-b421-b55c6881d278", "Botha", false, "annelise@example.com" },
                    { "customer-2", 0, null, "083 567 8910", "efdecf84-6a69-4772-9259-800e1977fc89", "ApplicationUser", "lerato.khumalo@example.com", true, "9503140321089", false, null, "Lerato", "LERATO.KHUMALO@EXAMPLE.COM", "LERATO.KHUMALO@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDEek4mkgHlkZAR4DGCAIM4u4pli9bq650mMJbMpfpOqz4K1W5DoB6k+WEUh6QhhtA==", null, false, "46826802-680b-495a-8cfe-cfc31c178a73", "Khumalo", false, "lerato.khumalo@example.com" },
                    { "customer-20", 0, null, "081 567 8901", "246d6d4b-2ecb-419f-854f-be71a5943b6d", "ApplicationUser", "nomsa.ndlovu@example.com", true, "9703280123088", false, null, "Nomsa", "NOMSA.NDLOVU@EXAMPLE.COM", "NOMSA.NDLOVU@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMgCD3jXqn9mGJBO0ZL0lxIA87VirPCR2E8BycYKJrItIYhWiKR86Lnz3AEcRoTwLg==", null, false, "6cdec0ba-6fd6-451d-8ffb-a259e1157a71", "Ndlovu", false, "nomsa.ndlovu@example.com" },
                    { "customer-3", 0, null, "084 123 4567", "4ec6eb13-dda9-4202-9ca4-e32626e8875f", "ApplicationUser", "sipho.dlamini@example.com", true, "7902106123081", false, null, "Sipho", "SIPHO.DLAMINI@EXAMPLE.COM", "SIPHO.DLAMINI@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFx6riz1+arE4vXLd4KYYdSy4Gsd36O/v3UNexHODuB5sFMimtcMMxiW17VHEiaFsw==", null, false, "bf16da76-3556-4d73-8dfc-d7a62cc57361", "Dlamini", false, "sipho.dlamini@example.com" },
                    { "customer-4", 0, null, "081 234 9876", "92b4a832-d9cc-4eea-9ffc-f7079e8a3ce2", "ApplicationUser", "naledi.maseko@example.com", true, "9107280023085", false, null, "Naledi", "NALEDI.MASEKO@EXAMPLE.COM", "NALEDI.MASEKO@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIlWXbpDdm9BUdb/+1Q2TA/7t9lPAyD7K0TustP654iodifL+o7qHMEm4FL80F4D0A==", null, false, "519d433a-af8d-4ed6-844c-11191b71379f", "Maseko", false, "naledi.maseko@example.com" },
                    { "customer-5", 0, null, "072 345 1234", "fca45c3b-97b4-4ba7-8b7e-c67220a56dfd", "ApplicationUser", "pieter.vdm@example.com", true, "8306195023082", false, null, "Pieter", "PIETER.VDM@EXAMPLE.COM", "PIETER.VDM@EXAMPLE.COM", "AQAAAAIAAYagAAAAEM7AWpcI/wW5oK6cvwFPdEtBZwMtIfpbCGgkf+wRCvg8y9plGayOWtKsjEYB319JLw==", null, false, "04870422-a7d8-4762-ae30-3b6f88b39be2", "van der Merwe", false, "pieter.vdm@example.com" },
                    { "customer-6", 0, null, "079 876 5432", "aa34220b-4818-4395-adc3-9354ae09dbfa", "ApplicationUser", "karabo.nkosi@example.com", true, "9701063123084", false, null, "Karabo", "KARABO.NKOSI@EXAMPLE.COM", "KARABO.NKOSI@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMEvyBkTqljftmelsPJ6jrWAMBVgocLgXu1D74A44PUy2fhy7dhNP4JXYSx1O2bpdA==", null, false, "e99c345a-7b1b-4666-9677-9f27311285a1", "Nkosi", false, "karabo.nkosi@example.com" },
                    { "customer-7", 0, null, "078 234 5678", "fb5966c7-aef9-4b98-948a-84c67e44bf85", "ApplicationUser", "annelise.botha@example.com", true, "8609031023086", false, null, "Annelise", "ANNELISE.BOTHA@EXAMPLE.COM", "ANNELISE.BOTHA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPkVX+1Hgbk3qCod2WGnFFXQ8m9TFMhTq4yuGAkK9L44ahKnC+rkaNn6Airw4PFR4A==", null, false, "4bedd8b0-ec49-4609-beb1-3ea60ae2ba74", "Botha", false, "annelise.botha@example.com" },
                    { "customer-8", 0, null, "084 567 8901", "60bd43d7-6615-46f5-9608-6f6e7e991924", "ApplicationUser", "kagiso.motloung@example.com", true, "0005016123080", false, null, "Kagiso", "KAGISO.MOTLOUNG@EXAMPLE.COM", "KAGISO.MOTLOUNG@EXAMPLE.COM", "AQAAAAIAAYagAAAAED4iBeai4tDXVtyyX8kJNUJuvSTI3FgoY039oeADKOk6x1Y1ezvYJorcp9eRZPMpmg==", null, false, "79e24e8f-3b92-45e5-94a4-b00162a6f6ad", "Motloung", false, "kagiso.motloung@example.com" },
                    { "customer-9", 0, null, "071 234 8901", "743ec571-5f3a-4f85-b63b-43cfcaff158b", "ApplicationUser", "sibusiso.gumede@example.com", true, "9204174123083", false, null, "Sibusiso", "SIBUSISO.GUMEDE@EXAMPLE.COM", "SIBUSISO.GUMEDE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEVJcu1o68cqpGLA7akcER9u5yDVtDXPz6dbp/vJy3L1cy8LF1Y7YUrntU+P4HEOxg==", null, false, "83f0555c-21f8-4ac8-b1d0-0b7b80aa7481", "Gumede", false, "sibusiso.gumede@example.com" },
                    { "manager-1", 0, null, "082 289 4758", "5137bade-2a23-40cf-83d3-c96b63d21c74", "ApplicationUser", "john.doe@example.com", true, "1234567890126", false, null, "John", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEC48EIovDrsKRvnzIoieTmVZAbsIBOZahcn7sCDG5Hk2oBMldfzek6TSC3xO5T5lbg==", null, false, "d2cd1c9f-2ece-417f-b744-0d2d5884c6a7", "Doe", false, "john.doe@example.com" },
                    { "pharmacist-1", 0, null, "061 2345 678", "ece3935d-c515-4809-bfef-85328d02a227", "ApplicationUser", "lindile@example.com", true, "1234567890123", false, null, "Lindile", "LINDILE@EXAMPLE.COM", "LINDILE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBIRwXep4BiMYeAqc1GXZwI0BZiIfklwF0oPGol6jxNesPk+KXJ1uac48xNba7VBYw==", null, false, "bf607900-4b76-48d7-9242-ebac60fe7eb0", "Hadebe", false, "lindile@example.com" },
                    { "pharmacist-2", 0, null, "062 2345 678", "dea3345f-a841-454e-b385-55c74740268b", "ApplicationUser", "dorothy@example.com", true, "1234567890124", false, null, "Dorothy", "DOROTHY@EXAMPLE.COM", "DOROTHY@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGTQdtFUDkihEUis24mEWeggCD2DZR+9pyTGrTSoUMR2qW0s+6id6j+I8S7PatyGZQ==", null, false, "df84c9e9-2781-4349-acc3-b04e8fba5242", "Daniels", false, "dorothy@example.com" },
                    { "pharmacist-3", 0, null, "063 2345 678", "7c03bd30-df64-491f-9373-6e68c01607b3", "ApplicationUser", "marcel@example.com", true, "1234567890125", false, null, "Marcel", "MARCEL@EXAMPLE.COM", "MARCEL@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA9JxQaeycyXLz1Gy97uE3WPI8fz3FC/s3aE30OpAdBu6dJdt1OOb1F9qnbOL+LfQw==", null, false, "dfd2ad3c-038a-42cd-b8b7-90f5c2b96f4c", "Van Niekerk", false, "marcel@example.com" }
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
                    { 5, "Pharmacy Manager Group Member Name", "Pharmacy Manager Group Member Surname", "Pharmacy Manager Group Member E-mail", "CuraNova" }
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
                    { "role-pharmacist", "pharmacist-3" }
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
                    { 3, "pharmacist-3", "345678" }
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
