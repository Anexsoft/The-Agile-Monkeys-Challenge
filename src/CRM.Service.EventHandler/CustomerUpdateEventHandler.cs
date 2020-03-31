using CRM.Persistence.Database;
using CRM.Service.EventHandler.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler
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

        public async Task Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Customers.SingleAsync(x =>
                x.CustomerId == request.CustomerId
            );

            originalEntry.Name = request.Name;
            originalEntry.Surname = request.Surname;

            _context.Update(originalEntry);
            await _context.SaveChangesAsync();
        }
    }
}
