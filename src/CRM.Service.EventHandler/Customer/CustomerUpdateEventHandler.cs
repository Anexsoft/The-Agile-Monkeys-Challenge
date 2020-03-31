using CRM.Persistence.Database;
using CRM.Service.EventHandler.Customer.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Customer
{
    public class CustomerUpdateEventHandler :
        INotificationHandler<CustomerUpdateCommand>
    {
        private readonly ApplicationDbContext _context;

        public CustomerUpdateEventHandler(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CustomerUpdateCommand command, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Customers.SingleAsync(x =>
                x.CustomerId == command.CustomerId
            );

            originalEntry.Name = command.Name;
            originalEntry.Surname = command.Surname;

            await _context.SaveChangesAsync();
        }
    }
}
