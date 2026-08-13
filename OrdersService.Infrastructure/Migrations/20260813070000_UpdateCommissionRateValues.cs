using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommissionRateValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Orders] SET [CommissionRate] = 0.005 WHERE [CommissionRate] = 0.01");
            migrationBuilder.Sql("UPDATE [Invoices] SET [CommissionRate] = 0.005 WHERE [CommissionRate] = 0.01");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Orders] SET [CommissionRate] = 0.01 WHERE [CommissionRate] = 0.005");
            migrationBuilder.Sql("UPDATE [Invoices] SET [CommissionRate] = 0.01 WHERE [CommissionRate] = 0.005");
        }
    }
}
