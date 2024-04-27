using AutoMapper;
using RestaurantAPI1.Exceptions;

namespace RestaurantAPI1.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        void RemoveAll(int restaurantId);
    }
    public class DishService : IDishService
    {
        private readonly IMapper _mapper;
        private readonly RestaurantDbContext _context;

        public DishService(IMapper mapper, RestaurantDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var dish = _mapper.Map<Dish>(dto);

            _context.Add(dish);
            _context.SaveChanges();

            return dish.Id;
        }

        public void RemoveAll(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }
    }
}
