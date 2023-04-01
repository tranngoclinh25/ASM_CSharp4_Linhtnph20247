using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM_CSharp4_Linhtnph20247.ViewModel
{
    public class ProductViewModel
    {
        //Product
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        //Brand
        public Brand Brand { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public List<SelectListItem> Brands { get; set; }
        //Size
        public Size Size { get; set; }
        public Guid SizeId { get; set; }
        public string SizeName { get; set; }
        public List<SelectListItem> Sizes { get; set; }
    }
}
