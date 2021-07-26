using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Order;
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
        public ActionResult<List<OrderListDto>> GetById()
        {
            var orders = _orderServices.GetAllOrdersListDto();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderInfoDto> GetById([FromRoute]int id )
        {
            var order = _orderServices.GetOrderInfoById(id);
            return Ok(order);
        }
        [HttpGet("GetByClientName")]
        public ActionResult<List<OrderListDto>> GetOrderListByName([FromQuery] string clientName)
        {
            var orders = _orderServices.GetOrderListByClientName(clientName);
            return Ok(orders);
        }
        [HttpGet("GetByStatus")]
        public ActionResult<List<OrderListDto>> GetOrderListByStatus([FromQuery] string status)
        {
            var order = _orderServices.GetOrderListByStatus(status);
            return Ok(order);
        }
        [HttpGet("GetByTarget")]
        public ActionResult<List<OrderListDto>> GetOrderListByTarget([FromQuery] string target)
        {
            var order = _orderServices.GetOrderListByTarget(target);
            return Ok(order);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        { 
            _orderServices.DeleteById(id);
            return NoContent();
        }
        [HttpPost]
        public ActionResult CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
           var orderId =  _orderServices.Create(orderCreateDto);
            return Created("/order/"+ orderId, null);
        }
        [HttpPut("{id}")]
        public ActionResult UpDateOrder([FromBody] OrderCreateDto orderUpdateDto, [FromRoute]int id)
        {
             _orderServices.Update(orderUpdateDto, id);
            return Ok();
        }
        [HttpPut("addItem/{id}")]
        public ActionResult AddItem([FromRoute] int id, [FromQuery]int itemId)
        {
            _orderServices.AddItem(id, itemId);
            return Ok();
        }
        [HttpDelete("removeItem/{id}")]
        public ActionResult RemoveItem([FromRoute] int id, [FromQuery] int itemId)
        {
            _orderServices.RemoveItem(id, itemId);
            return NoContent();
        }
    }
}
