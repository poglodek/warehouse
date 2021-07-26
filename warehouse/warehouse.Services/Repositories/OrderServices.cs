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

        public List<OrderListDto> GetOrderListByClientName(string clientName)
        {
            var orders = GetAllOrdersListDto();
            return orders.Where(x => x.WhoCreatedName.Contains(clientName)).ToList();

        }

        public List<OrderListDto> GetOrderListByStatus(string status)
        {
            var orders = GetAllOrdersListDto();
            return orders.Where(x => x.OrderStatus.Contains(status)).ToList();
        }
        public List<OrderListDto> GetOrderListByTarget(string target)
        {
            var orders = GetAllOrdersListDto();
            return orders.Where(x => x.TargetLocation.Contains(target)).ToList();
        }

        public void DeleteById(int id)
        {
            var order = GetOrderById(id);
            var orderDetails = GetOrderListByOrderId(order.Id);
            _warehouseDbContext.OrderDetails.RemoveRange(orderDetails);
            _warehouseDbContext.Orders.Remove(order);
            _warehouseDbContext.SaveChanges();


        }

        public OrderInfoDto GetOrderInfoById(int id)
        {
            var order = GetOrderById(id);
            var orderDetails = GetOrderDetailsDtoByOrderId(order.Id);
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

        
        private List<ItemDto> GetOrderDetailsDtoByOrderId(int orderId)
        {
            var orderList = GetOrderListByOrderId(orderId);
            var items = new List<ItemDto>();
            orderList.ForEach(order =>
            {
                var itemDto = _mapper.Map<ItemDto>(order.Items);
                items.Add(itemDto);
            });

            return items;
        }
        private List<OrderDetails> GetOrderListByOrderId(int orderId)
        {
            var orderList = _warehouseDbContext
                .OrderDetails
                .Include(x => x.Items)
                .Include(x => x.Items.IndexItem)
                .Where(x => x.Order.Id == orderId)
                .ToList();

            return orderList;
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
