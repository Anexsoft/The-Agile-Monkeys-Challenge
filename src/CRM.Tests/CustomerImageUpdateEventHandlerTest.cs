using CRM.Common.File;
using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Customer;
using CRM.Service.EventHandler.Customer.Commands;
using CRM.Tests.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Tests
{
    [TestClass]
    public class CustomerImageUpdateEventHandlerTest
    {
        private static IImageUploadService GetImageUploadService
        {
            get
            {
                var mock = new Mock<IImageUploadService>();
                mock.Setup(x => x.SaveAsync())
                    .Returns(Task.FromResult(Guid.NewGuid().ToString() + "-test.jpg"));

                return mock.Object;
            }
        }

        private static IFormFile GetIFormFile
        {
            get
            {
                return new Mock<IFormFile>().Object;
            }
        }

        [TestMethod]
        public async Task TryToAttachAnImageToCustomer()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            var handler = new CustomerImageEventHandler(context, GetImageUploadService);

            // Retrieve customer to Update
            var testCustomerId = GetTestCustomerId(context);

            // Update customer
            await handler.Handle(new CustomerImageUploadCommand
            {
                CustomerId = testCustomerId,
                File = GetIFormFile
            }, new CancellationToken());

            // Retrieve updated customer
            var modifiedEntry = context.Customers.Single(x => x.CustomerId == testCustomerId);

            // Check
            Assert.IsNotNull(modifiedEntry.Photo != null);
        }

        private int GetTestCustomerId(ApplicationDbContext context) 
        {
            // Insert record
            var entry = new Customer
            {
                Name = "Test",
                Surname = "Surname"
            };

            context.Add(entry);
            context.SaveChanges();

            // Retrieve from database
            return entry.CustomerId;
        }
    }
}
