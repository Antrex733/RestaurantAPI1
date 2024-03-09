
using AutoMapper;
using RestaurantAPI1.Models;
using RestaurantAPI1.Services;

namespace RestaurantAPI1.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _restaurantService.Delete(id);
            if (!isDeleted) return NotFound();

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = _restaurantService.Create(dto);

            return Created($"/api/restaurants/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDto = _restaurantService.GetAll();

            return Ok(restaurantsDto);
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurantDto = _restaurantService.GetById(id);

            if (restaurantDto == null)
                return NotFound();

            return Ok(restaurantDto);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody]UpdateRestaurantDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = _restaurantService.Update(id, dto);

            if (!isUpdated)
                return NotFound();

            return Ok();
        }
    }
}
