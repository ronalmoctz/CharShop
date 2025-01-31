using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

public partial class Car
{
    [Key]
    public Guid CarId { get; set; }

    [StringLength(50)]
    public string Brad { get; set; } = null!;

    [StringLength(50)]
    public string Moodel { get; set; } = null!;

    public int Year { get; set; }

    [StringLength(30)]
    public string Color { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Car")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("Car")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
