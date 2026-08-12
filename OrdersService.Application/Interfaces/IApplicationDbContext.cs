using OrdersService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OrdersService.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Orders> Orders { get; }
        DbSet <Executions> Executions { get; }
        DbSet<Invoices> Invoices { get; }
        DbSet<OutboxMessage> OutboxMessages { get; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
