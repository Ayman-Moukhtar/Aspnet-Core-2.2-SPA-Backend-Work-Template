using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkTemplate.Domain.Entity.Entities
{
    [Table("User")]
    public class User : IdentityUser<Guid>
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryDate { get; set; }
    }
}
