
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
                productVM.Product = _unitofwork.Product.GetFirstorDefault(u=>u.Id==Id);
                return View(productVM);
            }

            
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

                    if(Obj.Product.ImageUrl!=null)
                    {
                        var imagepath= Path.Combine(wwwRoothPath, Obj.Product.ImageUrl.Trim('\\'));
                        if(System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }
                    }
                    

                    using (var filestreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }
                    Obj.Product.ImageUrl = @"images\products" + filename + extension;
                }

                if(Obj.Product.Id==0)
                {
                    _unitofwork.Product.Add(Obj.Product);
                }
                else
                {
                    _unitofwork.Product.Update(Obj.Product);
                }

                
                _unitofwork.Save();
                TempData["Success"] = "Sucess";
                return RedirectToAction("Index");
            }
            return View(Obj);
        }


        //POST

        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var productlist = _unitofwork.Product.GetAll(IncludeProperties:"Category,CoverType");

            return Json(new { data = productlist });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitofwork.Product.GetFirstorDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitofwork.Product.Remove(obj);
            _unitofwork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



    }
}
