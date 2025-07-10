using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.Status)
            .HasColumnName("user_status")
            .HasConversion<string>()
            .HasColumnType("character varying(20)")
            .IsRequired();

        builder.Property(u => u.Role)
            .HasColumnName("user_role")
            .HasConversion<string>()
            .HasColumnType("character varying(20)")
            .IsRequired();

        builder.OwnsOne(u => u.Name, e =>
        {
            e.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .HasColumnType($"character varying({Name.MaxFirstNameLength})")
                .HasMaxLength(Name.MaxFirstNameLength)
                .IsRequired();

            e.Property(n => n.LastName)
                .HasColumnName("last_name")
                .HasColumnType($"character varying({Name.MaxLastNameLength})")
                .HasMaxLength(Name.MaxLastNameLength)
                .IsRequired();
        });

        builder.OwnsOne(u => u.Username, e =>
        {
            e.Property(u => u.Value)
                .HasColumnName("username")
                .HasColumnType($"character varying({Username.MaxLength})")
                .HasMaxLength(Username.MaxLength)
                .IsRequired();

            e.HasIndex(u => u.Value).IsUnique();
        });

        builder.OwnsOne(u => u.Password, e =>
        {
            e.Property(p => p.Value)
                .HasColumnName("password")
                .HasColumnType($"character varying({Password.MaxLength})")
                .HasMaxLength(Password.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(u => u.Email, e =>
        {
            e.Property(email => email.Address)
                .HasColumnName("email")
                .HasColumnType($"character varying({Email.MaxEmailLength})")
                .HasMaxLength(Email.MaxEmailLength)
                .IsRequired();

            e.HasIndex(email => email.Address).IsUnique();
        });

        builder.OwnsOne(u => u.Phone, e =>
        {
            e.Property(p => p.Idd)
                .HasColumnName("idd")
                .HasColumnType($"character varying({Phone.MaxIddLength})")
                .HasMaxLength(Phone.MaxIddLength);

            e.Property(p => p.Number)
                .HasColumnName("phone_number")
                .HasColumnType($"character varying({Phone.MaxNumberLength})")
                .HasMaxLength(Phone.MaxNumberLength);

            e.HasIndex(p => p.Number).IsUnique();
        });

        builder.ConfigureAuditableEntity();
    }
}