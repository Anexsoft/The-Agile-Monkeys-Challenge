using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CRM.Common.LoggedIn
{
    public interface ICurrentUserService
    {
        string GetUserId { get; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var claims = _httpContextAccessor.HttpContext.User.Claims;

                    if (claims.Any(x => x.Type.Equals("sub")))
                    {
                        return claims.Where(x => x.Type.Equals("sub")).First().Value;
                    }
                }

                return null;
            }
        }
    }
}
