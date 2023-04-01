using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface IBrandService
    {
        public List<Brand> GetAllBrand();
        public Brand GetBrandById(Guid id); 
    }
}
