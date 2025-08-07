using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Map;

public class PaymentMap : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(p => p.CustomerId)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(p => p.PaidAt)
            .HasColumnType("timestamp")
            .IsRequired(false);
    }
}