using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Map;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        
        builder.OwnsOne(u => u.FirstName, fn =>
        {
            fn.Property(f => f.Name)
                .HasColumnName("FirstName")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(u => u.LastName, ln =>
        {
            ln.Property(l => l.Name)
                .HasColumnName("LastName")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(u => u.Email, em =>
        {
            em.Property(e => e.Address)
                .HasColumnName("Email")
                .HasMaxLength(256)
                .IsRequired();
        });

        builder.OwnsOne(u => u.PasswordHash, ph =>
        {
            ph.Property(p => p.Value)
                .HasColumnName("PasswordHash")
                .IsRequired();
        });
        
        builder.OwnsMany(u => u.Roles, roles =>
        {
            roles.WithOwner().HasForeignKey("UserId");

            roles.Property<int>("Id");
            roles.HasKey("Id");

            roles.Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();

            roles.Property(r => r.ValidUntil);

            roles.ToTable("UserRoles");
        });
    }
}