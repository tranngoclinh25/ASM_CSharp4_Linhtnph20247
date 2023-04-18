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
        public IActionResult Create(ProductViewModel model, IFormFile imageFile)
        {
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
            if (imageFile != null && imageFile.Length > 0) //Không null và ko trống
            {
                //Trỏ tới thư mục wwwroot để lát nữa thực hiện Copy sang
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Styles/img/product", imageFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    //Thức hiện Copy ảnh vừa chọn sang thư mục mới (wwwroot)
                    imageFile.CopyTo(stream);
                }
                //Gán lại giá trị ImageUrl của đối tượng bằng file ảnh đã Copy
                product.ImageUrl = imageFile.FileName;
            }
            var products = _productService.GetAllProduct();
            foreach (var item in products)
            {
                if (item.Name == model.Name && item.BrandId == model.BrandId)
                {
                    var thongbao = "(Name + Brand) already exists!";
                    TempData["ThongBaoCreate"] = thongbao;
                    return RedirectToAction("Create", new { thongbao });
                }
            }
            if (_productService.CreateProduct(product))
                    return RedirectToAction("Redirect");
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
            return View(model);
        }
        public IActionResult Edit(ProductViewModel model, Guid id, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0) //Không null và ko trống
            {
                //Trỏ tới thư mục wwwroot để lát nữa thực hiện Copy sang
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Styles/img/product", imageFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    //Thức hiện Copy ảnh vừa chọn sang thư mục mới (wwwroot)
                    imageFile.CopyTo(stream);
                }
                //Gán lại giá trị ImageUrl của đối tượng bằng file ảnh đã Copy
                model.ImageUrl = imageFile.FileName;
            }
            else
            {
                model.ImageUrl = _productService.GetProductById(id).ImageUrl;
            }
            //var product = _productService.GetProductById(id);
            //if (product.Name != model.Name || product.BrandId != model.BrandId)
            //{
            //    var products = _productService.GetAllProduct();
            //    foreach (var item in products)
            //    {
            //        if (item.Name == model.Name && item.BrandId == model.BrandId)
            //        {
            //            var thongbao = "(Name + Brand) already exists!";
            //            TempData["ThongBaoUpdate"] = thongbao;
            //            return RedirectToAction("Edit", new { thongbao });
            //        }
            //    }
            //}
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
                Brand = product.Brand,
                Size = product.Size,
                Sizes = _sizeService.GetAllSizes().Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),
                SizeName = product.Size.Name,
                Brands = _brandService.GetAllBrand().Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList(),
                BrandName = product.Brand.Name,
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Product product = _productService.GetProductById(id);
            var viewModel = new ProductViewModel()
            {
                Product = product,
                Size = product.Size,
                Brand = product.Brand
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
