using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface ICartDetailService
    {
        public List<CartDetail> GetAllCartDetail();
        public CartDetail GetCartDetailById(Guid id);
        public List<CartDetail> GetCartDetailByName(string name);
        public bool CreateCartDetail(CartDetail cartDetail);
        public bool UpdateCartDetail(CartDetail cartDetail);
        public bool DeleteCartDetail(Guid id);
    }
}
