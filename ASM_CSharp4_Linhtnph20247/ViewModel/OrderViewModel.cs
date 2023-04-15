using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.ViewModel
{
    public class OrderViewModel
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Order> Orders { get; set; }
        public float TotalPrice { get; set; }
    }   
}
