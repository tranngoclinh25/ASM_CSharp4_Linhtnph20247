using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.ViewModel;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface IProductService
    {
        public List<Product> GetAllProduct();
        public Product GetProductById(Guid id);
        public List<Product> GetProductByName(string name);
        public bool CreateProduct(Product product);
        public bool UpdateProduct(ProductViewModel model, Guid id);
        public bool DeleteProduct(Guid id);
    }
}
