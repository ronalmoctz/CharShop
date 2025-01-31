using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

[Table("UserOAuth")]
public partial class UserOauth
{
    [Key]
    [Column("UserOAuthId")]
    public Guid UserOauthId { get; set; }

    public Guid UserId { get; set; }

    public int ProviderId { get; set; }

    [Column("OAuthUserId")]
    [StringLength(255)]
    public string OauthUserId { get; set; } = null!;

    [ForeignKey("ProviderId")]
    [InverseProperty("UserOauths")]
    public virtual OauthProvider Provider { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserOauths")]
    public virtual User User { get; set; } = null!;
}
