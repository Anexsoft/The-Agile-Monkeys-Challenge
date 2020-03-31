using CRM.Common.LoggedIn;
using CRM.Domain;
using CRM.Persistence.Database.Configuration;
using CRM.Persistence.Database.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService
        )
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            UseMyCustomFilters(builder);
            DomainConfiguration(builder);
        }

        public override int SaveChanges()
        {
            this.UseAuditLogic(_currentUserService?.GetUserId);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UseAuditLogic(_currentUserService?.GetUserId);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UseAuditLogic(_currentUserService?.GetUserId);
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Implements our custom filters such a soft delete
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void UseMyCustomFilters(ModelBuilder modelBuilder)
        {
            this.UseSoftDelete(modelBuilder);
        }

        /// <summary>
        /// Custom constraints
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void DomainConfiguration(ModelBuilder modelBuilder)
        {
            new CustomerConfiguration(modelBuilder.Entity<Customer>());
        }
    }
}
