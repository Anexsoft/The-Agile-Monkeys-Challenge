using CRM.Common.Validator;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CRM.Service.EventHandler.Identity.Commands
{
    public class UserCreateCommand : UserBaseInformationCommand, IRequest<string>
    {
        [Required]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(10)]
        public string Password { get; set; }
    }
}
