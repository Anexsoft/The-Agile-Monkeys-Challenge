using CRM.Service.EventHandler.Customer.Commands;
using CRM.Service.Query;
using CRM.Service.Query.DTOs;
using CRM.Service.Query.Extensions.Paging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CRM.Client.Controllers
{
    [ApiController]
    [Route("v1/customers")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerQueryService _customerQueryService;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(
            ILogger<CustomerController> logger,
            IMediator mediator,
            ICustomerQueryService customerQueryService)
        {
            _logger = logger;
            _mediator = mediator;
            _customerQueryService = customerQueryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<DataCollection<CustomerDto>> GetAll(int page = 1, int take = 10)
        {
            return await _customerQueryService.GetAllAsync(page, take);
        }

        [HttpGet("{id}", Name = "CustomerGetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetAsync(int id)
        {
            var result = await _customerQueryService.GetAsync(id);

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
        public async Task<IActionResult> Create(CustomerCreateCommand notification)
        {
            var entryId = await _mediator.Send(notification);

            return CreatedAtRoute(
                "CustomerGetById",
                new { id = entryId },
                null
            );
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, CustomerUpdateCommand notification)
        {
            notification.CustomerId = id;
            await _mediator.Publish(notification);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Publish(new CustomerRemoveCommand { 
                CustomerId = id
            });

            return NoContent();
        }

        [HttpPut("{id}/image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Upload(int id, IFormFile file)
        {
            await _mediator.Publish(new CustomerImageUploadCommand
            {
                CustomerId = id,
                File = file
            });

            return NoContent();
        }
    }
}
