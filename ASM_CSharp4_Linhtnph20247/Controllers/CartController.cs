using _2_ASP.NET_MVC.Models;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using ASM_CSharp4_Linhtnph20247.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService; 
        private readonly ICartDetailService _cartDetailService;

        public CartController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productService = new ProductService();
            _cartService = new CartService();
            _cartDetailService = new CartDetailService();
        }
        public IActionResult Cart()
        {
            //var cart = _context.Carts.FirstOrDefault(c => c.UserId == currentUserId); //Phân quyền: Lấy thông tin giỏ hàng của người dùng hiện tại
            var cartDetails = _cartDetailService.GetAllCartDetail()/*.Where(cd => cd.CartId == cart.Id)*/;
            float totalAmount = 0;
            foreach (var cartDetail in cartDetails)
            {
                totalAmount += cartDetail.Product.Price * cartDetail.Quantity;
            }
            var cartViewModel = new CartViewModel { CartDetails = cartDetails, TotalAmount = totalAmount};
            return View(cartViewModel);
        }
        [HttpPost]
        public IActionResult AddToCart(Guid productId, int quantity)
        {
            //Kiểm tra xem Giỏ hàng hiện tại của người dùng

            //C1
            //var cart =_cartService.GetCartById(currentUserId);

            //if (cart == null) 
            //{
            //    cart = new Cart { UserId = currentUserId };
            //    _cartService.CreateCart(cart);
            //}

            //C2
            //var cart = _cartService.GetCartById(User.Identity.GetUserId());

            //if (cart == null)
            //{
            //    cart = new Cart { UserId = User.Identity.GetUserId() };
            //}

            var product = _productService.GetProductById(productId);

            if (product == null)
            {
                return HttpNotFound();
            }

            var cartDetail = _cartDetailService.GetAllCartDetail().FirstOrDefault(p => p.ProductId == productId /*&& p.CartId == cart.Id*/);

            if (cartDetail == null)
            {
                cartDetail = new CartDetail
                {
                    ProductId = productId,
                    //CartId = cart.Id; //Chưa xác định được giỏ hàng của người dùng
                    CartId = Guid.Parse("48632772-2D15-4EA2-A863-32E317E4A3D5"),
                    Quantity = quantity
                };
                if(_cartDetailService.CreateCartDetail(cartDetail))
                    return RedirectToAction("Cart");
            }
            else
            {
                cartDetail.Quantity += quantity;
                if(_cartDetailService.UpdateCartDetail(cartDetail))
                    return RedirectToAction("Cart");

            }
            return RedirectToAction("ProductDetail", "Home");
        }
        private ActionResult HttpNotFound()
        {
            throw new ArgumentException($"Product with ID not found.");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
