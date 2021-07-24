using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;

namespace warehouse.Controllers
{
    [Route("/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [HttpGet("getAll")]
        public ActionResult getOrderList()
        {
            

            return Ok();
        }
        [HttpGet("/get/{orderId}")]
        public ActionResult getOrder([FromRoute]int orderId)
        {
            return Ok();
        }
    }
}
