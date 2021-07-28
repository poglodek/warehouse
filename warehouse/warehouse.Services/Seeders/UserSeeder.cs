using System.Linq;
using warehouse.Database;
using warehouse.Dto.User;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Seeders
{
    public class UserSeeder
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IUserServices _userServices;

        public UserSeeder(WarehouseDbContext warehouseDbContext,
            IUserServices userServices)
        {
            _warehouseDbContext = warehouseDbContext;
            _userServices = userServices;
        }

        public void Seed()
        {
            if (!_warehouseDbContext.Users.Any())
            {
                SetAdmin();

            }
        }

        private void SetAdmin()
        {
            var userCreated = new UserCreatedDto()
            {
                Email = "admin@admin.com",
                FirstName = "Admin",
                HashedPassword = "Adm!n1234",
                LastName = "Admin",
                Phone = "123123123"

            };
            var adminRole = _warehouseDbContext
                .Roles
                .FirstOrDefault(x => x.RoleName.Contains("Admin"));
            var userId = _userServices.RegisterUser(userCreated);
            var user = _userServices.GetUserById(userId);
            user.Role = adminRole;
            _warehouseDbContext.SaveChanges();
        }
    }


}
