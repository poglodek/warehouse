using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.User;
using warehouse.Exceptions;
using warehouse.Services.Authentication;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class UserServices : IUserServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserServices(WarehouseDbContext warehouseDbContext,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
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

        public int RegisterUser(UserCreatedDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Role = GetDefaultRole();
            var hashedPassword = _passwordHasher.HashPassword(user, user.HashedPassword);
            user.HashedPassword = hashedPassword;
            _warehouseDbContext.Users.Add(user);
            _warehouseDbContext.SaveChanges();
            return user.Id;
        }

        public string LoginUser(UserLoginDto userLoginDto)
        {
            var user = _warehouseDbContext
                .Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == userLoginDto.Email);
            if (user is null)
                throw new NotFound("User or password wrong.");
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, userLoginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new NotFound("User or password wrong.");

            var claims = GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.RoleName}")
            };
            return claims;
        }

        private Role GetDefaultRole()
        {
            return _warehouseDbContext.Roles.First();
        }

        public User GetUserById(int id)
        {
            var user = _warehouseDbContext
                .Users
                .FirstOrDefault(x => x.Id == id);
            if (user is null) throw new NotFound("User not found.");
            return user;
        }
    }
}