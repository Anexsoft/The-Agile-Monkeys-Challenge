using MediatR;

namespace CRM.Service.EventHandler.IdentityRole.Commands
{
    public class UserRoleRemoveCommand : INotification
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
