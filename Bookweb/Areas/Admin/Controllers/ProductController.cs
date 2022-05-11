
using Book.Dataaccess;
using Book.Dataaccess.Repository.IRepository;
using Book.Models;
using Book.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bookweb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitofWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
           _unitofwork=unitofwork;
            _webHostEnvironment=webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> ObjProductlist = _unitofwork.Product.GetAll();
            return View(ObjProductlist);
        }
        //GET
       
        //GET
        public IActionResult Upsert(int? Id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product=new (),
                CategoryList = _unitofwork.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),
                    }),
                CoverTypeList = _unitofwork.CoverType.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),
                    }),

            };



            if (Id==null||Id==0)
            {
                return View(productVM);

            }
            else
            {

            }

            return View(productVM);
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM Obj,IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                if (file!=null)
                {
                    var uploads=Path.Combine(wwwRoothPath, @"images\products");
                    string filename=Guid.NewGuid().ToString();
                    var extension=Path.GetExtension(file.FileName);

                    using (var filestreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }
                    Obj.Product.ImageUrl = @"images\products" + filename + extension;
                }


                _unitofwork.Product.Add(Obj.Product);
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
            var Obj = _unitofwork.Product.GetFirstorDefault(c => c.Id == Id);
            return View(Obj);
        }

        //POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {
            var Obj = _unitofwork.Product.GetFirstorDefault(c => c.Id == Id);
            _unitofwork.Product.Remove(Obj);
                _unitofwork.Save();
                TempData["Success"] = "Sucess";
                return RedirectToAction("Index");
             
        }

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productlist = _unitofwork.Product.GetAll();

            return Json(new { data = productlist });
        }

        #endregion



    }
}
