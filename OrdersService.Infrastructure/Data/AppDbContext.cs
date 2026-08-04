using OrdersService.Application.Interfaces;
using OrdersService.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace OrdersService.Infrastructure.Data
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Executions> Executions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Invoice)
                .WithOne(i => i.Order)
                .HasForeignKey<Invoice>(i => i.OrderId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.OrderId)
                .IsUnique();

            //modelBuilder.Entity<Orders>()
            //    .Property(o => o.NetAmount)
            //    .HasComputedColumnSql("[Quantity] * [UnitPrice]", true);


            //modelBuilder.Entity<Orders>()
            //    .Property(o => o.Commission)
            //    .HasComputedColumnSql("([Quantity] * [UnitPrice]) * [CommissionRate] / 100", true);


            //modelBuilder.Entity<Orders>()
            //    .Property(o => o.GrossAmount)
            //    .HasComputedColumnSql("([Quantity] * [UnitPrice]) + (([Quantity] * [UnitPrice]) * [CommissionRate] / 100)", true);



            base.OnModelCreating(modelBuilder);
        }
    }
}
