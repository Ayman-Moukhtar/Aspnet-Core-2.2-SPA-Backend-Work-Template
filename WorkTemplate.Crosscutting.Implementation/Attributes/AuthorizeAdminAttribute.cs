using Microsoft.AspNetCore.Authorization;

namespace WorkTemplate.Crosscutting.Implementation.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        public AuthorizeAdminAttribute()
        {
            Roles = AppConstant.RoleName.Admin;
        }
    }
}
