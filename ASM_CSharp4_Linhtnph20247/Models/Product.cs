namespace ASM_CSharp4_Linhtnph20247.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public Guid SizeId { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
    