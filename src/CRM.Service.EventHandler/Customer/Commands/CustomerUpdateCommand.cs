using MediatR;

namespace CRM.Service.EventHandler.Customer.Commands
{
    public class CustomerUpdateCommand : CustomerBaseInformationCommand, INotification
    {
        public int CustomerId { get; set; }
    }
}