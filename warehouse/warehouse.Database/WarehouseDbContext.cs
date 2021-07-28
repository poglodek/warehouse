using Microsoft.EntityFrameworkCore;
using warehouse.Database.Entity;

namespace warehouse.Database
{
    public class WarehouseDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<IndexItem> IndexItems { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShippingInfo> ShippingInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public WarehouseDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}