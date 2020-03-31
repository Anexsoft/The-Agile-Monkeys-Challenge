using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Identity.Commands;
using CRM.Service.EventHandler.Identity.Exceptions;
using CRM.Service.EventHandler.Identity.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Identity
{
    public class UserLoginEventHandler :
        IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserLoginEventHandler> _logger;

        public UserLoginEventHandler(
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<UserLoginEventHandler> logger)
        {
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand command, CancellationToken cancellationToken)
        {
            var result = new IdentityAccess();

            var originalEntry = await _context.Users.SingleAsync(x => x.UserName == command.UserName);
            var response = await _signInManager.CheckPasswordSignInAsync(originalEntry, command.Password, false);

            if (response.Succeeded)
            {
                result.Succeeded = true;
                await GenerateToken(originalEntry, result);
            }
            else 
            {
                _logger.LogWarning($"Access denied to user {originalEntry.UserName}");
                throw new UserSignInException(originalEntry.UserName);
            }

            return result;
        }

        private async Task GenerateToken(ApplicationUser user, IdentityAccess identity)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _context.Roles
                                      .Where(x => x.UserRoles.Any(y => y.UserId == user.Id))
                                      .ToListAsync();

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role.Name)
                );
            }
        }
    }
}
