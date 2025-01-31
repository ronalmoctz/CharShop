using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

[Index("Email", Name = "UQ__Users__A9D10534E6DFFA01", IsUnique = true)]
public partial class User
{
    [Key]
    public Guid UserId { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [InverseProperty("Customer")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("User")]
    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();

    [InverseProperty("User")]
    public virtual ICollection<UserOauth> UserOauths { get; set; } = new List<UserOauth>();
}
