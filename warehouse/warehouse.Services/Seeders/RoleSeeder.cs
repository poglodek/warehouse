using System.Collections.Generic;
using System.Linq;
using warehouse.Database;
using warehouse.Database.Entity;

namespace warehouse.Services.Seeders
{
    public class RoleSeeder
    {
        private readonly WarehouseDbContext _warehouseDbContext;

        public RoleSeeder(WarehouseDbContext warehouseDbContext)
        {
            _warehouseDbContext = warehouseDbContext;
        }

        public void Seed()
        {
            if (!_warehouseDbContext.Roles.Any())
            {
                _warehouseDbContext.Roles.AddRange(GetDefaultRoles());
                _warehouseDbContext.SaveChanges();
            }
        }

        private IEnumerable<Role> GetDefaultRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    RoleName = "Picker"
                },
                new Role()
                {
                    RoleName = "Admin"
                },
                new Role()
                {
                    RoleName = "Manager"
                }

            };
            return roles;
        }
    }
}
