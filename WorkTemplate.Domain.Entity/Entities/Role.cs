using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkTemplate.Domain.Entity.Entities
{
    [Table("Role")]
    public class Role : IdentityRole<Guid>
    {
    }
}
