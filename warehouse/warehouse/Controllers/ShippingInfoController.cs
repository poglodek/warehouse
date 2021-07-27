using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto;
using warehouse.Dto.ShippingInfo;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    
    [ApiController]
    [Route("/ShippingInfo")]
    public class ShippingInfoController : ControllerBase
    {
        private readonly IShippingInfoServices _shippingInfoServices;

        public ShippingInfoController(IShippingInfoServices shippingInfoServices)
        {
            _shippingInfoServices = shippingInfoServices;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<ShippingInfoDto>> GetShippingInfoList()
        {
            var shippingInfo = _shippingInfoServices.GetShippingInfoDtoList();
            return Ok(shippingInfo);
        }
        [HttpGet("{id}")]
        public ActionResult<List<ShippingInfoDto>> GetShippingInfoById([FromRoute]int id)
        {
            var shippingInfo = _shippingInfoServices.GetShippingInfoDtoById(id);
            return Ok(shippingInfo);
        }
        [HttpGet("target")]
        public ActionResult<List<ShippingInfoDto>> GetShippingInfoById([FromQuery] string target)
        {
            var shippingInfo = _shippingInfoServices.GetShippingInfoDtoByTarget(target);
            return Ok(shippingInfo);
        }
        [HttpGet("trackNumber")]
        public ActionResult<List<ShippingInfoDto>> GetShippingInfoByTrackNumber([FromQuery]string trackNumber)
        {
            var shippingInfo = _shippingInfoServices.GetShippingInfoDtoByTrackNumber(trackNumber);
            return Ok(shippingInfo);
        }
        [HttpGet("priority")]
        public ActionResult<List<ShippingInfoDto>> GetShippingInfoByPriority([FromQuery] string priority)
        {
            var shippingInfo = _shippingInfoServices.GetShippingInfoDtoByPriority(priority);
            return Ok(shippingInfo);
        }
        [HttpGet("client/{id}")]
        public ActionResult<List<ShippingInfoDto>> GetShippingInfoByClientId([FromRoute] int  id)
        {
            var shippingInfo = _shippingInfoServices.GetShippingInfoDtoByClientId(id);
            return Ok(shippingInfo);
        }
        [HttpPost]
        public ActionResult<List<ShippingInfoDto>> CreateShippingInfo([FromBody] ShippingInfoCreateDto shippingInfoCreateDto)
        {
            var id = _shippingInfoServices.CreateShippingInfo(shippingInfoCreateDto);
            return Created("/ShippingInfo/get/" + id, null);
        }
        [HttpPatch("{id}")]
        public ActionResult<List<ShippingInfoDto>> UpdateShippingInfo([FromBody] ShippingInfoUpdateDto shippingInfoUpdateDto,[FromRoute] int id )
        {
            _shippingInfoServices.UpdateShippingInfo(shippingInfoUpdateDto, id);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<List<ShippingInfoDto>> DeleteShippingInfoById([FromRoute] int id)
        {
            _shippingInfoServices.DeleteShippingInfoById(id);
            return NoContent();
        }
    }
}
