using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

public partial class UserDetail
{
    [Key]
    public Guid UserDetailId { get; set; }

    public Guid UserId { get; set; }

    [StringLength(150)]
    public string FullName { get; set; } = null!;

    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    public int Age { get; set; }

    [StringLength(255)]
    public string Address { get; set; } = null!;

    [StringLength(16)]
    public string? CreditCard { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserDetails")]
    public virtual User User { get; set; } = null!;
}
