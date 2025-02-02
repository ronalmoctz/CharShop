using System.ComponentModel.DataAnnotations;

namespace CharShop.DTO.Cars
{
    public class CarTitleDto
    {
        public Guid? TitleId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string TitleNumber { get; set; }

        [Required]
        public DateTime IssuedAt { get; set; }

        [Required]
        public Guid SaleId { get; set; }
    }
}
