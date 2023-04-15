using _2_ASP.NET_MVC.Models;
using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using ASM_CSharp4_Linhtnph20247.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICartDetailService _cartDetailService;

        public OrderController(ILogger<OrderController> logger)     
        {
            _logger = logger;
            _productService = new ProductService();
            _orderService = new OrderService();
            _orderDetailService = new OrderDetailService();
            _cartDetailService = new CartDetailService();
        }

        public IActionResult Order()
        {
            //var order = _orderService.GetOrderById(currentUserId); //Phân quyền: Lấy thông tin hóa đơn của người dùng hiện tại đang Login
            var orderDetails = _orderDetailService.GetAllOrderDetail()/*.Where(od => od.OrderId == order.Id)*/;
            var order = _orderService.GetAllOrder();
            float totalPrice = 0;
            foreach (var orderDetail in orderDetails)
            {
                totalPrice += orderDetail.Product.Price * orderDetail.Quantity;
            }
            var orderViewModel = new OrderViewModel() { OrderDetails = orderDetails, Orders = order, TotalPrice = totalPrice };
            return View(orderViewModel);
        }
        public IActionResult OrderDetail(Guid id)
        {   
            //var order = _orderService.GetOrderById(currentUserId); //Phân quyền: Lấy thông tin hóa đơn của người dùng hiện tại đang Login
            var orderDetails = _orderDetailService.GetAllOrderDetail().Where(p=>p.OrderId == id).ToList()/*.Where(od => od.OrderId == order.Id)*/;
            var order = _orderService.GetAllOrder();
            float totalPrice = 0;
            foreach (var orderDetail in orderDetails)
            {
                totalPrice += orderDetail.Product.Price * orderDetail.Quantity;
            }
            var orderViewModel = new OrderViewModel() { OrderDetails = orderDetails, Orders = order, TotalPrice = totalPrice };
            return View(orderViewModel);
        }
        [HttpPost]
        public IActionResult PlaceOrder(CartViewModel model)
        {
            var outOfStockItems = new List<Product>();
            // Kiểm tra số lượng sản phẩm trong giỏ hàng với số lượng sản phẩm còn lại
            foreach (var cartDetail in model.CartDetails)
            {
                var product = _productService.GetProductById(cartDetail.ProductId);
                if (product.Quantity < cartDetail.Quantity)
                {
                    outOfStockItems.Add(product);
                }
            }

            if (outOfStockItems.Count > 0)
            {
                // Trả về thông báo lỗi nếu có sản phẩm không đủ số lượng để thanh toán
                var message = "The following items are out of stock: ";
                foreach (var item in outOfStockItems)
                {
                    message += item.Name + ", ";
                }
                message = message.Substring(0, message.Length - 2);
                TempData["ErrorMessage"] = message;
                return RedirectToAction("CheckOut", "Home",new { message });
            }
            else
            {
                //Tạo hóa đơn mới
                var order = new Order()
                {
                    //UserId = currentUserId, //Id của người dùng đang Login
                    UserId = Guid.Parse("48632772-2D15-4EA2-A863-32E317E4A3D5"),
                    ShoppingAddress = "Hà Nội",
                    PaymentMethod = "Thanh toán khi nhận hàng",
                    TotalPrice = model.TotalAmount,
                    Status = 1
                };
                if (_orderService.CreateOrder(order) == false)
                    return RedirectToAction("CheckOut", "Home");

                //Thêm chi tiết hóa đơn cho từng sản phẩm trong giỏ hàng
                foreach (var cartDetail in model.CartDetails)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.Id, //Id của hóa đơn vừa tạo
                        ProductId = cartDetail.ProductId,
                        Quantity = cartDetail.Quantity,
                        DiscountAmount = 0
                    };
                    if (_orderDetailService.CreateOrderDetail(orderDetail) == false)
                        return RedirectToAction("CheckOut", "Home");
                    //Trừ số lượng sản phẩm trong CSDL
                    var product = _productService.GetProductById(cartDetail.ProductId);
                    product.Quantity -= cartDetail.Quantity;
                    _productService.UpdateProductQuantity(product);
                    if (_cartDetailService.DeleteCartDetail(cartDetail.Id) == false) //Xóa các bản ghi mà người dùng thêm vào trong giỏ hàng
                        return RedirectToAction("CheckOut", "Home");
                }
                return RedirectToAction("Order");
            }
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
