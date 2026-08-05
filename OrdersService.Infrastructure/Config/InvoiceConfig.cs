using OrdersService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvoiceConfig : IEntityTypeConfiguration<Invoices>
{
    public void Configure(EntityTypeBuilder<Invoices> builder)
    {
        builder.HasKey(u => u.InvoiceId);

        builder.Property(u => u.InvoiceId)
            .ValueGeneratedOnAdd();
    }
}

