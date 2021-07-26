using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Item;
using warehouse.Dto.Order;
using warehouse.Exceptions;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class OrderServices : IOrderServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IUserServices _userServices;
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;

        public OrderServices(WarehouseDbContext warehouseDbContext,
            IUserServices userServices,
            IClientServices clientServices,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _userServices = userServices;
            _clientServices = clientServices;
            _mapper = mapper;
        }

        public List<OrderListDto> GetAllOrdersListDto()
        {
            var orders = _mapper.Map<List<OrderListDto>>(GetAllOrders());
            return orders;
        }

        public List<OrderListDto> GetOrderInfoByClientName(string clientName)
        {
            var orders = GetAllOrdersListDto();
            return orders.Where(x => x.WhoCreatedName.Contains(clientName)).ToList();

        }

        public OrderInfoDto GetOrderInfoById(int id)
        {
            var order = GetOrderById(id);
            var orderDetails = GetOrderDetailsByOrder(order.Id);
            var orderInfoDto = _mapper.Map<OrderInfoDto>(order);
            orderInfoDto.WhoCreated = _userServices.GetUserDtoById(order.WhoCreated.Id);
            orderInfoDto.Client = _clientServices.GetClientDtoById(order.Client.Id);
            orderInfoDto.Items = orderDetails;
            return orderInfoDto;
        }

        private Order GetOrderById(int id)
        {
            var order = GetAllOrders()
                .FirstOrDefault(x => x.Id == id);
            if (order is null) throw new NotFound("Order not found.");
            return order;
        }

        
        private List<ItemDto> GetOrderDetailsByOrder(int orderId)
        {
            var orderList = _warehouseDbContext
                .OrderDetails
                .Include(x => x.Items)
                .Include(x => x.Items.IndexItem)
                .Where(x => x.Order.Id == orderId)
                .ToList();
            var items = new List<ItemDto>();
            orderList.ForEach(order =>
            {
                var itemDto = _mapper.Map<ItemDto>(order.Items);
                items.Add(itemDto);
            });

            return items;
        }
        

        private List<Order> GetAllOrders()
        {
            return _warehouseDbContext
                .Orders
                .Include(x => x.Client)
                .Include(x => x.WhoCreated)
                .ToList();
        }

    }
}
