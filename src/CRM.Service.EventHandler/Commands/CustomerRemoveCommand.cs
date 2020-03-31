using MediatR;

namespace CRM.Service.EventHandler.Commands
{
    public class CustomerRemoveCommand : INotification
    {
        public int CustomerId { get; set; }
    }
}
