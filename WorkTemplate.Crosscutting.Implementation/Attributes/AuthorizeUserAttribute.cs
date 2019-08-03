using Microsoft.AspNetCore.Authorization;

namespace WorkTemplate.Crosscutting.Implementation.Attributes
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public AuthorizeUserAttribute()
        {
            Roles = AppConstant.RoleName.User;
        }
    }
}
