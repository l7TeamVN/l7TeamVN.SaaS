namespace l7TeamVN.SaaS.Domain.Entities;

public abstract class AuditableEntity<T> : Entity<T>, ITrackable, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; }

    public DateTimeOffset? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}