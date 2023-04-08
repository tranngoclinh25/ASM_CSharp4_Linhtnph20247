using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface IOrderService
    {
        public List<Order> GetAllOrder();
        public Order GetOrderById(Guid id);
        public bool CreateOrder(Order order);   
        public bool UpdateOrder(Order order);
        public bool DeleteOrder(Guid id);
    }
}
