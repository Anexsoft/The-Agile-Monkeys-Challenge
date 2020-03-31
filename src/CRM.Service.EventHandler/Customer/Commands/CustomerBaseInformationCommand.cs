using CRM.Common.Validator;
using System.ComponentModel.DataAnnotations;

namespace CRM.Service.EventHandler.Customer.Commands
{
    public abstract class CustomerBaseInformationCommand
    {
        [Required, MinLength(3), MaxLength(20)]
        [CrossSiteScripting]
        public string Name { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        [CrossSiteScripting]
        public string Surname { get; set; }
    }
}
