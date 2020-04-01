using CRM.Api.Controllers;
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
    public class CustomerControllerGetAsyncActionTest
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
        public async Task TryToGetOkStatusCodeFromGetAsyncAction()
        {
            // Get Controller
            var controller = new CustomerController(GetILogger, GetMediator, GetCustomerQueryService);

            // Find a record by Customer Id
            var action = await controller.GetAsync(1);

            // Check
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TryToGetNotFoundStatusCodeFromGetAsyncAction()
        {
            // Get Controller
            var controller = new CustomerController(GetILogger, GetMediator, GetCustomerQueryService);

            // Find a record by Customer Id
            var action = await controller.GetAsync(2);

            // Check
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult));
        }
    }
}
