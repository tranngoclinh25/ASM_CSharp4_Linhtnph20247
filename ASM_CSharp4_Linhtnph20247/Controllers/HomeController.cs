using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _2_ASP.NET_MVC.Models;
using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using ASM_CSharp4_Linhtnph20247.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ISizeService _sizeService;
        private readonly IBrandService _brandService;
        private readonly ICartDetailService _cartDetailService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productService = new ProductService();
            _sizeService = new SizeService();
            _brandService = new BrandService();
            _cartDetailService = new CartDetailService();
            _userService = new UserService();
        }

        public IActionResult Index()
        {
            return View("Index");
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
            var viewModel = new ProductViewModel()
            {
                Product = product,
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
                QuantityAddToCart = 1
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

        public IActionResult Signin()
        {
            return View("Signin");
        }

        public IActionResult Login(string user, string pass)
        {
            if (/*user.Length <= 6 || */Regex.IsMatch(user, @"[!@#$%^&*()_+{}\[\]:;""',.<>/?\\|~-]"))
            {
                var thongbao = "UserName must be longer than 6 characters and contains no special characters";
                TempData["User"] = thongbao;
                return RedirectToAction("Login", new { thongbao });
            }
            if (pass.Length <= 6 || Regex.IsMatch(pass, @"[!@#$%^&*()_+{}\[\]:;""',.<>/?\\|~-]"))
            {
                var thongbao = "Password must be longer than 6 characters and contains no special characters";
                TempData["Pass"] = thongbao;
                return RedirectToAction("Login", new { thongbao });
            }

            var users = _userService.GetAllUsers();
            foreach (var item in users)
            {
                if (user == item.UserName && pass == item.Password)
                {
                    return RedirectToAction("Index");
                }
            }
            var tb = "Username or password incorrect";
            TempData["UserPass"] = tb;
            return RedirectToAction("Login", new { tb });
        }
        public IActionResult CheckOut()
        {
            //var cart = _context.Carts.FirstOrDefault(c => c.UserId == currentUserId); //Phân quyền: Lấy thông tin giỏ hàng của người dùng hiện tại
            var cartDetails = _cartDetailService.GetAllCartDetail()/*.Where(cd => cd.CartId == cart.Id)*/;
            float totalAmount = 0;
            foreach (var cartDetail in cartDetails)
            {
                totalAmount += cartDetail.Product.Price * cartDetail.Quantity;
            }
            var cartViewModel = new CartViewModel { CartDetails = cartDetails, TotalAmount = totalAmount };
            return View("CheckOut", cartViewModel);
        }

        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return RedirectToAction("Shop");
            }
            var check = _productService.GetProductByName(search);
            var viewModel = new ProductViewModel(){ Products = check};
            return View("Shop", viewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}