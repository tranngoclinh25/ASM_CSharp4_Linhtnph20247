using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        ShopDbContext _shopDbContext;

        public OrderDetailService()
        {
            _shopDbContext = new ShopDbContext();
        }
        public List<OrderDetail> GetAllOrderDetail()
        {
            return _shopDbContext.OrderDetails.Include(od => od.Product).Include(od => od.Order).ToList();
        }

        public OrderDetail GetOrderDetailById(Guid id)
        {
            return _shopDbContext.OrderDetails.Include(od => od.Product).Include(od => od.Order).FirstOrDefault(p=>p.Id ==id);
        }

        public bool CreateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                orderDetail.Id = Guid.NewGuid();
                _shopDbContext.OrderDetails.Add(orderDetail);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var od = _shopDbContext.OrderDetails.FirstOrDefault(p => p.Id == orderDetail.Id);
                od.Product = orderDetail.Product;
                od.ProductId = orderDetail.ProductId;
                od.Order = orderDetail.Order;
                od.OrderId = orderDetail.OrderId;
                od.Quantity = orderDetail.Quantity;
                od.DiscountAmount = orderDetail.DiscountAmount;
                _shopDbContext.OrderDetails.Update(od);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteOrderDetail(Guid id)
        {
            try
            {
                var orderDetail = _shopDbContext.OrderDetails.FirstOrDefault(p => p.Id == id);
                _shopDbContext.OrderDetails.Remove(orderDetail);
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
