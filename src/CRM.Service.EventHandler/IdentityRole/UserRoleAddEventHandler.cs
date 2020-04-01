using CRM.Domain;
using CRM.Service.EventHandler.IdentityRole.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.IdentityRole
{
    public class UserRoleAddEventHandler :
        INotificationHandler<UserRoleAddCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRoleAddEventHandler> _logger;

        public UserRoleAddEventHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<UserRoleAddEventHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task Handle(UserRoleAddCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            await _userManager.AddToRoleAsync(user, command.Role);
        }
    }
}
