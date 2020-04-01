using CRM.Common.Token;
using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Identity.Commands;
using CRM.Service.EventHandler.Identity.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Identity
{
    public class UserLoginEventHandler :
        IRequestHandler<UserLoginCommand, string>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserLoginEventHandler> _logger;
        private readonly ITokenCreationService _tokenCreationService;

        public UserLoginEventHandler(
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            ILogger<UserLoginEventHandler> logger,
            ITokenCreationService tokenCreationService)
        {
            _tokenCreationService = tokenCreationService;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        public async Task<string> Handle(UserLoginCommand command, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Users.SingleAsync(x => x.UserName == command.UserName);
            var response = await _signInManager.CheckPasswordSignInAsync(originalEntry, command.Password, false);

            if (!response.Succeeded)
            {
                _logger.LogWarning($"Access denied to user {originalEntry.UserName}");
                throw new UserSignInException(originalEntry.UserName);
            }

            return await _tokenCreationService.CreateAsync(command.UserName, command.Password);
        }
    }
}
