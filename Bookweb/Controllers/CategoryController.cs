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

        public IActionResult Create()
        {

            return View();
        }
    }
}
