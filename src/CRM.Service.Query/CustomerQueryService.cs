using CRM.Common.Mapper;
using CRM.Persistence.Database;
using CRM.Service.Query.DTOs;
using CRM.Service.Query.Extensions.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Service.Query
{
    public interface ICustomerQueryService
    {
        Task<DataCollection<CustomerDto>> GetAllAsync(int page = 1, int take = 10);
        Task<CustomerDto> GetAsync(int customerId);
    }

    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerQueryService> _logger;

        public CustomerQueryService(
            ApplicationDbContext context,
            ILogger<CustomerQueryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DataCollection<CustomerDto>> GetAllAsync(int page = 1, int take = 10) 
        {
            var collection = await _context.Customers
                .OrderBy(x => x.Name)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<CustomerDto>>();
        }

        public async Task<CustomerDto> GetAsync(int customerId)
        {
            var entry = await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId);

            if (entry == null) 
            {
                _logger.LogWarning($"The customer wasn't found by customerId: {customerId}");
                return null;
            }

            return entry.MapTo<CustomerDto>();
        }
    }
}
