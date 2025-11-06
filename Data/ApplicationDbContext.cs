using IbhayiPharmacy.Models;
using IbhayiPharmacy.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IbhayiPharmacy.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PresScriptLine> PresScriptLines { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<NewScript> NewScripts { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<PharmacyManager> PharmacyManagers { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Custormer_Allergy> Custormer_Allergies { get; set; }
        public DbSet<DosageForm> DosageForms { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Active_Ingredient> Active_Ingredients { get; set; }
        public DbSet<Medication_Ingredient> Medication_Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<UnprocessedScripts> UnprocessedScripts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<StockOrder> StockOrders { get; set; }
        public DbSet<StockOrderDetail> StockOrderDetails { get; set; }
        public DbSet<ScriptLine> ScriptLines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            




            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role-customer", Name = SD.Role_Customer, NormalizedName = SD.Role_Customer.ToUpper() },
                new IdentityRole { Id = "role-pharmacist", Name = SD.Role_Pharmacist, NormalizedName = SD.Role_Pharmacist.ToUpper() },
                new IdentityRole { Id = "role-manager", Name = SD.Role_PharmacyManager, NormalizedName = SD.Role_PharmacyManager.ToUpper() }
            );

            // Seed Active Ingredients
            modelBuilder.Entity<Active_Ingredient>().HasData(
                new Active_Ingredient { Active_IngredientID = 1, Name = "Pylorazine" },
                new Active_Ingredient { Active_IngredientID = 2, Name = "Vaspril" },
                new Active_Ingredient { Active_IngredientID = 3, Name = "Zentropine" },
                new Active_Ingredient { Active_IngredientID = 4, Name = "Histarelin" },
                new Active_Ingredient { Active_IngredientID = 5, Name = "Lorvexamine" },
                new Active_Ingredient { Active_IngredientID = 6, Name = "Aterolazine" },
                new Active_Ingredient { Active_IngredientID = 7, Name = "Bronchomid" },
                new Active_Ingredient { Active_IngredientID = 8, Name = "Alveclear" },
                new Active_Ingredient { Active_IngredientID = 9, Name = "Epidraxol" },
                new Active_Ingredient { Active_IngredientID = 10, Name = "Cortizane" },
                new Active_Ingredient { Active_IngredientID = 11, Name = "Glycetrol" },
                new Active_Ingredient { Active_IngredientID = 12, Name = "Somnexil" },
                new Active_Ingredient { Active_IngredientID = 13, Name = "Calcitrine" },
                new Active_Ingredient { Active_IngredientID = 14, Name = "Phospholax" },
                new Active_Ingredient { Active_IngredientID = 15, Name = "Virocelin" },
                new Active_Ingredient { Active_IngredientID = 16, Name = "Immubrine" },
                new Active_Ingredient { Active_IngredientID = 17, Name = "Trosamine" },
                new Active_Ingredient { Active_IngredientID = 18, Name = "Velocidine" },
                new Active_Ingredient { Active_IngredientID = 19, Name = "Nexorin" },
                new Active_Ingredient { Active_IngredientID = 20, Name = "Zyphralex" },
                new Active_Ingredient { Active_IngredientID = 21, Name = "Cardionol" },
                new Active_Ingredient { Active_IngredientID = 22, Name = "Alveretol" },
                new Active_Ingredient { Active_IngredientID = 23, Name = "Xylogran" },
                new Active_Ingredient { Active_IngredientID = 24, Name = "Fematrix" },
                new Active_Ingredient { Active_IngredientID = 25, Name = "Plastorin" },
                new Active_Ingredient { Active_IngredientID = 26, Name = "Seralox" },
                new Active_Ingredient { Active_IngredientID = 27, Name = "Quantrel" },
                new Active_Ingredient { Active_IngredientID = 28, Name = "Myvetrin" },
                new Active_Ingredient { Active_IngredientID = 29, Name = "Draxolene" },
                new Active_Ingredient { Active_IngredientID = 30, Name = "Veltraxin" }
            );

            // Seed Dosage Forms
            modelBuilder.Entity<DosageForm>().HasData(
                new DosageForm { DosageFormID = 1, DosageFormName = "Tablet" },
                new DosageForm { DosageFormID = 2, DosageFormName = "Capsule" },
                new DosageForm { DosageFormID = 3, DosageFormName = "Suspension" },
                new DosageForm { DosageFormID = 4, DosageFormName = "Syrup" },
                new DosageForm { DosageFormID = 5, DosageFormName = "Lotion" },
                new DosageForm { DosageFormID = 6, DosageFormName = "Spray" },
                new DosageForm { DosageFormID = 7, DosageFormName = "Gel" },
                new DosageForm { DosageFormID = 8, DosageFormName = "Suppository" },
                new DosageForm { DosageFormID = 9, DosageFormName = "Injectable" },
                new DosageForm { DosageFormID = 10, DosageFormName = "Drops" },
                new DosageForm { DosageFormID = 11, DosageFormName = "IV Drip" },
                new DosageForm { DosageFormID = 12, DosageFormName = "Powder" }
            );

            // Seed Suppliers
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierID = 1, SupplierName = "NovaCure", ContactName = "Davie", ContactSurname = "Jones", EmailAddress = "davie@example.com" },
                new Supplier { SupplierID = 2, SupplierName = "HelixMed", ContactName = "Nicky", ContactSurname = "Mostert", EmailAddress = "nmostert@mandela.ac.za" },
                new Supplier { SupplierID = 3, SupplierName = "VitaGenix", ContactName = "Matimu", ContactSurname = "Vuqa", EmailAddress = "matimu@example.com" },
                new Supplier { SupplierID = 4, SupplierName = "Apex Biomed", ContactName = "Lulu", ContactSurname = "Ndhambi", EmailAddress = "lulu@example.com" },
                new Supplier { SupplierID = 5, SupplierName = "CuraNova", ContactName = "Mbasa", ContactSurname = "Majila", EmailAddress = "s224378449@mandela.ac.za" }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorID = 1, Name = "Charmaine", Surname = "Meintjies", ContactNumber = "071 234 5678", Email = "charmaine@example.com", HealthCouncilRegistrationNumber = "976431" },
                new Doctor { DoctorID = 2, Name = "Jacob", Surname = "Moloi", ContactNumber = "072 234 5678", Email = "jacob@example.com", HealthCouncilRegistrationNumber = "316497" },
                new Doctor { DoctorID = 3, Name = "David", Surname = "Greeff", ContactNumber = "073 234 5678", Email = "david@gmail.example", HealthCouncilRegistrationNumber = "718293" },
                new Doctor { DoctorID = 4, Name = "Karien", Surname = "Momberg", ContactNumber = "075 234 5678", Email = "karien@example.com", HealthCouncilRegistrationNumber = "159753" },
                new Doctor { DoctorID = 5, Name = "Felicity", Surname = "Daniels", ContactNumber = "076 234 5678", Email = "felicity@example.com", HealthCouncilRegistrationNumber = "951357" },
                new Doctor { DoctorID = 6, Name = "Errol", Surname = "Pieterse", ContactNumber = "078 234 5678", Email = "errol@example.com", HealthCouncilRegistrationNumber = "852456" },
                new Doctor { DoctorID = 7, Name = "Alyce", Surname = "Morapedi", ContactNumber = "079 234 5678", Email = "alyce@example.com", HealthCouncilRegistrationNumber = "654852" }
            );

            // Seed ApplicationUsers
            modelBuilder.Entity<ApplicationUser>().HasData(
                // Customers
                new ApplicationUser { Id = "customer-1", UserName = "thabo.mokoena@example.com", NormalizedUserName = "THABO.MOKOENA@EXAMPLE.COM", Email = "thabo.mokoena@example.com", NormalizedEmail = "THABO.MOKOENA@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Mokoena123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Thabo", Surname = "Mokoena", IDNumber = "8805125123087", CellphoneNumber = "082 345 6789" },
                new ApplicationUser { Id = "customer-2", UserName = "lerato.khumalo@example.com", NormalizedUserName = "LERATO.KHUMALO@EXAMPLE.COM", Email = "lerato.khumalo@example.com", NormalizedEmail = "LERATO.KHUMALO@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Khumalo123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Lerato", Surname = "Khumalo", IDNumber = "9503140321089", CellphoneNumber = "083 567 8910" },
                new ApplicationUser { Id = "customer-3", UserName = "sipho.dlamini@example.com", NormalizedUserName = "SIPHO.DLAMINI@EXAMPLE.COM", Email = "sipho.dlamini@example.com", NormalizedEmail = "SIPHO.DLAMINI@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Dlamini123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Sipho", Surname = "Dlamini", IDNumber = "7902106123081", CellphoneNumber = "084 123 4567" },
                new ApplicationUser { Id = "customer-4", UserName = "naledi.maseko@example.com", NormalizedUserName = "NALEDI.MASEKO@EXAMPLE.COM", Email = "naledi.maseko@example.com", NormalizedEmail = "NALEDI.MASEKO@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Maseko123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Naledi", Surname = "Maseko", IDNumber = "9107280023085", CellphoneNumber = "081 234 9876" },
                new ApplicationUser { Id = "customer-5", UserName = "pieter.vdm@example.com", NormalizedUserName = "PIETER.VDM@EXAMPLE.COM", Email = "pieter.vdm@example.com", NormalizedEmail = "PIETER.VDM@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("vanderMerwe123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Pieter", Surname = "van der Merwe", IDNumber = "8306195023082", CellphoneNumber = "072 345 1234" },
                new ApplicationUser { Id = "customer-6", UserName = "karabo.nkosi@example.com", NormalizedUserName = "KARABO.NKOSI@EXAMPLE.COM", Email = "karabo.nkosi@example.com", NormalizedEmail = "KARABO.NKOSI@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Nkosi123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Karabo", Surname = "Nkosi", IDNumber = "9701063123084", CellphoneNumber = "079 876 5432" },
                new ApplicationUser { Id = "customer-7", UserName = "annelise.botha@example.com", NormalizedUserName = "ANNELISE.BOTHA@EXAMPLE.COM", Email = "annelise.botha@example.com", NormalizedEmail = "ANNELISE.BOTHA@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Botha123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Annelise", Surname = "Botha", IDNumber = "8609031023086", CellphoneNumber = "078 234 5678" },
                new ApplicationUser { Id = "customer-8", UserName = "kagiso.motloung@example.com", NormalizedUserName = "KAGISO.MOTLOUNG@EXAMPLE.COM", Email = "kagiso.motloung@example.com", NormalizedEmail = "KAGISO.MOTLOUNG@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Motloung123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Kagiso", Surname = "Motloung", IDNumber = "0005016123080", CellphoneNumber = "084 567 8901" },
                new ApplicationUser { Id = "customer-9", UserName = "sibusiso.gumede@example.com", NormalizedUserName = "SIBUSISO.GUMEDE@EXAMPLE.COM", Email = "sibusiso.gumede@example.com", NormalizedEmail = "SIBUSISO.GUMEDE@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Gumede123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Sibusiso", Surname = "Gumede", IDNumber = "9204174123083", CellphoneNumber = "071 234 8901" },
                new ApplicationUser { Id = "customer-10", UserName = "michelle.pretorius@example.com", NormalizedUserName = "MICHELLE.PRETORIUS@EXAMPLE.COM", Email = "michelle.pretorius@example.com", NormalizedEmail = "MICHELLE.PRETORIUS@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Pretorius123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Michelle", Surname = "Pretorius", IDNumber = "8508232023088", CellphoneNumber = "073 456 7890" },
                new ApplicationUser { Id = "customer-11", UserName = "vusi.zulu@example.com", NormalizedUserName = "VUSI.ZULU@EXAMPLE.COM", Email = "vusi.zulu@example.com", NormalizedEmail = "VUSI.ZULU@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Zulu123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Vusi", Surname = "Zulu", IDNumber = "8803115123089", CellphoneNumber = "082 987 6543" },
                new ApplicationUser { Id = "customer-12", UserName = "aisha.jacobs@example.com", NormalizedUserName = "AISHA.JACOBS@EXAMPLE.COM", Email = "aisha.jacobs@example.com", NormalizedEmail = "AISHA.JACOBS@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Jacobs123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Aisha", Surname = "Jacobs", IDNumber = "9909020323082", CellphoneNumber = "079 123 4567" },
                new ApplicationUser { Id = "customer-13", UserName = "johan.deklerk@example.com", NormalizedUserName = "JOHAN.DEKLERK@EXAMPLE.COM", Email = "johan.deklerk@example.com", NormalizedEmail = "JOHAN.DEKLERK@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("deKlerk123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Johan", Surname = "de Klerk", IDNumber = "8702054023087", CellphoneNumber = "074 567 8901" },
                new ApplicationUser { Id = "customer-14", UserName = "thandiwe.sithole@example.com", NormalizedUserName = "THANDIWE.SITHOLE@EXAMPLE.COM", Email = "thandiwe.sithole@example.com", NormalizedEmail = "THANDIWE.SITHOLE@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Sithole123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Thandiwe", Surname = "Sithole", IDNumber = "9306203123086", CellphoneNumber = "078 987 6543" },
                new ApplicationUser { Id = "customer-15", UserName = "riaan.vw@example.com", NormalizedUserName = "RIAAN.VW@EXAMPLE.COM", Email = "riaan.vw@example.com", NormalizedEmail = "RIAAN.VW@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("vanWyk123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Riaan", Surname = "van Wyk", IDNumber = "8108305023081", CellphoneNumber = "071 345 6789" },
                new ApplicationUser { Id = "customer-16", UserName = "palesa.molefe@example.com", NormalizedUserName = "PALESA.MOLEFE@EXAMPLE.COM", Email = "palesa.molefe@example.com", NormalizedEmail = "PALESA.MOLEFE@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Molefe123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Palesa", Surname = "Molefe", IDNumber = "9501151023084", CellphoneNumber = "083 234 5678" },
                new ApplicationUser { Id = "customer-17", UserName = "kobus.smit@example.com", NormalizedUserName = "KOBUS.SMIT@EXAMPLE.COM", Email = "kobus.smit@example.com", NormalizedEmail = "KOBUS.SMIT@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Smit123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Kobus", Surname = "Smit", IDNumber = "7909094023083", CellphoneNumber = "072 987 1234" },
                new ApplicationUser { Id = "customer-18", UserName = "zanele.mthembu@example.com", NormalizedUserName = "ZANELE.MTHEMBU@EXAMPLE.COM", Email = "zanele.mthembu@example.com", NormalizedEmail = "ZANELE.MTHEMBU@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Mthembu123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Zanele", Surname = "Mthembu", IDNumber = "9604232123085", CellphoneNumber = "079 456 7890" },
                new ApplicationUser { Id = "customer-19", UserName = "annelise@example.com", NormalizedUserName = "ANNELISE@EXAMPLE.COM", Email = "annelise@example.com", NormalizedEmail = "ANNELISE@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Botha123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Annelise", Surname = "Botha", IDNumber = "8407125023080", CellphoneNumber = "074 123 4567" },
                new ApplicationUser { Id = "customer-20", UserName = "nomsa.ndlovu@example.com", NormalizedUserName = "NOMSA.NDLOVU@EXAMPLE.COM", Email = "nomsa.ndlovu@example.com", NormalizedEmail = "NOMSA.NDLOVU@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Ndlovu123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Nomsa", Surname = "Ndlovu", IDNumber = "9703280123088", CellphoneNumber = "081 567 8901" },

                // Pharmacists
                new ApplicationUser { Id = "pharmacist-1", UserName = "lindile@example.com", NormalizedUserName = "LINDILE@EXAMPLE.COM", Email = "lindile@example.com", NormalizedEmail = "LINDILE@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Hadebe123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Lindile", Surname = "Hadebe", IDNumber = "1234567890123", CellphoneNumber = "061 2345 678" },
                new ApplicationUser { Id = "pharmacist-2", UserName = "dorothy@example.com", NormalizedUserName = "DOROTHY@EXAMPLE.COM", Email = "dorothy@example.com", NormalizedEmail = "DOROTHY@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("Daniels123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Dorothy", Surname = "Daniels", IDNumber = "1234567890124", CellphoneNumber = "062 2345 678" },
                new ApplicationUser { Id = "pharmacist-3", UserName = "marcel@example.com", NormalizedUserName = "MARCEL@EXAMPLE.COM", Email = "marcel@example.com", NormalizedEmail = "MARCEL@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = GetPasswordHash("VanNiekerk123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Marcel", Surname = "Van Niekerk", IDNumber = "1234567890125", CellphoneNumber = "063 2345 678" },
                new ApplicationUser { Id = "pharmacist-4", UserName = "s224113038@mandela.ac.za", NormalizedUserName = "S224113038@MANDELA.AC.ZA", Email = "s224113038@mandela.ac.za", NormalizedEmail = "S224113038@MANDELA.AC.ZA", EmailConfirmed = true, PasswordHash = GetPasswordHash("Mamba123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Melusi", Surname = "Mamba", IDNumber = "2201461182425424", CellphoneNumber = "063 2345 678" },

                // Pharmacy Manager
                new ApplicationUser { Id = "manager-1", UserName = "s224378449@mandela.ac.za", NormalizedUserName = "S224378449@MANDELA.AC.ZA", Email = "s224378449@mandela.ac.za", NormalizedEmail = "S224378449@MANDELA.AC.ZA", EmailConfirmed = true, PasswordHash = GetPasswordHash("Majila123!"), SecurityStamp = Guid.NewGuid().ToString(), Name = "Mbasa", Surname = "Majila", IDNumber = "1234567890126", CellphoneNumber = "082 289 4758" }
            );

            // Seed User Roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                // Customer Roles
                new IdentityUserRole<string> { UserId = "customer-1", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-2", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-3", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-4", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-5", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-6", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-7", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-8", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-9", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-10", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-11", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-12", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-13", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-14", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-15", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-16", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-17", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-18", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-19", RoleId = "role-customer" },
                new IdentityUserRole<string> { UserId = "customer-20", RoleId = "role-customer" },

                // Pharmacist Roles
                new IdentityUserRole<string> { UserId = "pharmacist-1", RoleId = "role-pharmacist" },
                new IdentityUserRole<string> { UserId = "pharmacist-2", RoleId = "role-pharmacist" },
                new IdentityUserRole<string> { UserId = "pharmacist-3", RoleId = "role-pharmacist" },
                new IdentityUserRole<string> { UserId = "pharmacist-4", RoleId = "role-pharmacist" },

                // Manager Role
                new IdentityUserRole<string> { UserId = "manager-1", RoleId = "role-manager" }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustormerID = 1, ApplicationUserId = "customer-1" },
                new Customer { CustormerID = 2, ApplicationUserId = "customer-2" },
                new Customer { CustormerID = 3, ApplicationUserId = "customer-3" },
                new Customer { CustormerID = 4, ApplicationUserId = "customer-4" },
                new Customer { CustormerID = 5, ApplicationUserId = "customer-5" },
                new Customer { CustormerID = 6, ApplicationUserId = "customer-6" },
                new Customer { CustormerID = 7, ApplicationUserId = "customer-7" },
                new Customer { CustormerID = 8, ApplicationUserId = "customer-8" },
                new Customer { CustormerID = 9, ApplicationUserId = "customer-9" },
                new Customer { CustormerID = 10, ApplicationUserId = "customer-10" },
                new Customer { CustormerID = 11, ApplicationUserId = "customer-11" },
                new Customer { CustormerID = 12, ApplicationUserId = "customer-12" },
                new Customer { CustormerID = 13, ApplicationUserId = "customer-13" },
                new Customer { CustormerID = 14, ApplicationUserId = "customer-14" },
                new Customer { CustormerID = 15, ApplicationUserId = "customer-15" },
                new Customer { CustormerID = 16, ApplicationUserId = "customer-16" },
                new Customer { CustormerID = 17, ApplicationUserId = "customer-17" },
                new Customer { CustormerID = 18, ApplicationUserId = "customer-18" },
                new Customer { CustormerID = 19, ApplicationUserId = "customer-19" },
                new Customer { CustormerID = 20, ApplicationUserId = "customer-20" }
            );

            // Seed Pharmacists
            modelBuilder.Entity<Pharmacist>().HasData(
                new Pharmacist { PharmacistID = 1, ApplicationUserId = "pharmacist-1", HealthCouncilRegNo = "123456" },
                new Pharmacist { PharmacistID = 2, ApplicationUserId = "pharmacist-2", HealthCouncilRegNo = "234567" },
                new Pharmacist { PharmacistID = 3, ApplicationUserId = "pharmacist-3", HealthCouncilRegNo = "345678" },
                new Pharmacist { PharmacistID = 4, ApplicationUserId = "pharmacist-4", HealthCouncilRegNo = "146220" }
            );

            // Seed Pharmacy Manager
            modelBuilder.Entity<PharmacyManager>().HasData(
                new PharmacyManager { PharmacyManagerID = 1, ApplicationUserId = "manager-1", HealthCouncilRegNo = "134679" }
            );

            // Seed Customer Allergies
            modelBuilder.Entity<Custormer_Allergy>().HasData(
                new Custormer_Allergy { Custormer_AllergyID = 1, CustomerID = 2, Active_IngredientID = 11 }, // Lerato Khumalo - Glycetrol
                new Custormer_Allergy { Custormer_AllergyID = 2, CustomerID = 7, Active_IngredientID = 19 }, // Annelise Botha (1) - Nexorin
                new Custormer_Allergy { Custormer_AllergyID = 3, CustomerID = 7, Active_IngredientID = 28 }, // Annelise Botha (1) - Myvetrin
                new Custormer_Allergy { Custormer_AllergyID = 4, CustomerID = 7, Active_IngredientID = 30 }, // Annelise Botha (1) - Veltraxin
                new Custormer_Allergy { Custormer_AllergyID = 5, CustomerID = 12, Active_IngredientID = 14 }, // Aisha Jacobs - Phospholax
                new Custormer_Allergy { Custormer_AllergyID = 6, CustomerID = 12, Active_IngredientID = 6 }  // Aisha Jacobs - Aterolazine
            );

            // Seed Medications
            modelBuilder.Entity<Medication>().HasData(
                new Medication { MedcationID = 1, MedicationName = "CardioVex", Schedule = "6", CurrentPrice = 2999, DosageFormID = 1, SupplierID = 1, ReOrderLevel = 100, QuantityOnHand = 90 },
                new Medication { MedcationID = 2, MedicationName = "Neurocalm", Schedule = "2", CurrentPrice = 1550, DosageFormID = 1, SupplierID = 2, ReOrderLevel = 110, QuantityOnHand = 100 },
                new Medication { MedcationID = 3, MedicationName = "Allerfree Duo", Schedule = "0", CurrentPrice = 799, DosageFormID = 12, SupplierID = 3, ReOrderLevel = 150, QuantityOnHand = 100 },
                new Medication { MedcationID = 4, MedicationName = "GastroEase", Schedule = "3", CurrentPrice = 1999, DosageFormID = 1, SupplierID = 4, ReOrderLevel = 400, QuantityOnHand = 470 },
                new Medication { MedcationID = 5, MedicationName = "Respivent", Schedule = "3", CurrentPrice = 1000, DosageFormID = 1, SupplierID = 5, ReOrderLevel = 300, QuantityOnHand = 490 },
                new Medication { MedcationID = 6, MedicationName = "Dermagard", Schedule = "3", CurrentPrice = 5150, DosageFormID = 1, SupplierID = 2, ReOrderLevel = 600, QuantityOnHand = 790 },
                new Medication { MedcationID = 7, MedicationName = "Metaborex", Schedule = "4", CurrentPrice = 3699, DosageFormID = 1, SupplierID = 2, ReOrderLevel = 200, QuantityOnHand = 250 },
                new Medication { MedcationID = 8, MedicationName = "Sleeptraze", Schedule = "2", CurrentPrice = 1990, DosageFormID = 1, SupplierID = 2, ReOrderLevel = 100, QuantityOnHand = 110 },
                new Medication { MedcationID = 9, MedicationName = "OsteoFlex", Schedule = "3", CurrentPrice = 2350, DosageFormID = 3, SupplierID = 2, ReOrderLevel = 200, QuantityOnHand = 210 },
                new Medication { MedcationID = 10, MedicationName = "Immunexin", Schedule = "6", CurrentPrice = 6799, DosageFormID = 9, SupplierID = 2, ReOrderLevel = 200, QuantityOnHand = 190 },
                new Medication { MedcationID = 11, MedicationName = "CardioPlus", Schedule = "6", CurrentPrice = 4450, DosageFormID = 11, SupplierID = 2, ReOrderLevel = 500, QuantityOnHand = 600 },
                new Medication { MedcationID = 12, MedicationName = "AllerCalm", Schedule = "6", CurrentPrice = 3499, DosageFormID = 11, SupplierID = 2, ReOrderLevel = 400, QuantityOnHand = 410 },
                new Medication { MedcationID = 13, MedicationName = "RespirAid", Schedule = "6", CurrentPrice = 4150, DosageFormID = 9, SupplierID = 2, ReOrderLevel = 100, QuantityOnHand = 100 },
                new Medication { MedcationID = 14, MedicationName = "DermaClear", Schedule = "6", CurrentPrice = 6799, DosageFormID = 5, SupplierID = 2, ReOrderLevel = 100, QuantityOnHand = 200 },
                new Medication { MedcationID = 15, MedicationName = "OsteoPrime", Schedule = "6", CurrentPrice = 1999, DosageFormID = 2, SupplierID = 2, ReOrderLevel = 100, QuantityOnHand = 400 }
            );

            // Seed Medication Ingredients
            modelBuilder.Entity<Medication_Ingredient>().HasData(
                new Medication_Ingredient { Medication_IngredientID = 1, MedicationID = 1, Active_IngredientID = 6, Strength = "18mg" },
                new Medication_Ingredient { Medication_IngredientID = 2, MedicationID = 2, Active_IngredientID = 2, Strength = "2mg" },
                new Medication_Ingredient { Medication_IngredientID = 3, MedicationID = 2, Active_IngredientID = 3, Strength = "50mg" },
                new Medication_Ingredient { Medication_IngredientID = 4, MedicationID = 3, Active_IngredientID = 4, Strength = "325mg" },
                new Medication_Ingredient { Medication_IngredientID = 5, MedicationID = 3, Active_IngredientID = 5, Strength = "453.6g" },
                new Medication_Ingredient { Medication_IngredientID = 6, MedicationID = 4, Active_IngredientID = 1, Strength = "5mg" },
                new Medication_Ingredient { Medication_IngredientID = 7, MedicationID = 5, Active_IngredientID = 7, Strength = "100mg" },
                new Medication_Ingredient { Medication_IngredientID = 8, MedicationID = 5, Active_IngredientID = 8, Strength = "25mg" },
                new Medication_Ingredient { Medication_IngredientID = 9, MedicationID = 6, Active_IngredientID = 9, Strength = "1mg" },
                new Medication_Ingredient { Medication_IngredientID = 10, MedicationID = 6, Active_IngredientID = 10, Strength = "2mg" },
                new Medication_Ingredient { Medication_IngredientID = 11, MedicationID = 7, Active_IngredientID = 11, Strength = "10mg" },
                new Medication_Ingredient { Medication_IngredientID = 12, MedicationID = 8, Active_IngredientID = 12, Strength = "45mg" },
                new Medication_Ingredient { Medication_IngredientID = 13, MedicationID = 9, Active_IngredientID = 13, Strength = "200mg" },
                new Medication_Ingredient { Medication_IngredientID = 14, MedicationID = 9, Active_IngredientID = 14, Strength = "250mg" },
                new Medication_Ingredient { Medication_IngredientID = 15, MedicationID = 10, Active_IngredientID = 15, Strength = "20mg" },
                new Medication_Ingredient { Medication_IngredientID = 16, MedicationID = 10, Active_IngredientID = 16, Strength = "30mg" },
                new Medication_Ingredient { Medication_IngredientID = 17, MedicationID = 11, Active_IngredientID = 13, Strength = "50mg" },
                new Medication_Ingredient { Medication_IngredientID = 18, MedicationID = 11, Active_IngredientID = 6, Strength = "30mg" },
                new Medication_Ingredient { Medication_IngredientID = 19, MedicationID = 12, Active_IngredientID = 4, Strength = "50mg" },
                new Medication_Ingredient { Medication_IngredientID = 20, MedicationID = 13, Active_IngredientID = 7, Strength = "20mg" },
                new Medication_Ingredient { Medication_IngredientID = 21, MedicationID = 14, Active_IngredientID = 9, Strength = "20mg" },
                new Medication_Ingredient { Medication_IngredientID = 22, MedicationID = 15, Active_IngredientID = 13, Strength = "20mg" }
            );
        }

        // Add this helper method to your ApplicationDbContext class
        private string GetPasswordHash(string password)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            return passwordHasher.HashPassword(null, password);
        }
    }
}