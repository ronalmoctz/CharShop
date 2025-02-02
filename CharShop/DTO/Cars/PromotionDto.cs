using CharShop.Variables;
using System.ComponentModel.DataAnnotations;

namespace CharShop.DTO.Cars
{
    public class PromotionDto
    {
        public Guid? PromotionId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Range(0.01, 99.99)]
        public decimal DiscountPercentage { get; set; }

        [FutureDate(ErrorMessage = "The start date must be a future date")]
        public DateTime StartDate { get; set; }

        [DateAfter("StartDate", ErrorMessage = "The end date must be after the start date")]
        public DateTime EndDate { get; set; }

        [Required]
        public Guid CarId { get; set; }
    }
}
