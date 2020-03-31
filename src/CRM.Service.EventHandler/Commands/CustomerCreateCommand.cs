using CRM.Common.Validator;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CRM.Service.EventHandler.Commands
{
    public class CustomerCreateCommand : IRequest<int>
    {
        [Required, MinLength(2), MaxLength(20)]
        [CrossSiteScripting]
        public string Name { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        [CrossSiteScripting]
        public string Surname { get; set; }
    }
}
