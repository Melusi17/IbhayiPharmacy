using Microsoft.AspNetCore.Mvc;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace PharmMan.Controllers
{
    public class PharmacyManagerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PharmacyManagerController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View();
        }

        //Pharmacy info

        public IActionResult PharmacyInfo()
        {
            IEnumerable<Pharmacy> obj = _db.Pharmacies;
            return View(obj);
        }

        [HttpGet]
        public IActionResult AddPharmacyInfo()
        {
            ViewBag.Pharmacist = _db.Pharmacists;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPharmacyInfo(Pharmacy pharm)
        {
            if (ModelState.IsValid)
            {
                _db.Pharmacies.Add(pharm);
                _db.SaveChanges();
                return RedirectToAction("PharmacyInfo");

            }
            return View(pharm);
        }


        //Active ingredients
        public IActionResult ActiveIngredients()
        {
            return View();
        }
        [HttpGet]
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

        //Dosage Form

        public IActionResult DosageForms()
        {

            return View();
        }
        [HttpGet]
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
            return View();
        }
        [HttpGet]
        public IActionResult AddSuppliers()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSuppliers(Supplier sup)
        {
            if (ModelState.IsValid)
            {
                _db.Suppliers.Add(sup);
                _db.SaveChanges();
                return RedirectToAction("MedicationSuppliers");
            }
            return View(sup);
        }



        //Medication 

        public IActionResult ManageMedication()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddMedication()
        {
            //ViewBag.DosageForm = _db.DosageForms;
            //ViewBag.ActiveIngredients = _db.Active_Ingredients;
            //ViewBag.Suppliers = _db.Suppliers;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMedication(Medication med)
        {

            //if (ModelState.IsValid)
            //{
            //    // Save medication first
            //    _db.Medications.Add(med);
            //    _db.SaveChanges();


            //    var ingredients = med.ActiveIngredients.ToList();
            //    // Save related active ingredients
            //    foreach (var ingredient in ingredients)
            //    {
            //        _db.Medication_Ingredients.Add(new Medication_Ingredient
            //        {
            //            Active_IngredientID = ingredient.Active_IngredientID,
            //            Strength = ingredient.Strength,

            //        });
            //    }
            //    _db.SaveChanges();

            //    return RedirectToAction("ManageMedication");
            //}



            //ViewBag.DosageForm = _db.DosageForms;
            //ViewBag.ActiveIngredients = _db.Active_Ingredients;
            //ViewBag.Suppliers = _db.Suppliers;
            return View();
        }


        //Doctors
        public IActionResult ManageDoctor()
        {
            return View();
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

        //Pharmacists
        public IActionResult ManagePharmacists()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddPharmacists()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPharmacists(Pharmacist pham)
        {
            if (ModelState.IsValid)
            {
                _db.Pharmacists.Add(pham);
                _db.SaveChanges();
                return RedirectToAction("ManagePharmacists");
            }
            return View(pham);
        }


        //Orders
        public IActionResult ManageOrders()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddOrders()
        {
            ViewBag.Suppliers = _db.Suppliers;
            ViewBag.Medications = _db.Medications;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrders(Order order)
        {
            ViewBag.Suppliers = _db.Suppliers;
            return View();
        }




        public IActionResult StockManagement()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }



    }
}
