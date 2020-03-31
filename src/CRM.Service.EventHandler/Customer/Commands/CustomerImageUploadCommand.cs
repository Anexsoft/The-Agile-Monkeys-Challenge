using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CRM.Service.EventHandler.Customer.Commands
{
    public class CustomerImageUploadCommand : INotification
    {
        public int CustomerId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
