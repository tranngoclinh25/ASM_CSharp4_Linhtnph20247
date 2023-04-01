using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class SizeService : ISizeService
    {
        ShopDbContext _shopDbContext;

        public SizeService()
        {
            _shopDbContext = new ShopDbContext();
        }

        public List<Size> GetAllSizes()
        {
            return _shopDbContext.Sizes.ToList();
        }

        public Size GetSizeById(Guid id)
        {
            return _shopDbContext.Sizes.FirstOrDefault(p => p.Id == id);
        }
    }
}
