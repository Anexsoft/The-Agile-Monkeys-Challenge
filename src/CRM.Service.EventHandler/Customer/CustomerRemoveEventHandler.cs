using CRM.Persistence.Database;
using CRM.Service.EventHandler.Customer.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Customer
{
    public class CustomerRemoveEventHandler :
        INotificationHandler<CustomerRemoveCommand>
    {
        private readonly ApplicationDbContext _context;

        public CustomerRemoveEventHandler(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CustomerRemoveCommand command, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Customers.SingleAsync(x =>
                x.CustomerId == command.CustomerId
            );

            // Soft delete
            originalEntry.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}
