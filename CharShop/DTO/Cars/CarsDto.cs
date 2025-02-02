using System.ComponentModel.DataAnnotations;

namespace CharShop.DTO.Cars
{
    public class CarsDto
    {
        public Guid? CardId { get; set; }
        [Required(ErrorMessage = "Brand name is required")]
        [StringLength(50, ErrorMessage = "Brand name is too long max 50 characters")]
        public required string Brand { get; set; }

        [Required(ErrorMessage = "Model name is required")]
        [StringLength(50, ErrorMessage = "Model name is too long max 50 characters")]
        public required string Moodel { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
        public required int Year { get; set; }

        [StringLength(30, ErrorMessage = "Color name is too long max 30 characters")]
        public required string Color { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public required decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }

    public class ICarStatus
    {
        [Required]
        public bool IsAvailable { get; set; }
    }
}
