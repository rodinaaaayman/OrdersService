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
        public DbSet<Invoices> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
