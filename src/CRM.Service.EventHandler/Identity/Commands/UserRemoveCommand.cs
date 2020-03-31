using MediatR;

namespace CRM.Service.EventHandler.Identity.Commands
{
    public class UserRemoveCommand : INotification
    {
        public string UserId { get; set; }
    }
}
