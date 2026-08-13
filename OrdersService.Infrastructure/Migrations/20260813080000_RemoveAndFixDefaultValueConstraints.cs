using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAndFixDefaultValueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing default constraint on Orders.CommissionRate if it exists
            migrationBuilder.Sql("ALTER TABLE [Orders] DROP CONSTRAINT IF EXISTS [DF_Orders_CommissionRate]");

            // Drop the existing default constraint on Invoices.CommissionRate if it exists
            migrationBuilder.Sql("ALTER TABLE [Invoices] DROP CONSTRAINT IF EXISTS [DF_Invoices_CommissionRate]");

            // Recreate them with correct value (no default - let application control it)
            // Note: Removed default constraints since CommissionRate should be set explicitly
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate defaults if rolling back
            migrationBuilder.Sql("ALTER TABLE [Orders] ADD CONSTRAINT [DF_Orders_CommissionRate] DEFAULT 0.005 FOR [CommissionRate]");
            migrationBuilder.Sql("ALTER TABLE [Invoices] ADD CONSTRAINT [DF_Invoices_CommissionRate] DEFAULT 0.005 FOR [CommissionRate]");
        }
    }
}
