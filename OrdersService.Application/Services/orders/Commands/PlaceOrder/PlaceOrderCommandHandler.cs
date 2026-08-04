using OrdersService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Services.orders.Commands.PlaceOrder
{


    public class PlaceOrderCommandHandler
        : IRequestHandler<PlaceOrderCommand, Orders>
    {
        
        private readonly IApplicationDbContext _context;

        public PlaceOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Orders> Handle(
            PlaceOrderCommand request,
            CancellationToken cancellationToken)
        {
        //    // Logic goes here
        //    var client = await _context.Clients
        //.FirstOrDefaultAsync(c => c.Id == request.Id && c.IsActive);
        //    if (client == null)
        //    {
        //        throw new KeyNotFoundException("Invalid client.");
        //    }
            

            var order = new Orders
            {
                Id = request.Id,
                OrderType = request.OrderType,
                LimitPrice = request.LimitPrice,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity
                
            };
            //if (client.AccountBalance < order.GrossAmount)
            //{
            //    throw new InvalidOperationException("Insufficient balance.");
         
            //}
            //client.AccountBalance -= order.GrossAmount;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(CancellationToken.None);
            return order;
        }
    }
}
