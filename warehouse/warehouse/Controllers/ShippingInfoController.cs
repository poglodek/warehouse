﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;

namespace warehouse.Controllers
{
    
    [ApiController]
    [Route("/shippingInfo")]
    public class ShippingInfoController : ControllerBase
    {

        public ShippingInfoController()
        {

        }
        [HttpGet("getAll")]
        public ActionResult getShippingInfoList()
        {
            return Ok();
        }
    }
}
