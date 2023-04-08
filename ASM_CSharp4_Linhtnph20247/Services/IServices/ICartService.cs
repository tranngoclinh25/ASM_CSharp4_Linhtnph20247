using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface ICartService
    {
        public List<Cart> GetAllCart();
        public Cart GetCartById(Guid id);
        public bool CreateCart(Cart cart);
        public bool UpdateCart(Cart cart);  
        public bool DeleteCart(Guid id);
    }
}
    