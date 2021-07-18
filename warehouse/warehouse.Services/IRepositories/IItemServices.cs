using warehouse.Database.Entity;
using warehouse.Dto.Item;

namespace warehouse.Services.IRepositories
{
    public interface IItemServices
    {
        Items CreateNewItem(ItemCreateDto itemCreateDto);
    }
}