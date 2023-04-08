using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface IOrderDetailService
    {
        public List<OrderDetail> GetAllOrderDetail();
        public OrderDetail GetOrderDetailById(Guid id);
        public bool CreateOrderDetail(OrderDetail orderDetail);
        public bool UpdateOrderDetail(OrderDetail orderDetail);
        public bool DeleteOrderDetail(Guid id);
    }
}   
    