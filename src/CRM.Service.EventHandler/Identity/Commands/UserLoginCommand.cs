using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CRM.Service.EventHandler.Identity.Commands
{
    public class UserLoginCommand : IRequest<string>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
