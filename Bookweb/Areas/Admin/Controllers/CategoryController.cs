
using Book.Dataaccess;
using Book.Dataaccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bookweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public CategoryController(IUnitofWork unitofwork)
        {
           _unitofwork=unitofwork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> Objcategorylist = _unitofwork.Category.GetAll();
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
            _unitofwork.Category.Add(Obj);
            _unitofwork.Save();
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
            var Obj = _unitofwork.Category.GetFirstorDefault(c => c.Id == Id);
            //var Obj = _db.Categories.Find(Id);

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
                _unitofwork.Category.Update(Obj);
                _unitofwork.Save();
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
            var Obj = _unitofwork.Category.GetFirstorDefault(c => c.Id == Id);
            return View(Obj);
        }

        //POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {
            var Obj = _unitofwork.Category.GetFirstorDefault(c => c.Id == Id);
            _unitofwork.Category.Remove(Obj);
                _unitofwork.Save();
                TempData["Success"] = "Sucess";
                return RedirectToAction("Index");
             
        }
    }
}
