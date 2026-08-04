using OrdersService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace OrdersService.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Orders> Orders { get; }
        DbSet <Executions> Executions { get; }
        DbSet<Invoice> Invoices { get; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
