using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _2_ASP.NET_MVC.Models;
using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productService = new ProductService();
        }

        public IActionResult Index()
        {
            List<Product> products = _productService.GetAllProduct();
            return View("Index", products);
        }

        public IActionResult Shop()
        {
            List<Product> products = _productService.GetAllProduct();
            return View("Shop", products);
        }

        public IActionResult ProductDetail(Guid id)
        {
            Product product = _productService.GetProductById(id);
            return View("ProductDetail", product);
        }

        public IActionResult Contact()
        {
            return View("Contact");
        }

        public IActionResult Blog()
        {
            return View("Blog");
        }
        public IActionResult BlogDetail()
        {
            return View("BlogDetail");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}