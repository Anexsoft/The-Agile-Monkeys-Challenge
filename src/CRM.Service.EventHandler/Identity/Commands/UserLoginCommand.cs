using CRM.Service.EventHandler.Identity.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CRM.Service.EventHandler.Identity.Commands
{
    public class UserLoginCommand : IRequest<IdentityAccess>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
