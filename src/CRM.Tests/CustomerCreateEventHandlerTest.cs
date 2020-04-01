using CRM.Service.EventHandler.Customer;
using CRM.Service.EventHandler.Customer.Commands;
using CRM.Service.Query;
using CRM.Tests.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Tests
{
    [TestClass]
    public class CustomerCreateEventHandlerTest
    {
        [TestMethod]
        public async Task TryToCreateANewCustomer()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            var handler = new CustomerCreateEventHandler(context);

            // Insert record
            var lastInsertId = await handler.Handle(new CustomerCreateCommand
            {
                Name = "Test",
                Surname = "Surname"
            }, new CancellationToken());

            // Retrieve from database
            var entry = context.Customers.SingleOrDefault(x => x.CustomerId == lastInsertId);

            // Check
            Assert.IsNotNull(entry);
        }

        [TestMethod]
        public async Task TryToCreateANewCustomerAndCheckReferencesToWhoCreatedIt()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            var handler = new CustomerCreateEventHandler(context);

            // Insert record
            var lastInsertId = await handler.Handle(new CustomerCreateCommand
            {
                Name = "Test",
                Surname = "Surname"
            }, new CancellationToken());

            // Retrieve from database
            var entry = context.Customers.SingleOrDefault(x => x.CustomerId == lastInsertId);

            // Check
            Assert.IsTrue(entry.CreatedBy != null && entry.CreatedAt != null);
        }
    }
}
