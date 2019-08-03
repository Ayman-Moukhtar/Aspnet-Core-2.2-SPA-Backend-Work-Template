using Microsoft.AspNetCore.Identity;
using System;

namespace WorkTemplate.Domain.Entity.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RoleClaim")]
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
    }
}
