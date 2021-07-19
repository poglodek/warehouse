using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Item;

namespace warehouse.Services.Validation
{
    public class ItemValidation : AbstractValidator<Items>
    {
        public ItemValidation(WarehouseDbContext warehouseDbContext)
        {
            RuleFor(x => x.ActualLocation).NotEmpty();
            
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.EAN).NotEmpty();
        }
    }
}
