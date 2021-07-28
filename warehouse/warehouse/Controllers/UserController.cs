using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using warehouse.Dto.User;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Route("/User")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("GetAll")]
        public ActionResult<List<UserDto>> GetClientsList()
        {
            var users = _userServices.GetAllUsers();
            return Ok(users);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById([FromRoute] int id)
        {
            var user = _userServices.GetUserDtoById(id);
            return Ok(user);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("GetByName")]
        public ActionResult<List<UserDto>> GetByName([FromQuery] string name)
        {
            var user = _userServices.GetUserDtoByName(name);
            return Ok(user);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("GetByEmail")]
        public ActionResult<List<UserDto>> GetByEmail([FromQuery] string email)
        {
            var user = _userServices.GetUserDtoByEmail(email);
            return Ok(user);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("GetByPhone")]
        public ActionResult<List<UserDto>> GetById([FromQuery] string phone)
        {
            var user = _userServices.GetUserDtoByPhone(phone);
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
            _userServices.DeleteById(id);
            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] UserCreatedDto user)
        {
            Console.WriteLine("asd");
            var userId = _userServices.RegisterUser(user);
            return Created("User/Get/" + userId, null);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<string> LoginUser([FromBody] UserLoginDto user)
        {
            var token = _userServices.LoginUser(user);
            return Ok(token);
        }
    }
}