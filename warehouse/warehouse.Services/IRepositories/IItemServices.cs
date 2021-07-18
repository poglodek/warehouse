using System.Collections.Generic;
using System.Linq;
using warehouse.Database.Entity;
using warehouse.Dto.Item;

namespace warehouse.Services.IRepositories
{
    public interface IItemServices
    {
        Items CreateNewItem(ItemCreateDto itemCreateDto);
        List<ItemDto> GetItemList(string searchingParse, int pageNumber, int quantity);
    }
}