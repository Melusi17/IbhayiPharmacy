using Microsoft.AspNetCore.Mvc;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using IbhayiPharmacy.Models.PharmacyManagerVM;
using Microsoft.Extensions.Options;
using SmtpSettings = IbhayiPharmacy.Models.PharmacyManagerVM.SmtpSettings;



namespace PharmMan.Controllers
{
    public class PharmacyManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _email;

        //public PharmacyManagerController(ApplicationDbContext db, IOptions<SmtpSettings> smtpSettings, EmailService email) //old version cauese error
        public PharmacyManagerController(ApplicationDbContext db, IOptions<IbhayiPharmacy.Models.SmtpSettings> smtpSettings, EmailService email)
        {
            _db = db;
            _email = email;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PharmacyInfo()
        {
            var pharmacies = _db.Pharmacies
           .Include(p => p.Pharmacist)
           .ThenInclude(ph => ph.ApplicationUser)
           .ToList();
            return View(pharmacies);
        }
        [HttpGet]
        public IActionResult AddPharmacyInfo()
        {
            var pharmacists = _db.Pharmacists
            .Include(p => p.ApplicationUser)
            .Select(p => new
            {
                p.PharmacistID,
                FullName = p.ApplicationUser != null
                    ? (p.ApplicationUser.Name + " " + p.ApplicationUser.Surname)
                    : "Unknown User"
            })
            .ToList();

            ViewBag.Pharmacists = new SelectList(pharmacists, "PharmacistID", "FullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPharmacyInfo(Pharmacy model)
        {
            if (ModelState.IsValid)
            {
                _db.Pharmacies.Add(model);
                _db.SaveChanges();
                return RedirectToAction("PharmacyInfo");
            }
            return View(model);
        }
        // GET: Pharmacy/EditPharmacyInfo/5
        public IActionResult EditPharmacyInfo(int id)
        {
            var pharmacy = _db.Pharmacies
                .Include(p => p.Pharmacist)
                .ThenInclude(ph => ph.ApplicationUser)
                .FirstOrDefault(p => p.PharmacyID == id);

            if (pharmacy == null)
            {
                return NotFound();
            }

            var pharmacists = _db.Pharmacists
                .Include(p => p.ApplicationUser)
                .Select(p => new
                {
                    p.PharmacistID,
                    FullName = p.ApplicationUser != null
                        ? (p.ApplicationUser.Name + " " + p.ApplicationUser.Surname)
                        : "Unknown"
                })
                .ToList();

            ViewBag.Pharmacists = new SelectList(pharmacists, "PharmacistID", "FullName", pharmacy.PharmacistID);

            return View(pharmacy);
        }

        // POST: Pharmacy/EditPharmacyInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPharmacyInfo(Pharmacy model)
        {
            
                _db.Pharmacies.Update(model);
                _db.SaveChanges();
                TempData["Success"] = "Pharmacy information updated successfully!";
            
    

            // Repopulate dropdown if validation fails
            var pharmacists = _db.Pharmacists
                .Include(p => p.ApplicationUser)
                .Select(p => new
                {
                    p.PharmacistID,
                    FullName = p.ApplicationUser != null
                        ? (p.ApplicationUser.Name + " " + p.ApplicationUser.Surname)
                        : "Unknown"
                })
                .ToList();

            ViewBag.Pharmacists = new SelectList(pharmacists, "PharmacistID", "FullName", model.PharmacistID);
            return RedirectToAction("PharmacyInfo");
        }






        public IActionResult ActiveIngredients()
        {
            IEnumerable<Active_Ingredient> active = _db.Active_Ingredients.ToList();
            return View(active);
        }

        public IActionResult AddActiveIngredients()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddActiveIngredients(Active_Ingredient active)
        {
            if (ModelState.IsValid)
            {
                _db.Active_Ingredients.Add(active);
                _db.SaveChanges();
                return RedirectToAction("ActiveIngredients");
            }

            return View(active);
        }






        //Dosage Forms
        public IActionResult DosageForms()
        {
            IEnumerable<DosageForm> forms = _db.DosageForms;
            return View(forms);
        }
        public IActionResult AddDosageForms()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDosageForms(DosageForm form)
        {
            if (ModelState.IsValid)
            {
                _db.DosageForms.Add(form);
                _db.SaveChanges();
                return RedirectToAction("DosageForms");
            }
            return View(form);
        }






        //Supplier
        public IActionResult MedicationSuppliers()
        {
            IEnumerable<Supplier> supplier = _db.Suppliers;
            return View(supplier);
        }
        [HttpGet]
        public IActionResult AddSuppliers()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSuppliers(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _db.Suppliers.Add(supplier);
                _db.SaveChanges();
                return RedirectToAction("MedicationSuppliers");
            }
            return View(supplier);
        }



        [HttpGet]
        public IActionResult ManageMedication()
        {
            var med = _db.Medications
         .Include(m => m.DosageForm)
         .Include(m => m.Supplier)
         .Include(m => m.Medication_Ingredients)
             .ThenInclude(mi => mi.Active_Ingredients) // ✅ this line is key
         .ToList();

            return View(med);
        }

        public IActionResult AddMedication()
        {
            ViewBag.DosageForms = new SelectList(_db.DosageForms, "DosageFormID", "DosageFormName");
            ViewBag.Suppliers = new SelectList(_db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.ActiveIngredients = _db.Active_Ingredients.ToList(); // optional for multi-select
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMedication(Medication med, List<MedicationIngredientVM> Ingredients)
        {
            if (med != null)
            {
                foreach (var ing in Ingredients)
                {
                    med.Medication_Ingredients.Add(new Medication_Ingredient
                    {
                        Active_IngredientID = ing.Active_IngredientID,
                        Strength = ing.Strength
                    });
                }

                _db.Medications.Add(med);
                _db.SaveChanges();
                return RedirectToAction("ManageMedication");
            }

            ViewBag.DosageForms = new SelectList(_db.DosageForms, "DosageFormID", "DosageFormName");
            ViewBag.Suppliers = new SelectList(_db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.ActiveIngredients = _db.Active_Ingredients.ToList();
            return View(med);
        }














        //Doctors
        public IActionResult ManageDoctor()
        {
            IEnumerable<Doctor> doctors = _db.Doctors;
            return View(doctors);
        }
        [HttpGet]
        public IActionResult AddDoctor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDoctor(Doctor doc)
        {
            if (ModelState.IsValid)
            {
                _db.Doctors.Add(doc);
                _db.SaveChanges();
                return RedirectToAction("ManageDoctor");
            }
            return View(doc);
        }

        public IActionResult EditDoctor(int id)
        {
            var doctor = _db.Doctors.FirstOrDefault(d => d.DoctorID == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDoctor(Doctor doc)
        {
            if (ModelState.IsValid)
            {
                _db.Doctors.Update(doc);
                _db.SaveChanges();
                return RedirectToAction("ManageDoctor");
            }
            return View(doc);
        }

        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _db.Doctors.FirstOrDefault(d => d.DoctorID == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Doctor doc)
        {
            
            var doctor = _db.Doctors.FirstOrDefault(d => d.DoctorID == doc.DoctorID);
            if (doctor != null)
            {
                _db.Doctors.Remove(doctor);
                _db.SaveChanges();
                return RedirectToAction("ManageDoctor");
            }
            return View(doc);
        }





        //Pharmacists
        public IActionResult ManagePharmacists()
        {
            IEnumerable<Pharmacist> pharmacist = _db.Pharmacists.Include(p => p.ApplicationUser).ToList();
            return View(pharmacist);
        }
        public IActionResult AddPharmacists()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPharmacists(Pharmacist pham)
        {
            pham.ApplicationUser.UserName = pham.ApplicationUser.Email;

            _db.Pharmacists.Add(pham);
            _db.SaveChanges();




                var pharmacist = _db.Pharmacists
            .FirstOrDefault(s => s.PharmacistID == pham.PharmacistID);

            if (pharmacist != null && !string.IsNullOrEmpty(pharmacist.ApplicationUser.Email))
            {
                var receiver = pharmacist.ApplicationUser.Email;
                var subject = "PHARMACIST LOGIN DETAILS";




                var message = "<h3>Temporary Password</h3>";
                message += $"<p>Dear {pharmacist.ApplicationUser.Name},</p>";
                message += $"<p>Your temporary password is: {pharmacist.ApplicationUser.PasswordHash}</p>";
                message += $"</br>";
                message += $"<p>Best regards</p>";

                _email.SendEmailAsync(receiver, subject, message);
            }





            return RedirectToAction("ManagePharmacists");


        }
        [HttpGet]
        public IActionResult EditPharmacist(int id)
        {
            var pharmacist = _db.Pharmacists
                                .Include(p => p.ApplicationUser)
                                .FirstOrDefault(p => p.PharmacistID == id);
            if (pharmacist == null) return NotFound();

            return View(pharmacist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPharmacist(Pharmacist pham)
        {
            if (!ModelState.IsValid) return View(pham);

            if (ModelState.IsValid)
            {
                _db.Pharmacists.Update(pham);
                _db.SaveChanges();
                return RedirectToAction("ManageDoctor");
            }

            _db.SaveChanges();
            return RedirectToAction("ManagePharmacists");
        }

        public IActionResult DeletePharmacist(int id)
        {

            var doctor = _db.Pharmacists.FirstOrDefault(d => d.PharmacistID == id);
            if (doctor != null)
            {
                _db.Pharmacists.Remove(doctor);
                _db.SaveChanges();
                return RedirectToAction("ManagePharmacists");
            }
            return View("ManagePharmacists");
        }












        //Orders
        public IActionResult ManageOrders()
        {
            // Include customer, pharmacist, and order lines with medications & suppliers
            var orders = _db.StockOrders
        .Select(o => new StockOrderVM
        {
            StockOrder = o,
            OrderLines = _db.StockOrderDetails
                .Where(d => d.StockOrderID == o.StockOrderID)
                .Include(d => d.Medication)
                .ThenInclude(m => m.Supplier)
                .ToList()
        })
        .ToList();

            return View(orders);
        }












        [HttpGet]
        public IActionResult Orders()
        {
            ViewBag.Suppliers = _db.Suppliers.ToList();
            ViewBag.Medications = _db.Medications.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Orders(StockOrderVM order)
        {
            ViewBag.Suppliers = _db.Suppliers.ToList();
            ViewBag.Medications = _db.Medications.ToList();

            if (order.OrderLines == null || !order.OrderLines.Any())
            {
                ModelState.AddModelError("", "Please add at least one medication.");
            }

          
       
            order.StockOrder.OrderDate = DateTime.Now;
            order.StockOrder.Status = "Ordered";

            _db.StockOrders.Add(order.StockOrder);
            _db.SaveChanges();

            foreach (var line in order.OrderLines)
            {
                if (line.MedicationID != 0 && line.OrderQuantity > 0)
                {
                    line.StockOrderID = order.StockOrder.StockOrderID;
                    _db.StockOrderDetails.Add(line);
                }
            }


            var supplier = _db.Suppliers
        .FirstOrDefault(s => s.SupplierID == order.StockOrder.SupplierID);

            if (supplier != null && !string.IsNullOrEmpty(supplier.EmailAddress))
            {
                var receiver = supplier.EmailAddress; // ✅ use the email address
                var subject = "Medication Order";

                 


                var message = "<h3>New Medication Order</h3>";
                message += $"<p>Dear {supplier.SupplierName},</p>";
                message += "<p>The following medications have been ordered:</p>";
                message += "<ul>";

                // Loop through medications
                foreach (var line in order.OrderLines)
                {
                    var medication = _db.Medications.FirstOrDefault(m => m.MedcationID == line.MedicationID);
                    if (medication != null)
                    {
                        message += $"<li>{medication.MedicationName} - Quantity: {line.OrderQuantity}</li>";
                    }
                }
                message += "</ul>";
                message += "<p>Please confirm and prepare the order for delivery.</p>";
                message += "<p>Kind regards,<br/>Ibhayi Pharmacy</p>";

                 _email.SendEmailAsync(receiver, subject, message);
            }

             

            _db.SaveChanges();

            return RedirectToAction("ManageOrders");
        }


        public IActionResult StockManagement()
        {
            var meds = _db.Medications
            .Include(m => m.DosageForm)
            .ToList();

            return View(meds);
        }
        [HttpPost]
        public IActionResult UpdateStock(int id, int? newStock, int? increment)
        {
            var med = _db.Medications.FirstOrDefault(m => m.MedcationID == id);
            if (med == null)
                return NotFound();

            if (newStock.HasValue)
                med.QuantityOnHand = newStock.Value;
            else if (increment.HasValue)
                med.QuantityOnHand += increment.Value;

            _db.SaveChanges();
            return RedirectToAction("StockManagement");
        }
        [HttpGet]
        public IActionResult Reports(string groupBy = "")
        {
            // Retrieve all medication data
            var medications = _db.Medications
                .Include(m => m.Supplier)
                .Include(m => m.DosageForm)
                .ToList();

            var model = new MedicationReportVM
            {
                GroupBy = groupBy,
                Groups = new List<MedicationGroup>()
            };

            if (!string.IsNullOrEmpty(groupBy))
            {
                switch (groupBy)
                {
                    case "Supplier":
                        model.Groups = medications
                            .GroupBy(m => m.Supplier.SupplierName)
                            .Select(g => new MedicationGroup
                            {
                                GroupName = g.Key,
                                Medications = g.ToList()
                            }).ToList();
                        break;

                    case "DosageForm":
                        model.Groups = medications
                            .GroupBy(m => m.DosageForm.DosageFormName)
                            .Select(g => new MedicationGroup
                            {
                                GroupName = g.Key,
                                Medications = g.ToList()
                            }).ToList();
                        break;

                    case "Schedule":
                        model.Groups = medications
                            .GroupBy(m => m.Schedule)
                            .Select(g => new MedicationGroup
                            {
                                GroupName = g.Key,
                                Medications = g.ToList()
                            }).ToList();
                        break;

                    default:
                        model.Groups = new List<MedicationGroup>
                {
                    new MedicationGroup
                    {
                        GroupName = "All Medications",
                        Medications = medications
                    }
                };
                        break;
                }
            }

            return View(model);
        }




    }
}
