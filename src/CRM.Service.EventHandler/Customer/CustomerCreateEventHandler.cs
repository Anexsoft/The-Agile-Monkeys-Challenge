using CRM.Common.Mapper;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Customer.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Customer
{
    public class CustomerCreateEventHandler :
        IRequestHandler<CustomerCreateCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CustomerCreateEventHandler(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CustomerCreateCommand command, CancellationToken cancellationToken)
        {
            var entry = command.MapTo<Domain.Customer>();

            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();

            return entry.CustomerId;
        }
    }
}
