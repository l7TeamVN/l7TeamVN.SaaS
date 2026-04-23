namespace l7TeamVN.SaaS.Domain.Entities;

public interface ITrackable
{
    DateTimeOffset CreatedAt { get; set; }
    string? CreatedBy { get; set; }

    DateTimeOffset? LastModifiedAt { get; set; }
    string? LastModifiedBy { get; set; }
}
