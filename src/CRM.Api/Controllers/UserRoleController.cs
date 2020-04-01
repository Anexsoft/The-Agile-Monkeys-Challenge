using CRM.Service.EventHandler.IdentityRole.Commands;
using CRM.Service.Query;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CRM.Api.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    [Route("v1/users/{userid}/roles")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserRoleController(
            ILogger<UserController> logger,
            IMediator mediator,
            IUserQueryService userQueryService)
        {
            _logger = logger;
            _mediator = mediator;
            _userQueryService = userQueryService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(string userid, UserRoleAddCommand command)
        {
            command.UserId = userid;
            await _mediator.Publish(command);

            return NoContent();
        }

        [HttpDelete("{role}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveAsync(string userid, string role)
        {
            await _mediator.Publish(new UserRoleRemoveCommand { 
                UserId = userid,
                Role = role
            });

            return NoContent();
        }
    }
}