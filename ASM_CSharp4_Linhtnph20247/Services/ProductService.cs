using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class ProductService : IProductService
    {
        ShopDbContext _shopDbContext;

        public ProductService()
        {
            _shopDbContext = new ShopDbContext();
        }
        public List<Product> GetAllProduct()
        {
            return _shopDbContext.Products.ToList(); // Lấy tất cả các sản phẩm
        }

        public Product GetProductById(Guid id)
        {
            return _shopDbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProductByName(string name)
        {
            return _shopDbContext.Products.Where(p => p.Name.Contains(name)).ToList();
            //Trả về danh sách những Sản Phẩm mà tên có chứa chuỗi cần tìm
        }

        public bool CreateProduct(Product product)
        {
            try
            {
                product.Id = Guid.NewGuid();
                product.CreatedAt = DateTime.Now;
                _shopDbContext.Products.Add(product);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                var p = _shopDbContext.Products.FirstOrDefault(p => p.Id == product.Id);
                p.Name = product.Name;
                p.Description = product.Description;
                p.Price = product.Price;
                p.Quantity = product.Quantity;
                p.CreatedAt = product.CreatedAt;
                p.Brand = product.Brand;
                p.Size = product.Size;
                p.ImageUrl = product.ImageUrl;
                p.Status = product.Status;
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var product = _shopDbContext.Products.FirstOrDefault(p => p.Id == id);
                _shopDbContext.Products.Remove(product);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
