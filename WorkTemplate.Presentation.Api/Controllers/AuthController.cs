using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkTemplate.Domain.Entity.ViewModels;
using WorkTemplate.Domain.Service.Services.User;

namespace WorkTemplate.Presentation.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Lazy<IUserService> _userService;

        public AuthController(
            Lazy<IUserService> userService
            )
        {
            _userService = userService;
        }

        [HttpGet]
        public int Get()
        {
            return _userService.Value.Dummy();
        }

        [HttpPost("login")]
        public async Task<UserTicketViewModel> Login([FromBody]LoginViewModel model)
        {
            return await _userService.Value.LoginAsync(model);
        }

        [HttpPost("refresh")]
        public async Task<UserTicketViewModel> Refresh([FromBody]RefreshTicketViewModel model)
        {
            return await _userService.Value.RefreshAsync(model);
        }
    }
}
