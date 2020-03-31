using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Identity.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Customer
{
    public class UserRemoveEventHandler :
        INotificationHandler<UserRemoveCommand>
    {
        private readonly ApplicationDbContext _context;

        public UserRemoveEventHandler(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UserRemoveCommand command, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Users.SingleAsync(x => x.Id == command.UserId);

            _context.Remove(originalEntry);
            await _context.SaveChangesAsync();
        }
    }
}
