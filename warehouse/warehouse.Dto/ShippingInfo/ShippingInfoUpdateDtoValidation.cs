using System.Linq;
using FluentValidation;
using warehouse.Database;
using warehouse.Dto.ShippingInfo;

namespace warehouse.Dto.User
{
    public class ShippingInfoUpdateDtoValidation : AbstractValidator<ShippingInfoUpdateDto>
    {
        public ShippingInfoUpdateDtoValidation(WarehouseDbContext warehouseDbContext)
        {
            RuleFor(x => x.ShippingPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.TrackingNumber).NotEmpty();
            RuleFor(x => x.IsInsurance).NotEmpty();
            RuleFor(x => x.IsPriority).NotEmpty();
            RuleFor(x => x.TargetLocation).NotEmpty();

            
        }
    }
}
