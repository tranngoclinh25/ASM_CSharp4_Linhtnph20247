using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM_CSharp4_Linhtnph20247.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.HasOne(p => p.User)
                .WithOne();
        }
    }
}
