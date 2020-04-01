using CRM.Domain;
using CRM.Service.EventHandler.IdentityRole.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.IdentityRole
{
    public class UserRoleRemoveEventHandler :
        INotificationHandler<UserRoleRemoveCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRoleRemoveEventHandler> _logger;

        public UserRoleRemoveEventHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<UserRoleRemoveEventHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task Handle(UserRoleRemoveCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            await _userManager.RemoveFromRoleAsync(user, command.Role);
        }
    }
}
