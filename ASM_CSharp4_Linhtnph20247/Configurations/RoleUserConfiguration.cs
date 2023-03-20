using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM_CSharp4_Linhtnph20247.Configurations
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Role)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(p => p.RoleId);
            builder.HasOne(p => p.User)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(p => p.UserId);
        }
    }
}
