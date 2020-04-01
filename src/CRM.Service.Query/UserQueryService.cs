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
    public interface IUserQueryService
    {
        Task<DataCollection<UserDto>> GetAllAsync(int page, int take);
        Task<UserDto> GetAsync(string id);
    }

    public class UserQueryService : IUserQueryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserQueryService> _logger;

        public UserQueryService(
            ApplicationDbContext context,
            ILogger<UserQueryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DataCollection<UserDto>> GetAllAsync(int page, int take)
        {
            var collection = await _context.Users
                .OrderBy(x => x.UserName)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UserDto>>();
        }

        public async Task<UserDto> GetAsync(string id)
        {
            var entry = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (entry == null) 
            {
                _logger.LogWarning($"The user wasn't found by userId: {id}");
                return null;
            }

            return entry.MapTo<UserDto>();
        }
    }
}
