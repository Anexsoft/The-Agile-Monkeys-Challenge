using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.EventHandler.Customer;
using CRM.Service.EventHandler.Customer.Commands;
using CRM.Tests.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Tests
{
    [TestClass]
    public class CustomerUpdateEventHandlerTest
    {
        [TestMethod]
        public async Task TryToUpdateACustomer()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            var handler = new CustomerUpdateEventHandler(context);

            // Retrieve customer to Update
            var testCustomerId = GetTestCustomerId(context);

            // Update customer
            await handler.Handle(new CustomerUpdateCommand
            {
                CustomerId = testCustomerId,
                Name = "Test modified",
                Surname = "Surname modified"
            }, new CancellationToken());

            // Retrieve updated customer
            var modifiedEntry = context.Customers.Single(x => x.CustomerId == testCustomerId);

            // Check
            Assert.IsNotNull(modifiedEntry.Name.Equals("Test modified"));
        }

        [TestMethod]
        public async Task TryToUpdateACustomerAndCheckReferencesToWhoUpdatedIt()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            var handler = new CustomerUpdateEventHandler(context);

            // Retrieve customer to Update
            var testCustomerId = GetTestCustomerId(context);

            // Update customer
            await handler.Handle(new CustomerUpdateCommand
            {
                CustomerId = testCustomerId,
                Name = "Test modified",
                Surname = "Surname modified"
            }, new CancellationToken());

            // Retrieve updated customer
            var modifiedEntry = context.Customers.Single(x => x.CustomerId == testCustomerId);

            // Check
            Assert.IsNotNull(modifiedEntry.UpdatedBy != null && modifiedEntry.UpdatedAt != null);
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
