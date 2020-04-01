using CRM.Service.EventHandler.Identity.Commands;
using CRM.Service.EventHandler.Identity.Exceptions;
using CRM.Service.Query;
using CRM.Service.Query.DTOs;
using CRM.Service.Query.Extensions.Paging;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CRM.Api.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    [Route("v1/users")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(
            ILogger<UserController> logger,
            IMediator mediator,
            IUserQueryService userQueryService)
        {
            _logger = logger;
            _mediator = mediator;
            _userQueryService = userQueryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<DataCollection<UserDto>> GetAllAsync(int page = 1, int take = 10)
        {
            return await _userQueryService.GetAllAsync(page, take);
        }

        [HttpGet("{id}", Name = "UserGetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetAsync(string id)
        {
            var result = await _userQueryService.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(UserCreateCommand command)
        {
            try
            {
                var entryId = await _mediator.Send(command);

                return CreatedAtRoute(
                    "UserGetById",
                    new { id = entryId },
                    null
                );
            }
            catch (UserCreationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(string id, UserUpdateCommand command)
        {
            command.UserId = id;
            await _mediator.Publish(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveAsync(string id)
        {
            await _mediator.Publish(new UserRemoveCommand
            {
                UserId = id
            });

            return NoContent();
        }
    }
}