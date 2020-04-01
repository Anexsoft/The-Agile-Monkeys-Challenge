using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.Query;
using CRM.Tests.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CRM.Tests
{
    [TestClass]
    public class CustomerQueryServiceTest
    {
        private ILogger<CustomerQueryService> GetIlogger
        {
            get
            {
                return new Mock<ILogger<CustomerQueryService>>().Object;
            }
        }

        [TestMethod]
        public async Task TryToGetACustomerThatExists()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            ICustomerQueryService queryService = new CustomerQueryService(context, GetIlogger);

            // Retrieve customer to Update
            var testCustomerId = GetTestCustomerId(context);

            // Retrieve the new record by USD code
            var record = await queryService.GetAsync(testCustomerId);

            // Check
            Assert.IsNotNull(record);
        }

        [TestMethod]
        public async Task TryToGetACustomerThatNotExists()
        {
            // Retrieve DbContext
            var context = ApplicationDbContextInMemory.Get();
            ICustomerQueryService queryService = new CustomerQueryService(context, GetIlogger);

            // Test customer
            var testCustomerId = 99999999;

            // Retrieve the new record by USD code
            var record = await queryService.GetAsync(testCustomerId);

            // Check
            Assert.IsNull(record);
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
