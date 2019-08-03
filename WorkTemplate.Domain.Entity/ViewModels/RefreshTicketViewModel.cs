using System.ComponentModel.DataAnnotations;

namespace WorkTemplate.Domain.Entity.ViewModels
{
    public class RefreshTicketViewModel
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
