using MediatR;

namespace CRM.Service.EventHandler.Customer.Commands
{
    public class CustomerRemoveCommand : INotification
    {
        public int CustomerId { get; set; }
    }
}