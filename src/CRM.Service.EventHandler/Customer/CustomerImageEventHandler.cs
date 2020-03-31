using CRM.Common.File;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Customer.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.EventHandler.Customer
{
    public class CustomerImageEventHandler :
        INotificationHandler<CustomerImageUploadCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageUploadService _imageUploadService;

        public CustomerImageEventHandler(
            ApplicationDbContext context,
            IImageUploadService imageUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;
        }

        public async Task Handle(CustomerImageUploadCommand command, CancellationToken cancellationToken)
        {
            var originalEntry = await _context.Customers.SingleAsync(x =>
                x.CustomerId == command.CustomerId
            );

            // Save and get the file path
            _imageUploadService.Attach(command.File);
            var filePath = await _imageUploadService.SaveAsync();

            originalEntry.Photo = filePath;

            await _context.SaveChangesAsync();
        }
    }
}
