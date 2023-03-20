using _2_ASP.NET_MVC.Models;
using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            _productService = new ProductService();
        }
        public IActionResult Product()
        {
            List<Product> products = _productService.GetAllProduct();
            return View("Product", products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (_productService.CreateProduct(product))
            {
                return RedirectToAction("Redirect");
            }
            return View();
        }
        public IActionResult Edit(Product product)
        {
            if (_productService.UpdateProduct(product))
            {
                return RedirectToAction("Redirect");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Product product = _productService.GetProductById(id);
            return View(product);
        }
        public IActionResult Details(Guid id)
        {
            Product product = _productService.GetProductById(id);
            return View(product);
        }
        public IActionResult Delete(Guid id)
        {
            if (_productService.DeleteProduct(id))
            {
                return RedirectToAction("Redirect");
            }
            return View("Product");
        }
        public IActionResult Redirect()
        {
            return RedirectToAction("Product");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
