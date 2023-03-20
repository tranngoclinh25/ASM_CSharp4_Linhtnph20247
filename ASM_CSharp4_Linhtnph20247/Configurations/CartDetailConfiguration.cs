using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM_CSharp4_Linhtnph20247.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Cart)
                .WithMany(p => p.CartDetails)
                .HasForeignKey(p => p.CartId);
            builder.HasOne(p => p.Product)
                .WithMany(p => p.CartDetails)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
