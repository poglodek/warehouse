using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using warehouse.Database;
using warehouse.Dto.User;

namespace warehouse.Dto.User
{
    public class UserCreatedDtoValidation : AbstractValidator<UserCreatedDto>
    {
        private const string regex = @"[0-9]{9}";
        public UserCreatedDtoValidation(WarehouseDbContext warehouseDbContext)
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.HashedPassword).MinimumLength(8);
            RuleFor(x => x.Phone).Matches(regex);
        }
    }
}
