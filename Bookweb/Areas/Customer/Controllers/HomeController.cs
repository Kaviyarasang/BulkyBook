using Book.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Book.Dataaccess.Repository;
using Book.Dataaccess.Repository.IRepository;

namespace Bookweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofwork;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofwork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitofwork.Product.GetAll();
            return View(products);
        }

        public IActionResult Details(int Id)
        {
            ShoppingCart obj = new()
            {
                count = 1,
                Product=_unitofwork.Product.GetFirstorDefault(u=>u.Id==Id,IncludeProperties:"Category,CoverType"),


            };

            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}