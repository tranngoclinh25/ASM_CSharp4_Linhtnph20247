using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using ASM_CSharp4_Linhtnph20247.ViewModel;
using Microsoft.EntityFrameworkCore;

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
            return _shopDbContext.Products.Include(p=>p.Brand).Include(p=>p.Size).Where(p=>p.Status == 1).ToList(); // Lấy tất cả các sản phẩm
        }

        public Product GetProductById(Guid id)
        {
            return _shopDbContext.Products.Include(p => p.Brand).Include(p => p.Size).FirstOrDefault(p => p.Id == id);
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

        public bool UpdateProduct(ProductViewModel model, Guid id)
        {
            try
            {
                var p = _shopDbContext.Products.FirstOrDefault(p => p.Id == id);
                //if (model.Price > p.Price)
                //{
                    p.Name = model.Name;
                    p.BrandId = model.BrandId;
                    p.SizeId = model.SizeId;
                    p.Description = model.Description;
                    p.Price = model.Price;
                    p.Quantity = model.Quantity;
                    p.Brand = model.Brand;
                    p.Size = model.Size;
                    p.ImageUrl = model.ImageUrl;
                    p.Status = model.Status;
                    _shopDbContext.Products.Update(p);
                    _shopDbContext.SaveChanges();
                    return true;
                //}
                //return false;
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
