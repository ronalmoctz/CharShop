using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

[Index("TitleNumber", Name = "UQ__CarTitle__4187EF9ECEAA4ED3", IsUnique = true)]
public partial class CarTitle
{
    [Key]
    public Guid TitleId { get; set; }

    public Guid SaleId { get; set; }

    [StringLength(50)]
    public string TitleNumber { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime IssuedAt { get; set; }

    [ForeignKey("SaleId")]
    [InverseProperty("CarTitles")]
    public virtual Sale Sale { get; set; } = null!;
}
