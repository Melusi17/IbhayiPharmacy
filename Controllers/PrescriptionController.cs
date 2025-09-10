using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using Microsoft.AspNetCore.Mvc;

namespace IbhayiPharmacy.Controllers
{
    
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PrescriptionController(ApplicationDbContext db,IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
           _hostEnvironment = webHostEnvironment;


        }


        public IActionResult Index()
        {
            var objScript = _db.NewScripts.ToList();
            return View(objScript);
        }

        
        public IActionResult Upsert(int? id)
        {
            if(id==0||id==null)
            {   //create
                return View(new NewScript());
            }
            else
            {
                //update
                var obj = _db.NewScripts.FirstOrDefault(u => u.PrescriptionID == id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }

            
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(NewScript obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName).ToLower();

                    if (extension != ".pdf")
                    {
                        return BadRequest("Only PDF files are allowed.");
                    }

                    string filename = Path.GetFileName(file.FileName);

                    string uploadPath = Path.Combine(_hostEnvironment.WebRootPath, @"Documents/Scripts");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    string filePath = Path.Combine(uploadPath, filename);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        obj.Script = ms.ToArray();
                    }
                }

                if (obj.PrescriptionID == 0)
                {
                    _db.NewScripts.Add(obj);
                }
                else
                {
                    _db.NewScripts.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


    }
}
