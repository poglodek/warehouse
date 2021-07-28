using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly IUserContextServices _userContextServices;
        private readonly IItemServices _itemServices;
        private readonly IMapper _mapper;

        public OrderServices(WarehouseDbContext warehouseDbContext,
            IUserServices userServices,
            IClientServices clientServices,
            IUserContextServices userContextServices,
            IItemServices itemServices,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _userServices = userServices;
            _clientServices = clientServices;
            _userContextServices = userContextServices;
            _itemServices = itemServices;
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

        public int Create(OrderCreateDto orderCreateDto)
        {
            var order = _mapper.Map<Order>(orderCreateDto);
            order.DateTime = DateTime.Now;
            order.Client = _clientServices.GetClientById(orderCreateDto.ClientId);
            order.WhoCreated = _userServices.GetUserById(_userContextServices.GetUserId());
            _warehouseDbContext.Orders.Add(order);
            _warehouseDbContext.SaveChanges();
            return order.Id;
        }

        public void Update(OrderCreateDto orderUpdateDto, int id)
        {
            var order = GetOrderById(id);
            order.Client = _clientServices.GetClientById(orderUpdateDto.ClientId);
            order.TargetLocation = orderUpdateDto.TargetLocation;
            order.OrderStatus = orderUpdateDto.OrderStatus;
            _warehouseDbContext.SaveChanges();
        }

        public void AddItem(int id, int itemId)
        {
            var order = GetOrderById(id);
            var item = _itemServices.GetItemById(itemId);
            var orderDetails = new OrderDetails
            {
                Items = item,
                Order = order
            };
            _warehouseDbContext.OrderDetails.Add(orderDetails);
            _warehouseDbContext.SaveChanges();
        }

        public void RemoveItem(int id, int itemId)
        {
            var orderDetails = _warehouseDbContext
                .OrderDetails
                .Where(x => x.Order.Id == id)
                .FirstOrDefault(x => x.Items.Id == itemId);
            _warehouseDbContext.OrderDetails.Remove(orderDetails);
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

        public Order GetOrderById(int id)
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