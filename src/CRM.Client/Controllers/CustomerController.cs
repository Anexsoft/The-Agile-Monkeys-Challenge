using CRM.Service.EventHandler.Commands;
using CRM.Service.Query;
using CRM.Service.Query.DTOs;
using CRM.Service.Query.Extensions.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CRM.Client.Controllers
{
    [ApiController]
    [Route("v1/customers")]
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
        public async Task<DataCollection<CustomerDto>> GetAllAsync(int page = 1, int take = 10)
        {
            return await _customerQueryService.GetAllAsync(page, take);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _customerQueryService.GetAsync(id);

            if (result == null) 
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerCreateCommand notification)
        {
            var entryId = await _mediator.Send(notification);

            return CreatedAtRoute(
                "GetById",
                new { id = entryId },
                null
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CustomerUpdateCommand notification)
        {
            notification.CustomerId = id;
            await _mediator.Publish(notification);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _mediator.Publish(new CustomerRemoveCommand { 
                CustomerId = id
            });

            return NoContent();
        }

        [HttpPut("{id}/image")]
        public async Task<IActionResult> UploadAsync(int id, CustomerImageUploadCommand notification)
        {
            return NoContent();
        }
    }
}
