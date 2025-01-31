using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

public partial class Sale
{
    [Key]
    public Guid SaleId { get; set; }

    public Guid CarId { get; set; }

    public Guid CustomerId { get; set; }

    public int PaymentMethodId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiscountAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SaleDate { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("Sales")]
    public virtual Car Car { get; set; } = null!;

    [InverseProperty("Sale")]
    public virtual ICollection<CarTitle> CarTitles { get; set; } = new List<CarTitle>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Sales")]
    public virtual User Customer { get; set; } = null!;

    [InverseProperty("Sale")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Sales")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
