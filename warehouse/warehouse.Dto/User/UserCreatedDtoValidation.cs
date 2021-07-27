using System.Linq;
using FluentValidation;
using warehouse.Database;
using warehouse.Dto.User;

namespace warehouse.Dto.ShippingInfo
{
    public class UserCreatedDtoValidation : AbstractValidator<UserCreatedDto>
    {
        private const string regex = @"[0-9]{9}";
        public UserCreatedDtoValidation(WarehouseDbContext warehouseDbContext)
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.HashedPassword).MinimumLength(8);
            RuleFor(x => x.Phone).Matches(regex);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var email = warehouseDbContext.Users.Any(x => x.Email == value);
                if (email)
                    context.AddFailure("email was taken.");
            });
        }
    }
}
