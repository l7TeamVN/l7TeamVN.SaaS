using l7TeamVN.SaaS.Domain.Entities;
using l7TeamVN.SaaS.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace l7TeamVN.SaaS.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private static void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        var utcNow = DateTimeOffset.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is ITrackable auditable)
            {
                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedAt = utcNow;
                    auditable.CreatedBy ??= SystemsConstants.SystemUser;
                }

                if (entry.State == EntityState.Modified)
                {
                    auditable.LastModifiedAt = utcNow;
                    auditable.LastModifiedBy ??= SystemsConstants.SystemUser;
                }
            }

            if (entry.Entity is ISoftDeletable softDelete && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                softDelete.IsDeleted = true;
                softDelete.DeletedAt = utcNow;
                softDelete.DeletedBy ??= SystemsConstants.SystemUser;
            }
        }
    }
}