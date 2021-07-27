using System.Linq;
using FluentValidation;
using warehouse.Database;
using warehouse.Dto.ShippingInfo;

namespace warehouse.Dto.User
{
    public class ShippingInfoCreateDtoValidation : AbstractValidator<ShippingInfoCreateDto>
    {
        public ShippingInfoCreateDtoValidation(WarehouseDbContext warehouseDbContext)
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.ShippingPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.TrackingNumber).NotEmpty();

            RuleFor(x => x.ClientId).Custom((value, context) =>
            {
                var client = warehouseDbContext.Clients.Any(x => x.Id == value);
                if (!client)
                    context.AddFailure("Client not found!");
            });
            RuleFor(x => x.OrderId).Custom((value, context) =>
            {
                var order = warehouseDbContext.Orders.Any(x => x.Id == value);
                if (!order)
                    context.AddFailure("Order not found!");
            });
        }
    }
}
