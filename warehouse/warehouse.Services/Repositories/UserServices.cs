using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Client;
using warehouse.Dto.Index;
using warehouse.Dto.User;
using warehouse.Exceptions;
using warehouse.Exceptions.Exceptions;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class UserServices : IUserServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;

        public UserServices(WarehouseDbContext warehouseDbContext,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }


        public List<UserDto> GetAllUsers()
        {
            var users = _warehouseDbContext
                .Users
                .ToList();

            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public UserDto GetUserDtoById(int id)
        {
            var user = GetUserById(id);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public List<UserDto> GetUserDtoByName(string name)
        {
            var users = _warehouseDbContext
                .Users
                .Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name))
                .ToList();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public List<UserDto> GetUserDtoByEmail(string email)
        {
            var users = _warehouseDbContext
                .Users
                .Where(x => x.Email.Contains(email))
                .ToList();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public List<UserDto> GetUserDtoByPhone(string phone)
        {
            var users = _warehouseDbContext
                .Users
                .Where(x => x.Phone.Contains(phone))
                .ToList();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public void DeleteById(int id)
        {
            var user = GetUserById(id);

            _warehouseDbContext.Users.Remove(user);
            _warehouseDbContext.SaveChanges();
        }

        private User GetUserById(int id)
        {
            var user = _warehouseDbContext
                .Users
                .FirstOrDefault(x => x.Id == id);
            if (user is null) throw new NotFound("User not found.");
            return user;
        }
    }
}
