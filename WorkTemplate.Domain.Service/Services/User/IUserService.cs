using System.Threading.Tasks;
using WorkTemplate.Domain.Entity.ViewModels;

namespace WorkTemplate.Domain.Service.Services.User
{
    public interface IUserService
    {
        int Dummy();

        Task<UserTicketViewModel> LoginAsync(LoginViewModel credentials);
        Task<UserTicketViewModel> RefreshAsync(RefreshTicketViewModel credentials);
    }
}
