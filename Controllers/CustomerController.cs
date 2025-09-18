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
using Microsoft.EntityFrameworkCore;

namespace IbhayiPharmacy.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        //REGISTER NEW CUSTOMER
        // GET: Register
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
                    }).ToList()

                
            };

            return View(viewModel);
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterViewModel model)
        {
            // Debug log: check that POST is triggered
            Console.WriteLine("POST triggered!");
            Console.WriteLine($"Name: {model.Name}, Email: {model.Email}, SelectedAllergies: {model.SelectedAllergies}");

            // Check model validation
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid:");
                foreach (var e in ModelState.Values.SelectMany(v => v.Errors))
                    Console.WriteLine(e.ErrorMessage);

                // Repopulate allergy dropdown
                model.AllergiesList = _context.Allergies
                    .Select(a => new SelectListItem { Value = a.AllergyId.ToString(), Text = a.Name })
                    .ToList();

                return View(model);
            }

            // Create the customer
            var customer = new Customer
            {
                Name = model.Name,
                Surname = model.Surname,
                IdNumber = model.IdNumber,
                Cellphone = model.Cellphone,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            // Add customer to DB
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Customer saved with ID: {customer.CustomerID}");

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
                Console.WriteLine($"Saved {allergyIds.Count} allergies for customer {customer.CustomerID}");
            }

            // Redirect to Login page
            return RedirectToAction("Login", "Customer");
        }








        // DASHBOARD
        public IActionResult CustomerDashboard()
        {
            return View();
        }







        // UPLOAD PRESCRIPTION
        [HttpGet]
        public async Task<IActionResult> UploadPrescription()
        {
            var model = new UploadPrescriptionViewModel
            {
                UnprocessedPrescriptions = _context.Prescriptions
                .Where(p => !p.IsProcessed)
                .ToList(),
                ProcessedPrescriptions = _context.Prescriptions
                .Where(p => p.IsProcessed)
                .ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPrescription(IFormFile prescriptionFile, bool dispenseCheckbox)
        {
            if (prescriptionFile == null || prescriptionFile.Length == 0)
            {
                TempData["Error"] = "Please select a prescription file.";
                return RedirectToAction("UploadPrescription");
            }

            using var ms = new MemoryStream();
            await prescriptionFile.CopyToAsync(ms);

            var prescription = new Prescription
            {

                //CustomerID = 1/* Get logged-in customer ID */,
                //DoctorID = 1 /* Assign doctor ID */,
                //PharmacistID = 1 /* Assign pharmacist ID */,
                DateIssued = DateTime.Now,
                Script = ms.ToArray(),
                FileName = prescriptionFile.FileName,
                ContentType = prescriptionFile.ContentType,
                IsProcessed = false,


            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Prescription uploaded successfully!";
            return RedirectToAction("UploadPrescription");
        }

        public async Task<IActionResult> DownloadPrescription(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);

            if (prescription == null || prescription.Script == null)
                return NotFound();

            return File(prescription.Script, prescription.ContentType, prescription.FileName);
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
