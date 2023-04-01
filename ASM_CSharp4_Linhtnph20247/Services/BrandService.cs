using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class BrandService : IBrandService
    {
        ShopDbContext _shopDbContext;

        public BrandService()
        {
            _shopDbContext = new ShopDbContext();
        }
        public List<Brand> GetAllBrand()
        {
            return _shopDbContext.Brands.ToList();
        }

        public Brand GetBrandById(Guid id)
        {
            return _shopDbContext.Brands.FirstOrDefault(p => p.Id == id);
        }
    }
}
