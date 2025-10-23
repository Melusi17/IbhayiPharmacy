using Microsoft.AspNetCore.Mvc;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;


namespace PharmMan.Controllers
{
    public class PharmacyManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public PharmacyManagerController(ApplicationDbContext db)
        {
            _db = db;
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
            if (ModelState.IsValid)
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

            _db.Pharmacists.Add(pham);
            _db.SaveChanges();
            return RedirectToAction("ManagePharmacists");


        }

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














        //Orders
        public IActionResult ManageOrders()
        {
            // Include customer, pharmacist, and order lines with medications & suppliers
            var orders = _db.Orders
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.Medications)
                        .ThenInclude(m => m.Supplier)
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
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
        public IActionResult Orders(Order order, List<OrderLine> OrderLines)
        {
            order.OrderDate = DateTime.Now;
            order.Status = "Ordered";
            if (ModelState.IsValid)
            {
                foreach (var line in OrderLines)
                {
                    if (line.MedicationID != 0 && line.Quantity > 0)
                        order.OrderLines.Add(line);
                }

                order.OrderDate = DateTime.Now;
                order.Status = "Ordered";
                order.OrderNumber = $"ORD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4)}";

                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("ManageOrders");
            }

            ViewBag.Customers = new SelectList(_db.Customers, "CustomerID", "FullName");
            ViewBag.Pharmacists = new SelectList(_db.Pharmacists, "PharmacistID", "FullName");
            ViewBag.Medications = _db.Medications.ToList();
            return View(order);
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
        public IActionResult Reports()
        {
            var model = new MedicationReportVM
            {
                GroupBy = "", // default
                Groups = new List<MedicationGroup>() // initialize to avoid null
            };
            return View(model);
        }
            [HttpPost]
        public IActionResult Reports(string groupBy)
        {
            var medications = _db.Medications
                .Include(m => m.Supplier)
                .Include(m => m.DosageForm)
                .ToList();

            var model = new MedicationReportVM
            {
                GroupBy = groupBy
            };

            if (string.IsNullOrEmpty(groupBy))
            {
                model.Groups.Add(new MedicationGroup
                {
                    GroupName = "All Medications",
                    Medications = medications
                });
            }
            else
            {
                switch (groupBy.ToLower())
                {
                    case "dosageform":
                        model.Groups = medications
                            .GroupBy(m => m.DosageForm.DosageFormName)
                            .Select(g => new MedicationGroup
                            {
                                GroupName = g.Key,
                                Medications = g.ToList()
                            })
                            .ToList();
                        break;

                    case "schedule":
                        model.Groups = medications
                            .GroupBy(m => m.Schedule)
                            .Select(g => new MedicationGroup
                            {
                                GroupName = g.Key,
                                Medications = g.ToList()
                            })
                            .ToList();
                        break;

                    case "supplier":
                        model.Groups = medications
                            .GroupBy(m => m.Supplier.SupplierName)
                            .Select(g => new MedicationGroup
                            {
                                GroupName = g.Key,
                                Medications = g.ToList()
                            })
                            .ToList();
                        break;

                    default:
                        model.Groups.Add(new MedicationGroup
                        {
                            GroupName = "All Medications",
                            Medications = medications
                        });
                        break;
                }
            }

            return View(model);
        }

    }
}
