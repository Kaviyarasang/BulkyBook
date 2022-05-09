
using Book.Dataaccess;
using Book.Dataaccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bookweb.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public CoverTypeController(IUnitofWork unitofwork)
        {
           _unitofwork=unitofwork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> ObjCoverTypelist = _unitofwork.CoverType.GetAll();
            return View(ObjCoverTypelist);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType Obj)
        {

            if (ModelState.IsValid)
            {
            _unitofwork.CoverType.Add(Obj);
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
            var Obj = _unitofwork.CoverType.GetFirstorDefault(c => c.Id == Id);
            //var Obj = _db.Categories.Find(Id);

            return View(Obj);
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType Obj)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.CoverType.Update(Obj);
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
            var Obj = _unitofwork.CoverType.GetFirstorDefault(c => c.Id == Id);
            return View(Obj);
        }

        //POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {
            var Obj = _unitofwork.CoverType.GetFirstorDefault(c => c.Id == Id);
            _unitofwork.CoverType.Remove(Obj);
                _unitofwork.Save();
                TempData["Success"] = "Sucess";
                return RedirectToAction("Index");
             
        }
    }
}
