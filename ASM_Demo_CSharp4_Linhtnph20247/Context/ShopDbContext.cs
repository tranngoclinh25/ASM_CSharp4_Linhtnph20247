using System.Reflection;
using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM_CSharp4_Linhtnph20247.ContextEF
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {
            
        }

        public ShopDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-56CMKMS;Initial Catalog=ASM_C#4_Linhtnph20247;Persist Security Info=True;User ID=Linhtnph20247;Password=25102003");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
