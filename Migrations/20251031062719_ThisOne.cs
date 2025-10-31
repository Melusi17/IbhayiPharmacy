using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbhayiPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class ThisOne : Migration
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
                    { "customer-1", 0, null, "082 345 6789", "052d572b-93a4-49ae-bcc0-0e19494e57fc", "ApplicationUser", "thabo.mokoena@example.com", true, "8805125123087", false, null, "Thabo", "THABO.MOKOENA@EXAMPLE.COM", "THABO.MOKOENA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIviiuA+LTDA0jrUT8VV7hQljBafjDsW62py9AQ5M2xBjlb0tYHvur6nopryPMQ1bA==", null, false, "b6c18fe8-715c-4086-8f4c-513d5784da11", "Mokoena", false, "thabo.mokoena@example.com" },
                    { "customer-10", 0, null, "073 456 7890", "a92fb0e1-c0fb-47c9-8478-00dec85b074a", "ApplicationUser", "michelle.pretorius@example.com", true, "8508232023088", false, null, "Michelle", "MICHELLE.PRETORIUS@EXAMPLE.COM", "MICHELLE.PRETORIUS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEH2S27GWkc9AyyhF4Y+H3khEb5lXlhLgSGAWSW8xZ9UHkIc439fbrj8nCzLMCbUPsg==", null, false, "7dd48ef1-7012-4a06-944e-a80fba50a38f", "Pretorius", false, "michelle.pretorius@example.com" },
                    { "customer-11", 0, null, "082 987 6543", "b74e3fc8-20e6-4f65-b7b6-8ed30874f28d", "ApplicationUser", "vusi.zulu@example.com", true, "8803115123089", false, null, "Vusi", "VUSI.ZULU@EXAMPLE.COM", "VUSI.ZULU@EXAMPLE.COM", "AQAAAAIAAYagAAAAENBecj5oioz/pqrZLGgpEmCulexDARYBusjTcjhBeV0Bl8btaZ48XaO5JP8N28Q9nw==", null, false, "459bea80-7861-4b15-aedf-9ce71e06363f", "Zulu", false, "vusi.zulu@example.com" },
                    { "customer-12", 0, null, "079 123 4567", "b4e23be7-d04d-4fa0-865a-0722aaaadbcf", "ApplicationUser", "aisha.jacobs@example.com", true, "9909020323082", false, null, "Aisha", "AISHA.JACOBS@EXAMPLE.COM", "AISHA.JACOBS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEH3YdCGxhVzdMb4aNDX3OCpp/PpayP7ce9lqaRO1p7B2T9PH6wiceBEAkuDx5+omvQ==", null, false, "ae394ae8-62a5-49a4-9a1f-5c8886d58497", "Jacobs", false, "aisha.jacobs@example.com" },
                    { "customer-13", 0, null, "074 567 8901", "7c38a9e4-6222-4fca-8983-9224c745facf", "ApplicationUser", "johan.deklerk@example.com", true, "8702054023087", false, null, "Johan", "JOHAN.DEKLERK@EXAMPLE.COM", "JOHAN.DEKLERK@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDjE8tDkQxfMa7z3/W4j6de/jHikpCp1/+8iJH/e4vRFD9DUxvFf59zURjmwaWQFVA==", null, false, "0a2605b6-1e58-4b3e-aee7-0ad46b07c282", "de Klerk", false, "johan.deklerk@example.com" },
                    { "customer-14", 0, null, "078 987 6543", "70c65b58-3c1d-478c-8774-a022c0a9d623", "ApplicationUser", "thandiwe.sithole@example.com", true, "9306203123086", false, null, "Thandiwe", "THANDIWE.SITHOLE@EXAMPLE.COM", "THANDIWE.SITHOLE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFO2CJ43klmpB5ctbuxPwUhKVHqSPQxMJ50Yj4EJNFlF4pNpVkeNbkY1yHwgzGPUjQ==", null, false, "d5cb6c0d-f7c2-43b8-bc6b-bc7b6ae74f36", "Sithole", false, "thandiwe.sithole@example.com" },
                    { "customer-15", 0, null, "071 345 6789", "1aefab9c-3c84-439c-ad1f-1bd2966c492b", "ApplicationUser", "riaan.vw@example.com", true, "8108305023081", false, null, "Riaan", "RIAAN.VW@EXAMPLE.COM", "RIAAN.VW@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFieQ/13S22DRMii8IPq19GSXpLz0Aedfgl9lPCo6cgWNH6a/uo6a/RjvIEmjUFDvw==", null, false, "aee01418-35fc-430c-8a94-f6b6fea266fa", "van Wyk", false, "riaan.vw@example.com" },
                    { "customer-16", 0, null, "083 234 5678", "e815ed75-b714-47ed-a947-12b6da711b0e", "ApplicationUser", "palesa.molefe@example.com", true, "9501151023084", false, null, "Palesa", "PALESA.MOLEFE@EXAMPLE.COM", "PALESA.MOLEFE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEH8RArku0Xj6F7ZYOCdFaeGwVkCEVteiFw7pbiaClv/nKQE5bFpRIo1nbCVD+Y2QlQ==", null, false, "86a8e031-e80a-43ac-893e-47358afc0aad", "Molefe", false, "palesa.molefe@example.com" },
                    { "customer-17", 0, null, "072 987 1234", "4e11d66c-ebb8-4460-aacb-5ab23a8c65af", "ApplicationUser", "kobus.smit@example.com", true, "7909094023083", false, null, "Kobus", "KOBUS.SMIT@EXAMPLE.COM", "KOBUS.SMIT@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAFBFN4uAEm7BiPnBgf+qRG1l4BXw0SCTB3O6efX37Fbd2/s7d+TJnNtFILPTDvhqw==", null, false, "fbe18bb7-3633-416c-8f98-f052017f00e5", "Smit", false, "kobus.smit@example.com" },
                    { "customer-18", 0, null, "079 456 7890", "e5e07a4b-7af2-4115-a71c-c99c201938d9", "ApplicationUser", "zanele.mthembu@example.com", true, "9604232123085", false, null, "Zanele", "ZANELE.MTHEMBU@EXAMPLE.COM", "ZANELE.MTHEMBU@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFFQ7wy/5VZxKK7u4kGYfRRk/rzKvqi8ZktCwhti96zyl9JxjoWPyggm1B7VsFLKmQ==", null, false, "cdf83dac-6d34-41e4-9dde-f572f5f86990", "Mthembu", false, "zanele.mthembu@example.com" },
                    { "customer-19", 0, null, "074 123 4567", "c32323e4-487c-423f-984b-1ddb13d1ee82", "ApplicationUser", "annelise@example.com", true, "8407125023080", false, null, "Annelise", "ANNELISE@EXAMPLE.COM", "ANNELISE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEH+4btonsZLghzX88tmnJlScUbaEbgS7My1ics5zsbiOzZ5RdL4Kn+QVchjZWm2edA==", null, false, "e714ee6b-891f-406d-951a-57934e13b4f4", "Botha", false, "annelise@example.com" },
                    { "customer-2", 0, null, "083 567 8910", "95677a98-db7b-48ad-b5c6-866eede59d2a", "ApplicationUser", "lerato.khumalo@example.com", true, "9503140321089", false, null, "Lerato", "LERATO.KHUMALO@EXAMPLE.COM", "LERATO.KHUMALO@EXAMPLE.COM", "AQAAAAIAAYagAAAAENRh8hF1J4MQ50BrLCfa5sxH/4r8zxWdqKpg9Cyf/ngcto+s6nUlCgB0H4jvJEoIXg==", null, false, "f8dd97f7-59dd-4549-97c8-18db91abcb29", "Khumalo", false, "lerato.khumalo@example.com" },
                    { "customer-20", 0, null, "081 567 8901", "334eb0c0-11f3-44ec-9aaf-5fcd8be58482", "ApplicationUser", "nomsa.ndlovu@example.com", true, "9703280123088", false, null, "Nomsa", "NOMSA.NDLOVU@EXAMPLE.COM", "NOMSA.NDLOVU@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJtJEB9CPWFgLS5nQgB+8cSEUjHvsgKIE+Kw5LsOxvdkHqkYDYKvphriV1GUwoNINw==", null, false, "1222c2b7-acba-4503-9f7f-1a9a35a02c29", "Ndlovu", false, "nomsa.ndlovu@example.com" },
                    { "customer-3", 0, null, "084 123 4567", "6e2613dd-7fe8-4eb6-9215-82094af2a2d0", "ApplicationUser", "sipho.dlamini@example.com", true, "7902106123081", false, null, "Sipho", "SIPHO.DLAMINI@EXAMPLE.COM", "SIPHO.DLAMINI@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOe2QbT5j38x6vPdkkxzdVZefV8K2b+6AhG3d7oH7u7m5HshFWX8znq6A0H/d+9MuQ==", null, false, "2e075ba1-7c48-4775-8349-35707bc8c7a9", "Dlamini", false, "sipho.dlamini@example.com" },
                    { "customer-4", 0, null, "081 234 9876", "8691ebe5-ff93-4f8d-be9f-362233c3b2be", "ApplicationUser", "naledi.maseko@example.com", true, "9107280023085", false, null, "Naledi", "NALEDI.MASEKO@EXAMPLE.COM", "NALEDI.MASEKO@EXAMPLE.COM", "AQAAAAIAAYagAAAAEI9l+XMTldqxwKw59hEaXALMBH6nhRbBtBHERM4oCQqkOA+PQBOxi3eh5cPVO6T9Xw==", null, false, "46c3a4de-a21b-4185-ad4b-b7a5585f0348", "Maseko", false, "naledi.maseko@example.com" },
                    { "customer-5", 0, null, "072 345 1234", "ad355f78-bf81-43e3-bc8f-afde3269c127", "ApplicationUser", "pieter.vdm@example.com", true, "8306195023082", false, null, "Pieter", "PIETER.VDM@EXAMPLE.COM", "PIETER.VDM@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPujAw6PzpCwXE0t401cTfsLh1wdXHjingrlLRg/1TohaZ8WgLjrmEmkKLqTePUBgA==", null, false, "6c06187b-eefd-4f09-affb-ea57d0c5eb26", "van der Merwe", false, "pieter.vdm@example.com" },
                    { "customer-6", 0, null, "079 876 5432", "deeb7888-f494-465c-b0b6-d42f2dca801e", "ApplicationUser", "karabo.nkosi@example.com", true, "9701063123084", false, null, "Karabo", "KARABO.NKOSI@EXAMPLE.COM", "KARABO.NKOSI@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHUZ+hbvXI6WG/mfb5Z1wozoTVAYDl/uKMPUYTYz/8A7jcbub5dCrd+E3uD9aEFy2Q==", null, false, "c05b0ffd-293e-482f-a00c-4a9421bdbe09", "Nkosi", false, "karabo.nkosi@example.com" },
                    { "customer-7", 0, null, "078 234 5678", "02257b13-eb78-4097-8caa-5919b0d77ce6", "ApplicationUser", "annelise.botha@example.com", true, "8609031023086", false, null, "Annelise", "ANNELISE.BOTHA@EXAMPLE.COM", "ANNELISE.BOTHA@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEtc+UWUzLg7dqiJY72z1NypTFmb2zfe2lXM2xm71xgO/L/J2RyQoJn7X8EZ+SCrTw==", null, false, "a85b22b0-9761-4797-81e4-fae14c3eb059", "Botha", false, "annelise.botha@example.com" },
                    { "customer-8", 0, null, "084 567 8901", "3523ad50-ffaa-404d-bbd9-14938ffc1d4d", "ApplicationUser", "kagiso.motloung@example.com", true, "0005016123080", false, null, "Kagiso", "KAGISO.MOTLOUNG@EXAMPLE.COM", "KAGISO.MOTLOUNG@EXAMPLE.COM", "AQAAAAIAAYagAAAAELISodxlR06IcDrLWfOxgcBGdvRsJ+kUN5ME42H43EhQWZ/sond2xNf9G5DHuEKsdQ==", null, false, "147efb10-5887-4212-8834-a585986fcd26", "Motloung", false, "kagiso.motloung@example.com" },
                    { "customer-9", 0, null, "071 234 8901", "bf718971-7188-4714-ab07-fd4a7b5ccc30", "ApplicationUser", "sibusiso.gumede@example.com", true, "9204174123083", false, null, "Sibusiso", "SIBUSISO.GUMEDE@EXAMPLE.COM", "SIBUSISO.GUMEDE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGLn1jnJOywjz1O3ZziYzeKAi2oSas8ETPuzUYvpWrBdg+uFiQSpmyVwLN+mF5Rtxg==", null, false, "dd6cf372-de39-4ee9-ba42-7c3b93e96ec3", "Gumede", false, "sibusiso.gumede@example.com" },
                    { "manager-1", 0, null, "082 289 4758", "5cc20415-699b-4936-b56f-cfc24e7e6f13", "ApplicationUser", "john.doe@example.com", true, "1234567890126", false, null, "John", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPNuprJYWnTzq8b3QfZEXyo2yOVcrh4xSTgivm+z3RtWpQzXIUSHO3x7tGhIdYUrhg==", null, false, "04858c77-2a87-4a5d-8f8e-22448901e27e", "Doe", false, "john.doe@example.com" },
                    { "pharmacist-1", 0, null, "061 2345 678", "844ed146-3c41-416a-be10-c529c074ca2f", "ApplicationUser", "lindile@example.com", true, "1234567890123", false, null, "Lindile", "LINDILE@EXAMPLE.COM", "LINDILE@EXAMPLE.COM", "AQAAAAIAAYagAAAAECkSLyKsqsZbeB2bXmpG2HUntCBOK6pcn3LZN69W6PdzwE1sNq6FCzfHQ12UH0cNkA==", null, false, "f428ccf8-e43f-42ea-bb4b-ffb8d9c48a68", "Hadebe", false, "lindile@example.com" },
                    { "pharmacist-2", 0, null, "062 2345 678", "e5f57adb-b6f5-4168-af98-4c99896750ef", "ApplicationUser", "dorothy@example.com", true, "1234567890124", false, null, "Dorothy", "DOROTHY@EXAMPLE.COM", "DOROTHY@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIil4+6nzRAi/7kDC5NMiHxmhgiGelshZeHIN1kUPFEaOUoYbKAQagNsQKVRdJlnSQ==", null, false, "377f7d0e-1563-4ce4-ad2a-1596c5dbbf7e", "Daniels", false, "dorothy@example.com" },
                    { "pharmacist-3", 0, null, "063 2345 678", "0b3111db-8b45-4290-9de1-c5ed33a1e9c0", "ApplicationUser", "marcel@example.com", true, "1234567890125", false, null, "Marcel", "MARCEL@EXAMPLE.COM", "MARCEL@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIlNxliCPciDG4eDm1P0Q8UuCb4JjQRn4+j6Y9WzbSj6IVEFojY3ZdtJCOuhl8RJJg==", null, false, "323b63f7-5dd5-446a-b157-9e8ee40f2ab0", "Van Niekerk", false, "marcel@example.com" }
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
