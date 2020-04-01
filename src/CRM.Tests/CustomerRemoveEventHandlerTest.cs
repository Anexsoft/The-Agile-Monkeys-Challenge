using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Customer;
using CRM.Service.EventHandler.Customer.Commands;
using CRM.Tests.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Tests
{
    [TestClass]
    public class CustomerRemoveEventHandlerTest
    {
        [TestMethod]
        public async Task TryToRemoveACustomer()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            var handler = new CustomerRemoveEventHandler(context);

            // Get test customer
            var testCustomerId = GetTestCustomerId(context);

            // Retrieve from database
            await handler.Handle(new CustomerRemoveCommand
            {
                CustomerId = testCustomerId
            }, new CancellationToken());

            var entry = context.Customers.SingleOrDefault(x => x.CustomerId == testCustomerId);

            // Check
            Assert.IsNull(entry);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task TryToRemoveACustomerThatDoesntExists()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            var handler = new CustomerRemoveEventHandler(context);

            // Set test customer
            var testCustomerId = 999999999;

            // Retrieve from database
            await handler.Handle(new CustomerRemoveCommand
            {
                CustomerId = testCustomerId
            }, new CancellationToken());
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
