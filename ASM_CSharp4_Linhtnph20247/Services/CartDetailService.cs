using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class CartDetailService : ICartDetailService
    {
        ShopDbContext _shopDbContext;

        public CartDetailService()
        {
            _shopDbContext = new ShopDbContext();
        }
        public List<CartDetail> GetAllCartDetail()
        {
            return _shopDbContext.CartDetails.Include(cd => cd.Product).Include(cd=>cd.Cart).ToList(); // Lấy tất cả các sản phẩm
        }

        public CartDetail GetCartDetailById(Guid id)
        {
            return _shopDbContext.CartDetails.Include(cd=>cd.Product).Include(cd=>cd.Cart).FirstOrDefault(p => p.Id == id);
        }

        public bool CreateCartDetail(CartDetail cartDetail)
        {
            try
            {
                cartDetail.Id = Guid.NewGuid();
                _shopDbContext.CartDetails.Add(cartDetail);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCartDetail(CartDetail cartDetail)
        {
            try
            {
                var cd = _shopDbContext.CartDetails.FirstOrDefault(p => p.Id == cartDetail.Id);
                cd.Product = cartDetail.Product;
                cd.ProductId = cartDetail.ProductId;
                cd.Cart = cartDetail.Cart;
                cd.CartId = cartDetail.CartId;
                cd.Quantity = cartDetail.Quantity;
                _shopDbContext.CartDetails.Update(cd);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteCartDetail(Guid id)
        {
            try
            {
                var cartDetail = _shopDbContext.CartDetails.FirstOrDefault(p => p.Id == id);
                _shopDbContext.CartDetails.Remove(cartDetail);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
