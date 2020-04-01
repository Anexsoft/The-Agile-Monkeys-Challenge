using CRM.Common.LoggedIn;
using CRM.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace CRM.Tests.Configuration
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDbContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TheApiCRM")
                .Options;

            return new ApplicationDbContext(options, GetCurrentUserService);
        }

        private static ICurrentUserService GetCurrentUserService
        {
            get
            {
                var mock = new Mock<ICurrentUserService>();
                mock.Setup(x => x.GetUserId).Returns(Guid.NewGuid().ToString());

                return mock.Object;
            }
        }
    }
}
