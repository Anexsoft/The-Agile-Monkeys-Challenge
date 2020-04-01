using CRM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Persistence.Database.Configuration
{
    public class ApplicationRoleConfiguration
    {
        public ApplicationRoleConfiguration(EntityTypeBuilder<ApplicationRole> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            var role = new ApplicationRole
            {
                Id = "25fe6bec-401c-4157-9e3e-0819fd31e68b",
                Name = "Admin"
            };

            entityBuilder.HasData(role);
        }
    }
}