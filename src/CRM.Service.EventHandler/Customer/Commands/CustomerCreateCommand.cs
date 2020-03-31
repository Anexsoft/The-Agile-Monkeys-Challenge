using MediatR;

namespace CRM.Service.EventHandler.Customer.Commands
{
    public class CustomerCreateCommand : CustomerBaseInformationCommand, IRequest<int>
    {

    }
}
