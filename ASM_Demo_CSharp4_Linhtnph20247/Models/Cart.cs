namespace ASM_CSharp4_Linhtnph20247.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }    
        public DateTime CreatedAt { get; set;}
        public virtual User User { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}   
