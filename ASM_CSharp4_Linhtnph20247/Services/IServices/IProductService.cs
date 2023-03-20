using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface IProductService
    {
        public List<Product> GetAllProduct();
        public Product GetProductById(Guid id);
        public List<Product> GetProductByName(string name);
        public bool CreateProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(Guid id);
    }
}
