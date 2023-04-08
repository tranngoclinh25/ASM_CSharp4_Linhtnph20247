using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class OrderService : IOrderService
    {
        ShopDbContext _shopDbContext;

        public OrderService()
        {
            _shopDbContext = new ShopDbContext();
        }

        public List<Order> GetAllOrder()
        {
            return _shopDbContext.Orders.Include(p=>p.User).ToList();
        }

        public Order GetOrderById(Guid id)
        {
            return _shopDbContext.Orders.Include(p=>p.User).FirstOrDefault(p => p.Id == id);
        }

        public bool CreateOrder(Order order)
        {
            try
            {
                order.Id = Guid.NewGuid();
                order.CreatedAt = DateTime.Now;
                _shopDbContext.Orders.Add(order);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            return false;
        }

        public bool DeleteOrder(Guid id)
        {
            try
            {
                var order = _shopDbContext.Orders.FirstOrDefault(p => p.UserId == id);
                _shopDbContext.Orders.Remove(order);
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
