using CRM.Persistence.Database;
using CRM.Service.EventHandler.Identity.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Identity
{
    public class UserUpdateEventHandler :
        INotificationHandler<UserUpdateCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserLoginEventHandler> _logger;

        public UserUpdateEventHandler(
            ILogger<UserLoginEventHandler> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(UserUpdateCommand command, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Users.SingleAsync(x =>
                x.Id == command.UserId
            );

            originalEntry.Name = command.Name;
            originalEntry.Surname = command.Surname;

            await _context.SaveChangesAsync();
        }
    }
}
