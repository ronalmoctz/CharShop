using AutoMapper;
using CharShop.Data;
using CharShop.DTO.Cars;
using CharShop.Interfaces;
using CharShop.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CharShop.Services
{
    public class CarService : ICarService
    {
        private readonly CarShopDbContext _context;

        public CarService(CarShopDbContext context)
        {
            _context = context;
        }

        public async Task<PromotionDto> AddPromotionAsync(PromotionDto promotionDto)
        {
            var promotion = new Promotion
            {
                PromotionId = Guid.NewGuid(),
                CarId = promotionDto.CarId,
                Name = promotionDto.Name,
                DiscountPercentage = promotionDto.DiscountPercentage,
                StartDate = promotionDto.StartDate,
                EndDate = promotionDto.EndDate
            };

            await _context.Promotions.AddAsync(promotion);
            await _context.SaveChangesAsync();

            Log.Information("Promotion added to car {CarId}", promotionDto.CarId);
            return promotionDto;
        }

        public async Task<CarsDto> CreateCarAsync(CarsDto carsDto)
        {
            var Car = new Car
            {
                CarId = Guid.NewGuid(),
                Brad = carsDto.Brand,
                Moodel = carsDto.Moodel,
                Year = carsDto.Year,
                Price = carsDto.Price,
                IsAvailable = carsDto.IsAvailable,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Cars.AddAsync(Car);
            await _context.SaveChangesAsync();

            Log.Information("Car created {CarId}", Car.CarId);
            return carsDto;
        }

        public async Task<IEnumerable<CarsDto>> GetAllCarsAsync()
        {
            var cars = await _context.Cars
                .Include(c => c.Promotions)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Customer)
                .AsNoTracking()
                .ToListAsync();

            return cars.Select( car => new CarsDto
            {
                CardId = car.CarId,
                Brand = car.Brad,
                Moodel = car.Moodel,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                IsAvailable = car.IsAvailable,
            });
            
        }

        public async Task<CarsDto?> GetCarByIdAsync(Guid id)
        {
            var car = await _context.Cars
                .Include(c => c.Promotions)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CarId == id);

            if (car == null) return null;

            return new CarsDto
            {
                CardId = car.CarId,
                Brand = car.Brad,
                Moodel = car.Moodel,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                IsAvailable = car.IsAvailable,
            };
        }

        public async Task<bool> UpdateCarAsync(Guid id, CarsDto carDto)
        {
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null) return false;

            existingCar.Brad = carDto.Brand;
            existingCar.Moodel = carDto.Moodel;
            existingCar.Year = carDto.Year;
            existingCar.Color = carDto.Color;
            existingCar.Price = carDto.Price;
            existingCar.IsAvailable = carDto.IsAvailable;
            existingCar.UpdatedAt = DateTime.UtcNow;

            _context.Cars.Update(existingCar);
            await _context.SaveChangesAsync();

            Log.Information("Car updated {CarId}", id);
            return true;
        }

        public async Task<bool> UpdateCarStatusAsync(Guid id, bool isAvailable)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;

            car.IsAvailable = isAvailable;
            car.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            Log.Information("Car status updated {CarId}", id);
            return true;
        }
    }
}
