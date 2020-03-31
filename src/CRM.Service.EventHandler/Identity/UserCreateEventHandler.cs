using CRM.Domain;
using CRM.Service.EventHandler.Identity.Commands;
using CRM.Service.EventHandler.Identity.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Identity
{
    public class UserCreateEventHandler :
        IRequestHandler<UserCreateCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserLoginEventHandler> _logger;

        public UserCreateEventHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<UserLoginEventHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<string> Handle(UserCreateCommand command, CancellationToken cancellationToken)
        {
            var entry = new ApplicationUser
            {
                Email = command.Email,
                UserName = command.Email,
                Name = command.Name,
                Surname = command.Surname
            };

            var result = await _userManager.CreateAsync(entry, command.Password);

            if (!result.Succeeded) 
            {
                var error = result.Errors.First().Description;

                _logger.LogWarning($"User could not be created by ${entry.UserName}");
                _logger.LogError(error);

                throw new UserCreationException(error);
            }

            return entry.Id;
        }
    }
}
