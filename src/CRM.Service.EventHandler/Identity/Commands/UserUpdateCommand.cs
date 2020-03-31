using MediatR;

namespace CRM.Service.EventHandler.Identity.Commands
{
    public class UserUpdateCommand : UserBaseInformationCommand, INotification
    {
        public string UserId { get; set; }
    }
}
