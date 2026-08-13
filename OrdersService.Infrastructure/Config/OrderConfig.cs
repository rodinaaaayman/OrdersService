using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Models;
using System.Reflection.Emit;

public class OrderConfig: IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        builder.HasKey(u => u.OrderId);

        builder.Property(u => u.OrderId)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.UnitPrice)
            .IsRequired();

        builder.Property(u => u.Quantity)
            .IsRequired();

        builder.HasOne(o => o.Invoice)
            .WithOne(i => i.Order)
            .HasForeignKey<Invoices>(i => i.OrderId);

        builder.HasMany(o => o.Executions)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId);

        builder.Property(o => o.CommissionRate)
        .HasColumnType("decimal(5,4)");
    }
}

