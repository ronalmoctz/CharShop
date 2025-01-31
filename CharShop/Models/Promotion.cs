using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

public partial class Promotion
{
    [Key]
    public Guid PromotionId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiscountPercentage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    public Guid CarId { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("Promotions")]
    public virtual Car Car { get; set; } = null!;
}
