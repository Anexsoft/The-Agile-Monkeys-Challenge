using CRM.Client.Controllers;
using CRM.Service.EventHandler.Customer.Commands;
using CRM.Service.Query;
using CRM.Service.Query.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CRM.Tests
{
    [TestClass]
    public class CustomerControllerCreateAsyncActionTest
    {
        private ILogger<CustomerController> GetILogger
        {
            get
            {
                return new Mock<ILogger<CustomerController>>().Object;
            }
        }

        private ICustomerQueryService GetCustomerQueryService
        {
            get
            {
                var mock = new Mock<ICustomerQueryService>();
                var test = new CustomerDto
                {
                    CustomerId = 1
                };

                mock.Setup(x =>
                    x.GetAsync(It.Is<int>(x => x.Equals(test.CustomerId)))
                ).Returns(System.Threading.Tasks.Task.FromResult(test));

                return mock.Object;
            }
        }

        private IMediator GetMediator
        {
            get
            {
                return new Mock<IMediator>().Object;
            }
        }

        [TestMethod]
        public async Task TryToGetCreatedStatusCodeFromCreateAsyncAction()
        {
            // Get Controller
            var controller = new CustomerController(GetILogger, GetMediator, GetCustomerQueryService);

            // Find a record by Customer Id
            var action = await controller.CreateAsync(new CustomerCreateCommand());

            // Check
            Assert.IsInstanceOfType(action, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        public async Task TryToGetBadRequestStatusCodeFromCreateAsyncAction()
        {
            // Get Controller
            var controller = new CustomerController(GetILogger, GetMediator, GetCustomerQueryService);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Find a record by Customer Id
            var action = await controller.CreateAsync(new CustomerCreateCommand());

            // Check
            Assert.IsInstanceOfType(action, typeof(BadRequestObjectResult));
        }
    }
}
