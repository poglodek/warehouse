﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using warehouse.Dto.Client;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Authorize]
    [Route("/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<ClientDto>> GetClientsList()
        {
            var clients = _clientServices.GetAllClients();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDto> GetById([FromRoute] int id)
        {
            var client = _clientServices.GetClientDtoById(id);
            return Ok(client);
        }

        [HttpGet("GetByName")]
        public ActionResult<List<ClientDto>> GetByName([FromQuery] string name)
        {
            var clients = _clientServices.GetClientsByName(name);
            return Ok(clients);
        }

        [HttpGet("GetByAddress")]
        public ActionResult<List<ClientDto>> GetByAddress([FromQuery] string address)
        {
            var clients = _clientServices.GetClientsByAddress(address);
            return Ok(clients);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
            _clientServices.DeleteById(id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateClient([FromBody] ClientDto clientDto)
        {
            var newClientId = _clientServices.CreateClient(clientDto);
            return Created("/Client/get/" + newClientId, null);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult UpdateClient([FromBody] ClientDto clientDto, [FromRoute] int id)
        {
            _clientServices.UpdateClient(clientDto, id);
            return Ok();
        }
    }
}