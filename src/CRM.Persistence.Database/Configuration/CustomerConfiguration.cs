using CRM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CRM.Persistence.Database.Configuration
{
    public class CustomerConfiguration
    {
        public CustomerConfiguration(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.HasKey(x => x.CustomerId);

            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Photo).HasMaxLength(150);
        }
    }
}
