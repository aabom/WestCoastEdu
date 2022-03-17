using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;
using WestCoastEdu.Models.ViewModels;

namespace WestCoastEdu.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Location,Status");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id, includeProperties: "Location,Status")

            };
            return View(cartObj);
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