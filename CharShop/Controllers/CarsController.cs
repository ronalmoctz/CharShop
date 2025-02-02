using CharShop.DTO.Cars;
using CharShop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CharShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();

            Log.Information("Get all cars");
            return Ok(cars);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCarById(Guid id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                Log.Warning("Car with ID {CarId} not found", id);
                return NotFound();
            }

            Log.Information("Retrieved car with ID {CarId}", id);
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarsDto carsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _carService.CreateCarAsync(carsDto);
            return CreatedAtAction(nameof(GetCarById), new { id = result.CardId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(Guid id, [FromBody] CarsDto carsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _carService.UpdateCarAsync(id, carsDto);
            return result ? NoContent() : NotFound();
        }


        [HttpPost("{carId}/promotions")]
        public async Task<IActionResult> AddPromotion(Guid carId, [FromBody] PromotionDto promotionDto)
        {
            promotionDto.CarId = carId;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _carService.AddPromotionAsync(promotionDto);
            return CreatedAtAction(nameof(GetCarById), new { id = carId }, result);
        }
    }
}
