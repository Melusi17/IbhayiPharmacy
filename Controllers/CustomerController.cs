using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.CustomerVM;
using BCrypt.Net;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis.Scripting;

namespace IbhayiPharmacy.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new CustomerRegisterViewModel
            {
                AllergiesList = _context.Allergies
                    .Select(a => new SelectListItem
                    {
                        Value = a.AllergyId.ToString(),
                        Text = a.Name
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate dropdown if validation fails
                model.AllergiesList = _context.Allergies
                    .Select(a => new SelectListItem
                    {
                        Value = a.AllergyId.ToString(),
                        Text = a.Name
                    })
                    .ToList();

                return View(model);
            }

            // Create customer
            var customer = new Customer
            {
                Name = model.Name,
                Surname = model.Surname,
                IdNumber = model.IdNumber,
                Cellphone = model.Cellphone,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Save selected allergies
            if (!string.IsNullOrEmpty(model.SelectedAllergies))
            {
                var allergyIds = model.SelectedAllergies.Split(',')
                    .Select(int.Parse)
                    .ToList();

                foreach (var allergyId in allergyIds)
                {
                    _context.Customer_Allergies.Add(new CustomerAllergy
                    {
                        CustomerId = customer.CustomerID,
                        AllergyId = allergyId
                    });
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Login", "Customer");
        }

        //Dashboard
        public IActionResult CustomerDashboard()
        {
            return View();
        }

        //Upload Prescription
        public IActionResult UploadPrescription()
        {
            return View();
        }

        // Place Order
        public IActionResult PlaceOrder()
        {
            return View();
        }
        public IActionResult TrackOrder()
        {
            return View();
        }

        // Manage Repeats
        public IActionResult ManageRepeats()
        {
            return View();
        }

        // View Reports
        public IActionResult ViewReports()
        {
            return View();
        }

        // Profile
        public IActionResult Profile()
        {
            return View();
        }
    }
}
