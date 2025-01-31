using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Models;

[Table("OAuthProviders")]
[Index("ProviderName", Name = "UQ__OAuthPro__7D057CE5D375B8FE", IsUnique = true)]
public partial class OauthProvider
{
    [Key]
    public int ProviderId { get; set; }

    [StringLength(50)]
    public string ProviderName { get; set; } = null!;

    [InverseProperty("Provider")]
    public virtual ICollection<UserOauth> UserOauths { get; set; } = new List<UserOauth>();
}
