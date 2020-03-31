using CRM.Domain.Feautures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CRM.Persistence.Database.Extensions
{
    public static class DbContextExtension
    {
        public static void UseSoftDelete(this DbContext context, ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                        .Where(entityType => typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                        .ToList()
                        .ForEach(entityType =>
                        {
                            modelBuilder.Entity(entityType.ClrType)
                            .HasQueryFilter(ConvertFilterExpression<ISoftDelete>(x => !x.IsDeleted, entityType.ClrType));
                        });

            LambdaExpression ConvertFilterExpression<TInterface>(
                                       Expression<Func<TInterface, bool>> filterExpression,
                                       Type entityType)
            {
                var newParam = Expression.Parameter(entityType, filterExpression.Parameters[0].Name);
                var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);
                return Expression.Lambda(newBody, newParam);
            }
        }

        public static void UseAuditLogic(this DbContext context, string userId)
        {
            var modifiedEntries = context.ChangeTracker.Entries().Where(
                            x => x.Entity is Audit
                                 && (x.State == EntityState.Added || x.State == EntityState.Modified)
                        );

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as Audit;

                if (entity != null)
                {
                    var date = DateTime.UtcNow;

                    if (string.IsNullOrEmpty(userId))
                    {
                        userId = "SYSTEM";
                    }

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDelete && ((ISoftDelete)entity).IsDeleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }

                    context.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                    entity.UpdatedAt = date;
                    entity.UpdatedBy = userId;
                }
            }
        }
    }
}
