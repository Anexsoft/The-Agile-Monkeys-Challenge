using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CRM.Domain
{
    public class ApplicationRole : IdentityRole
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}
