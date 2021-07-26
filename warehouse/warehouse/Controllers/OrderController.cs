using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Route("/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [HttpGet("GetAll")]
        public ActionResult GetById()
        {
            var orders = _orderServices.GetAllOrdersListDto();
            return Ok(orders);
        }

        [HttpGet("Get/{id}")]
        public ActionResult GetById([FromRoute]int id )
        {
            var order = _orderServices.GetOrderInfoById(id);
            return Ok(order);
        }
        [HttpGet("GetByClientName")]
        public ActionResult GetOrderList([FromQuery] string clientName)
        {
            var order = _orderServices.GetOrderInfoByClientName(clientName);
            return Ok(order);
        }

    }
}
