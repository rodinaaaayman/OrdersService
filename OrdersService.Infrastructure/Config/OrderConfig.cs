using OrdersService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.Property(o => o.NetAmount)
                .HasComputedColumnSql("[Quantity] * [UnitPrice]", stored: true);

        builder.Property(o => o.Commission)
            .HasComputedColumnSql("([Quantity] * [UnitPrice]) * [CommissionRate] / 100", stored: true);

        builder.Property(o => o.GrossAmount)
            .HasComputedColumnSql("([Quantity] * [UnitPrice]) + (([Quantity] * [UnitPrice]) * [CommissionRate] / 100)", stored: true);

        builder.HasOne(o => o.Invoice)
            .WithOne(i => i.Order)
            .HasForeignKey<Invoices>(i => i.OrderId);

        builder.HasMany(o => o.Executions)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId);
    }
}

