using System.Collections.Generic;
using warehouse.Database.Entity;
using warehouse.Dto.Order;

namespace warehouse.Services.IRepositories
{
    public interface IOrderServices
    {
        OrderInfoDto GetOrderInfoById(int id);
        List<OrderListDto> GetAllOrdersListDto();
        List<OrderListDto> GetOrderListByClientName(string clientName);
        List<OrderListDto> GetOrderListByStatus(string status);
        List<OrderListDto> GetOrderListByTarget(string target);
        void DeleteById(int id);
        int Create(OrderCreateDto orderCreateDto);
        void Update(OrderCreateDto orderUpdateDto, int id);
        void AddItem(int id, int itemId);
        void RemoveItem(int id, int itemId);
    }
}