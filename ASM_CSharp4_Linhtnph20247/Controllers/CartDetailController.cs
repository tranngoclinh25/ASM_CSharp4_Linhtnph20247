using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using ASM_CSharp4_Linhtnph20247.Services;
using Microsoft.AspNetCore.Mvc;
using _2_ASP.NET_MVC.Models;
using System.Diagnostics;

namespace ASM_CSharp4_Linhtnph20247.Controllers
{
    public class CartDetailController : Controller
    {
        private readonly ILogger<CartDetailController> _logger;
        private readonly ICartDetailService _cartDetailService;
        public CartDetailController(ILogger<CartDetailController> logger)
        {
            _logger = logger;
            _cartDetailService = new CartDetailService();
        }

        public IActionResult CartDetail()
        {
            List<CartDetail> cartDetails = _cartDetailService.GetAllCartDetail();
            return View("CartDetail", cartDetails);
        }
        public IActionResult Redirect()
        {
            return RedirectToAction("CartDetail");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
