using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

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
            return _shopDbContext.CartDetails.ToList(); // Lấy tất cả các sản phẩm
        }

        public CartDetail GetCartDetailById(Guid id)
        {
            return _shopDbContext.CartDetails.FirstOrDefault(p => p.Id == id);
        }

        public List<CartDetail> GetCartDetailByName(string name)
        {
            //return _shopDbContext.CartDetails.Where(p => p.Name.Contains(name)).ToList();
            //Trả về danh sách những Sản Phẩm mà tên có chứa chuỗi cần tìm
            return null;
        }

        public bool CreateCartDetail(CartDetail CartDetail)
        {
            try
            {
                CartDetail.Id = Guid.NewGuid();
                _shopDbContext.CartDetails.Add(CartDetail);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCartDetail(CartDetail CartDetail)
        {
            //try
            //{
            //    var p = _shopdbcontext.cartdetails.firstordefault(p => p.id == cartdetail.id);
            //    //if (cartdetail.price > p.price)
            //    //{
            //    p.name = cartdetail.name;
            //    p.description = cartdetail.description;
            //    p.price = cartdetail.price;
            //    p.quantity = cartdetail.quantity;
            //    p.createdat = cartdetail.createdat;
            //    p.brand = cartdetail.brand;
            //    p.size = cartdetail.size;
            //    p.imageurl = cartdetail.imageurl;
            //    p.status = cartdetail.status;
            //    _shopdbcontext.savechanges();
            //    return true;
            //    //}
            //    return false;
            //}
            //catch (exception e)
            //{
            //    return false;
            //}
            return false;
        }

        public bool DeleteCartDetail(Guid id)
        {
            try
            {
                var CartDetail = _shopDbContext.CartDetails.FirstOrDefault(p => p.Id == id);
                _shopDbContext.CartDetails.Remove(CartDetail);
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
