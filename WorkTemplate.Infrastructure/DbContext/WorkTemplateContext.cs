using System;
using WorkTemplate.Domain.Entity.Entities;

namespace WorkTemplate.Infrastructure.DbContext
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class WorkTemplateContext
        : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public WorkTemplateContext(DbContextOptions<WorkTemplateContext> options)
            : base(options)
        {
        }
    }
}
