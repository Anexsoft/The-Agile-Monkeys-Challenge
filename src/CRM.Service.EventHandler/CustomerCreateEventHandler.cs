using CRM.Common.Mapper;
using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler
{
    public class CustomerEventHandler :
        IRequestHandler<CustomerCreateCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CustomerEventHandler(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var entry = request.MapTo<Customer>();

            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();

            return entry.CustomerId;
        }
    }
}
