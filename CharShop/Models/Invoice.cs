using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

[Index("InvoiceNumber", Name = "UQ__Invoices__D776E981485F7E95", IsUnique = true)]
public partial class Invoice
{
    [Key]
    public Guid InvoiceId { get; set; }

    public Guid SaleId { get; set; }

    [StringLength(50)]
    public string InvoiceNumber { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime GeneratedAt { get; set; }

    [ForeignKey("SaleId")]
    [InverseProperty("Invoices")]
    public virtual Sale Sale { get; set; } = null!;
}
