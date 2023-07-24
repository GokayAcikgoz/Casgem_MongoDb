using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Casgem_MongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IEstateService _estateService;

        public EstateController(IEstateService estateService)
        {
            _estateService = estateService;
        }

        [HttpGet]
        public ActionResult<List<Estate>> Get()
        {
            return _estateService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Estate> Get(string id)
        {
            var essate = _estateService.Get(id);
            if (essate == null)
            {
                return NotFound($"Essate with Id = {id} not found");
            }

            return essate;
        }

        [HttpPost]
        public ActionResult<Estate> Post([FromBody] Estate estate)
        {

            _estateService.Create(estate);

            return CreatedAtAction(nameof(Get), new { id = estate.Id }, estate);
        }


        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Estate estate)
        {
            var existingEssate = _estateService.Get(id);
            if (existingEssate == null)
            {
                return NotFound($"Essate with Id = {id} not found");
            }

            _estateService.Update(id, estate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var essate = _estateService.Get(id);
            if (essate == null)
            {
                return NotFound($"Essate with Id = {id} not found");
            }

            _estateService.Remove(essate.Id);
            return Ok($"Essate with id = {id} deleted");
        }


        //[HttpGet("filter")]
        //public ActionResult<List<Estate>> GetByFilter([FromQuery] string? city = null, [FromQuery] string? type = null, 
        //    [FromQuery] int? room = null, [FromQuery] string? title = null, [FromQuery] int? price = null, [FromQuery] string? buildYear = null) 
        //{
        //    var estate = _estateService.GetByFilter(city, type, room, title, price, buildYear);

        //    if(estate.Count == 0)
        //    {
        //        return NotFound("No estate found for the specified filter");
        //    }
        //    return Ok(estate);
        //}


        [HttpGet("filter")]
        public ActionResult<List<Estate>> GetByFilter([FromQuery] string? city = null, [FromQuery] string? type = null,
            [FromQuery] int? room = null, [FromQuery] string? title = null, [FromQuery] int? price = null, [FromQuery] string? buildYear = null)
        {
            var estate = _estateService.GetByFilter(city, type, room, title, price, buildYear);

            if (estate.Count == 0)
            {
                return NotFound("No estate found for the specified filter");
            }
            return Ok(estate);
        }
    }
}

