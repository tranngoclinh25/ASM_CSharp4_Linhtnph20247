namespace ASM_CSharp4_Linhtnph20247.Models
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
