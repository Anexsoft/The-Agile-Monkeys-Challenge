using CRM.Common.Mapper;
using CRM.Persistence.Database;
using CRM.Service.Query.DTOs;
using CRM.Service.Query.Extensions.Paging;
using Microsoft.EntityFrameworkCore;
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

        public UserQueryService(
            ApplicationDbContext context)
        {
            _context = context;
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
                return null;
            }

            return entry.MapTo<UserDto>();
        }
    }
}
