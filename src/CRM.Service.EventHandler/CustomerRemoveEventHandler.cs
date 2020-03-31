using CRM.Persistence.Database;
using CRM.Service.EventHandler.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler
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

        public async Task Handle(CustomerRemoveCommand request, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Customers.SingleAsync(x =>
                x.CustomerId == request.CustomerId
            );

            // Soft delete
            originalEntry.IsDeleted = true;

            _context.Update(originalEntry);
            await _context.SaveChangesAsync();
        }
    }
}
