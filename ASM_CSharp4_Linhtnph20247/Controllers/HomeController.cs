using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _2_ASP.NET_MVC.Models;
using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using ASM_CSharp4_Linhtnph20247.ViewModel;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ISizeService _sizeService;
        private readonly IBrandService _brandService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productService = new ProductService();
            _sizeService = new SizeService();
            _brandService = new BrandService();
        }

        public IActionResult Index()
        {
            var viewModel = new ProductViewModel();
            viewModel.Products = _productService.GetAllProduct();
            return View("Index", viewModel);
        }

        public IActionResult Shop()
        {
            var viewModel = new ProductViewModel();
            viewModel.Products = _productService.GetAllProduct();
            return View("Shop", viewModel);
        }
        [HttpGet]
        public IActionResult ProductDetail(Guid id)
        {
            Product product = _productService.GetProductById(id);
            Brand brand = _brandService.GetBrandById(product.BrandId);
            Size size = _sizeService.GetSizeById(product.SizeId);
            var viewModel = new ProductViewModel()
            {
                Product = product,
                Size = size,
                Brand = brand
            };
            return View("ProductDetail", viewModel);
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