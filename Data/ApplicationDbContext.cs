using IbhayiPharmacy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace IbhayiPharmacy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pharmacist> Pharmacists { get; set; }
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
        public DbSet<User> Users { get; set; }
        public DbSet<StockOrder> StockOrders { get; set; }
        public DbSet<StockOrderDetail> StockOrderDetails { get; set; }
        public DbSet<ScriptLine> ScriptLines { get; set; }

    }
}
