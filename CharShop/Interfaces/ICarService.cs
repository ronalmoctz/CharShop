using CharShop.DTO.Cars;

namespace CharShop.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarsDto>> GetAllCarsAsync();
        Task<CarsDto?> GetCarByIdAsync(Guid id); 
        Task<CarsDto> CreateCarAsync(CarsDto car);
        Task<bool> UpdateCarAsync(Guid id, CarsDto carDto);
        Task<bool> UpdateCarStatusAsync(Guid id, bool isAvailable);
        Task<PromotionDto> AddPromotionAsync(PromotionDto promotionDto);
    }
}
