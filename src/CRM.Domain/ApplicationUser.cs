using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CRM.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}
