using Bookweb.DATA;
using Bookweb.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bookweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbcontext _db;
        public CategoryController(ApplicationDbcontext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> Objcategorylist = _db.Categories;
            return View(Objcategorylist);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category Obj)
        {
            if (Obj.Name == Obj.Displayorder.ToString())
            {
                ModelState.AddModelError("Customerror","Name and Display order are same");
            }
            if (ModelState.IsValid)
            {
            _db.Categories.Add(Obj);
            _db.SaveChanges();
            TempData["Success"] = "Sucess";
            return RedirectToAction("Index");
           }
            return View(Obj);
        }


        //GET
        public IActionResult Edit(int? Id)
        {
            if(Id==null||Id==0)
                return NotFound();
            //var obj = _db.Categories.FirstOrDefault(c => c.Id == Id);
            var Obj = _db.Categories.Find(Id);
            return View(Obj);
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category Obj)
        {
            if (Obj.Name == Obj.Displayorder.ToString())
            {
                ModelState.AddModelError("Customerror", "Name and Display order are same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Obj);
                _db.SaveChanges();
                TempData["Success"] = "Sucess";
                return RedirectToAction("Index");
            }
            return View(Obj);
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();
            //var obj = _db.Categories.FirstOrDefault(c => c.Id == Id);
            var Obj = _db.Categories.Find(Id);
            return View(Obj);
        }

        //POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {
                var Obj= _db.Categories.Find(Id);
                _db.Categories.Remove(Obj);
                _db.SaveChanges();
                TempData["Success"] = "Sucess";
                return RedirectToAction("Index");
             
        }
    }
}
