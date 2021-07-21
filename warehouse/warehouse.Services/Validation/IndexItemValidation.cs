using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using warehouse.Database;
using warehouse.Dto.Index;
using warehouse.Dto.Item;

namespace warehouse.Services.Validation
{
    public class IndexItemValidation : AbstractValidator<IndexDto>
    {
        public IndexItemValidation(WarehouseDbContext warehouseDbContext)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEqual(0);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
