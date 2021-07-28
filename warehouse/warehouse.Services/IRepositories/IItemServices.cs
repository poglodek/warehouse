using System.Collections.Generic;
using warehouse.Database.Entity;
using warehouse.Dto.Item;

namespace warehouse.Services.IRepositories
{
    public interface IItemServices
    {
        Items CreateNewItem(ItemCreateDto itemCreateDto);

        List<ItemDto> GetItemList(string searchingParse, int pageNumber, int quantity);

        ItemDto GetItemDtoById(int id);

        void DeleteById(int id);

        List<ItemDto> GetByEan(string ean);

        List<ItemDto> GetByLocation(string location);

        List<ItemDto> GetBySerialNumber(string serialNumber);

        void Update(ItemDto itemDto, int id);

        public Items GetItemById(int id);
    }
}