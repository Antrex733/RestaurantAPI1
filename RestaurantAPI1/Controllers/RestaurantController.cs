
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI1.Models;
using RestaurantAPI1.Services;

namespace RestaurantAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            _restaurantService.Delete(id);

            return NoContent();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            int id = _restaurantService.Create(dto);

            return Created($"/api/restaurants/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDto = _restaurantService.GetAll();

            return Ok(restaurantsDto);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurantDto = _restaurantService.GetById(id);

            return Ok(restaurantDto);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody]UpdateRestaurantDto dto)
        {
            _restaurantService.Update(id, dto);

            return Ok();
        }
    }
}
