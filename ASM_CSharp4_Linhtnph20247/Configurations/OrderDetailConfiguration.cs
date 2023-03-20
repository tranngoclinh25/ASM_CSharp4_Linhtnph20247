using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM_CSharp4_Linhtnph20247.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(p => p.OrderId);
            builder.HasOne(p => p.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
