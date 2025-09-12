using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IbhayiPharmacy.Controllers
{
    public class ScriptsProcessedController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ScriptsProcessedController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Prescription> allScripts = _db.Prescriptions
                .Include(p => p.ApplicationUser)
                .ToList();


    //        Prescription prescription = _db.Prescriptions
    //.Include(p => p.ApplicationUser)  // customer/user
    //.Include(p => p.Doctor)           // doctor
    //.Include(p => p.Medication)       // medication
    //.FirstOrDefault(p => p.PrescriptionID == id);



            return View(allScripts); 
        }

       
        public IActionResult Edit(int id)
        {
           
            Prescription prescription = _db.Prescriptions
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return NotFound();
            }
            var CustomerScriptsVM = new CustomerScriptsVM
            {
                Prescr = prescription.PrescriptionID,
                Name = prescription.ApplicationUser.Name,
                Surname = prescription.ApplicationUser.Surname,
                IDNumber=prescription.ApplicationUser.IDNumber,
                ScriptList = new List<Prescription> { prescription },

            
            };

            return View(CustomerScriptsVM);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerScriptsVM CustomerScriptsVM)
        {
            if (ModelState.IsValid)
            {
                
                Prescription prescription = _db.Prescriptions
               .FirstOrDefault(p => p.PrescriptionID == CustomerScriptsVM.Prescr);

                if (prescription == null)
                {
                    return NotFound();
                }
                prescription.Status = "Processed";
                prescription.DateIssued = DateTime.Now;
                _db.SaveChanges();
                TempData["Success"] = "Prescription processed successfully!";
                return RedirectToAction(nameof(Index));
            }
            
           return View();
        }
    }
}
