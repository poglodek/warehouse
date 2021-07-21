using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Index;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Route("/Index")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IIndexServices _indexServices;

        public IndexController(IIndexServices indexServices)
        {
            _indexServices = indexServices;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<IndexDto>> getIndexList()
        {
            var indexes = _indexServices.GetIndexes();
            return Ok(indexes);
        }
    }
}
