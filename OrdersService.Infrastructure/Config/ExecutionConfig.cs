using OrdersService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExecutionConfig : IEntityTypeConfiguration<Executions>
{
    public void Configure(EntityTypeBuilder<Executions> builder)
    {
        builder.HasKey(u => u.ExecutionId);

        builder.Property(u => u.ExecutionId)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.ExecutionQuantity)
            .IsRequired();

        
    }
}

