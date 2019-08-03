using Microsoft.AspNetCore.Identity;
using System;

namespace WorkTemplate.Domain.Entity.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserRole")]
    public class UserRole : IdentityUserRole<Guid>
    {
    }
}
