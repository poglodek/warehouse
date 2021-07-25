using System.Collections.Generic;
using warehouse.Database.Entity;
using warehouse.Dto.Order;

namespace warehouse.Services.IRepositories
{
    public interface IOrderServices
    {
        OrderInfoDto GetOrderInfoById(int id);
    }
}