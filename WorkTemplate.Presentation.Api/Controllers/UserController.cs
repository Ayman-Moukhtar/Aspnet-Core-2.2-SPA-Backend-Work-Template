using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkTemplate.Domain.Entity.ViewModels;
using WorkTemplate.Domain.Service.Services.User;

namespace WorkTemplate.Presentation.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Lazy<IUserService> _userService;

        public UserController(
            Lazy<IUserService> userService
            )
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public int Get()
        {
            return _userService.Value.Dummy();
        }

        [HttpPost("auth/login")]
        public async Task<UserTicketViewModel> Login([FromBody]LoginViewModel model)
        {
            return await _userService.Value.LoginAsync(model);
        }

        [HttpPost("auth/refresh")]
        public async Task<UserTicketViewModel> Refresh([FromBody]RefreshTicketViewModel model)
        {
            return await _userService.Value.RefreshAsync(model);
        }
    }
}
