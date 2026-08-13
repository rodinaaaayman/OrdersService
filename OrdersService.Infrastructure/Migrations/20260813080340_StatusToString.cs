using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add a temporary string column
            migrationBuilder.AddColumn<string>(
                name: "Status_temp",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            // 2. Convert existing int values into their enum name strings
            migrationBuilder.Sql(@"
                UPDATE Orders SET Status_temp =
                    CASE Status
                        WHEN 0 THEN 'Pending'
                        WHEN 1 THEN 'PartiallyFilled'
                        WHEN 2 THEN 'Filled'
                        WHEN 3 THEN 'Cancelled'
                    END");

            // 3. Drop the old int column
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            // 4. Rename temp column to Status
            migrationBuilder.RenameColumn(
                name: "Status_temp",
                table: "Orders",
                newName: "Status");

            // 5. Make it non-nullable now that every row is populated
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            // Keep CommissionRate default intact
            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionRate",
                table: "Orders",
                type: "decimal(5,4)",
                nullable: false,
                defaultValue: 0.005m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)",
                oldDefaultValue: 0.005m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status_temp",
                table: "Orders",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE Orders SET Status_temp =
                    CASE Status
                        WHEN 'Pending' THEN 0
                        WHEN 'PartiallyFilled' THEN 1
                        WHEN 'Filled' THEN 2
                        WHEN 'Cancelled' THEN 3
                    END");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Status_temp",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionRate",
                table: "Orders",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)",
                oldDefaultValue: 0.005m);
        }
    }
}