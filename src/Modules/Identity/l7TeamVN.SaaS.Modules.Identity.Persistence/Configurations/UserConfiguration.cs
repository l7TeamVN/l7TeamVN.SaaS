using l7TeamVN.SaaS.Modules.Identity.Domain.Entities.Aggregates;
using l7TeamVN.SaaS.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace l7TeamVN.SaaS.Modules.Identity.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasConversion(
                v => v.ToString(),
                v => EmailAddress.Create(v).Value)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.AvatarUrl)
            .HasConversion(
                v => v == null ? null : v.ToString(),
                v => v == null ? null : WebsiteUrl.Create(v).Value);

        builder.Property(u => u.PhoneNumber)
            .HasConversion(
                v => v == null ? null : v.ToString(),
                v => v == null ? null : PhoneNumber.Create(v).Value);

    }
}
