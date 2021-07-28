using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.ShippingInfo;
using warehouse.Exceptions;
using warehouse.Exceptions.Exceptions;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class ShippingInfoServices : IShippingInfoServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IClientServices _clientServices;
        private readonly IOrderServices _orderServices;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;

        public ShippingInfoServices(WarehouseDbContext warehouseDbContext,
            IClientServices clientServices,
            IOrderServices orderServices,
            IUserContextServices userContextServices,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _clientServices = clientServices;
            _orderServices = orderServices;
            _userContextServices = userContextServices;
            _mapper = mapper;
        }

        public List<ShippingInfoDto> GetShippingInfoDtoList()
        {
            var shippingInfo = _warehouseDbContext
                .ShippingInfos
                .Include(x => x.Client)
                .Include(x => x.Order)
                .ToList();

            var shippingInfoDto = _mapper.Map<List<ShippingInfoDto>>(shippingInfo);

            return shippingInfoDto;
        }

        public ShippingInfoDto GetShippingInfoDtoById(int id)
        {
            var shippingInfo = GetShippingInfoById(id);
            if (shippingInfo is null) throw new NotFound("Shipping info not found.");

            var shippingInfoDto = _mapper.Map<ShippingInfoDto>(shippingInfo);

            return shippingInfoDto;
        }

        public List<ShippingInfoDto> GetShippingInfoDtoByTarget(string target)
        {
            var shippingList = GetShippingInfoDtoList()
                .Where(x => x.TargetLocation.Contains(target))
                .ToList();

            return shippingList;
        }

        public List<ShippingInfoDto> GetShippingInfoDtoByTrackNumber(string trackNumber)
        {
            var shippingList = GetShippingInfoDtoList()
                .Where(x => x.TrackingNumber.Contains(trackNumber))
                .ToList();

            return shippingList;
        }

        public List<ShippingInfoDto> GetShippingInfoDtoByPriority(string priority)
        {
            if (!bool.TryParse(priority, out var isPriority)) throw new CannotParse("This is not a bool value.");
            var shippingList = GetShippingInfoDtoList()
                .Where(x => x.IsPriority == isPriority)
                .ToList();

            return shippingList;
        }

        public List<ShippingInfoDto> GetShippingInfoDtoByClientId(int id)
        {
            var shippingInfo = _warehouseDbContext
                .ShippingInfos
                .Include(x => x.Client)
                .Include(x => x.Order)
                .Where(x => x.Client.Id == id)
                .ToList();
            if (shippingInfo is null) throw new NotFound("Shipping info not found.");
            var shippingInfoDto = _mapper.Map<List<ShippingInfoDto>>(shippingInfo);

            return shippingInfoDto;
        }

        public int CreateShippingInfo(ShippingInfoCreateDto shippingInfoCreateDto)
        {
            var shippingInfo = _mapper.Map<ShippingInfo>(shippingInfoCreateDto);
            shippingInfo.DateTime = DateTime.Now;
            shippingInfo.Client = _clientServices.GetClientById(shippingInfoCreateDto.ClientId);
            shippingInfo.Order = _orderServices.GetOrderById(shippingInfoCreateDto.OrderId);
            _warehouseDbContext.ShippingInfos.Add(shippingInfo);
            _warehouseDbContext.SaveChanges();
            return shippingInfo.Id;
        }

        public void UpdateShippingInfo(ShippingInfoUpdateDto shippingInfoUpdateDto, int id)
        {
            if (!IfUserCreated(_userContextServices.GetUserId(), id)) throw new ForbiddenException("Forbidden");
            var shippingInfo = GetShippingInfoById(id);
            shippingInfo.TargetLocation = shippingInfoUpdateDto.TargetLocation;
            shippingInfo.TrackingNumber = shippingInfoUpdateDto.TrackingNumber;
            shippingInfo.IsInsurance = shippingInfoUpdateDto.IsInsurance;
            shippingInfo.IsPriority = shippingInfoUpdateDto.IsPriority;
            shippingInfo.ShippingPrice = shippingInfoUpdateDto.ShippingPrice;
            _warehouseDbContext.SaveChanges();
        }

        public void DeleteShippingInfoById(int id)
        {
            if (!IfUserCreated(_userContextServices.GetUserId(), id)) throw new ForbiddenException("Forbidden");
            var shippingInfo = GetShippingInfoById(id);
            _warehouseDbContext.ShippingInfos.Remove(shippingInfo);
            _warehouseDbContext.SaveChanges();
        }

        private ShippingInfo GetShippingInfoById(int id)
        {
            var shippingInfo = _warehouseDbContext
                .ShippingInfos
                .Include(x => x.Client)
                .Include(x => x.Order)
                .FirstOrDefault(x => x.Id == id);
            if (shippingInfo is null) throw new NotFound("Shipping info not found.");

            return shippingInfo;
        }

        private bool IfUserCreated(int userId, int shippingInfoId)
        {
            var whoCreatedId = GetShippingInfoById(shippingInfoId)
                .WhoCreated
                .Id;
            var role = _userContextServices.GetUser.FindFirst(x => x.Type == ClaimTypes.Role).Value;
            if (userId == whoCreatedId) return true;
            else if (role == "Admin") return true;
            return false;
        }
    }
}