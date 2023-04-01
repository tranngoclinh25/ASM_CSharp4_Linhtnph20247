using _2_ASP.NET_MVC.Models;
using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using ASM_CSharp4_Linhtnph20247.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ISizeService _sizeService;
        private readonly IBrandService _brandService;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            _productService = new ProductService();
            _sizeService = new SizeService();
            _brandService = new BrandService();
        }
        public IActionResult Product()
        {
            var viewModel = new ProductViewModel();
            viewModel.Products = _productService.GetAllProduct();
            return View("Product",viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new ProductViewModel
            {
                Sizes = _sizeService.GetAllSizes().Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),
                Brands = _brandService.GetAllBrand().Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList()
        };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            //// Kiểm tra xem thông tin sản phẩm mới có hợp lệ không
            //if (ModelState.IsValid)
            //{
                var product = new Product()
                {
                    Name = model.Name,
                    SizeId = model.SizeId,
                    BrandId = model.BrandId,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Status = model.Status
                };
                if (_productService.CreateProduct(product))
                {
                    return RedirectToAction("Redirect");
                }

                var brand = _brandService.GetBrandById(model.BrandId);
                model.BrandName = brand.Name;
                model.Brands = _brandService.GetAllBrand().Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList();
                var size = _sizeService.GetSizeById(model.SizeId);
                model.SizeName = size.Name;
                model.Sizes = _sizeService.GetAllSizes().Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
            //}
            return View(model);
        }
        public IActionResult Edit(ProductViewModel model, Guid id)
        {
            if (_productService.UpdateProduct(model, id))
            {
                return RedirectToAction("Redirect");
            }
            model.Brands = _brandService.GetAllBrand().Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();
            model.Sizes = _sizeService.GetAllSizes().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Product product = _productService.GetProductById(id);
            Brand brand = _brandService.GetBrandById(product.BrandId);
            Size size = _sizeService.GetSizeById(product.SizeId);
            var viewModel = new ProductViewModel
            {
                Product = product,
                Name = product.Name,
                BrandId = product.BrandId,
                SizeId = product.SizeId,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                Status = product.Status,
                Brand = brand,
                Size = size,
                Sizes = _sizeService.GetAllSizes().Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),
                SizeName = size.Name,
                Brands = _brandService.GetAllBrand().Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList(),
                BrandName = brand.Name,
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Details(Guid id)
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
            return View(viewModel);
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
