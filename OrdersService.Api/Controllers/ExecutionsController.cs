using OrdersService.Infrastructure.Data;
using OrdersService.Application.DTOs;
using OrdersService.Application.Events;
using OrdersService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Enums;

namespace OrdersService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public ExecutionsController(
            AppDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostExecution(CreateExecutionDTO dto)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == dto.OrderId);

            if (order == null)
                return NotFound("Order not found.");

            if (order.FilledQuantity + dto.ExecutionQuantity > order.Quantity)
            {
                return UnprocessableEntity(
                    "Execution exceeds remaining quantity."
                );
            }
            var execution = new Executions
            {
                OrderId = dto.OrderId,
                ExecutionQuantity = dto.ExecutionQuantity,
                ExecutionDate = DateTime.UtcNow
            };

            _context.Executions.Add(execution);
            order.FilledQuantity += dto.ExecutionQuantity;
            
                if (order.FilledQuantity == order.Quantity)
                {
                    order.Status = OrderStatus.Filled;

                    await _mediator.Publish(
                        new OrderFullyFilledEvent(order.OrderId)
                    );
                }
            else
            {
                order.Status = OrderStatus.PartiallyFilled;
            }
            await _context.SaveChangesAsync();
            return Ok(new
            {
                execution.ExecutionId,
                execution.OrderId,
                execution.ExecutionQuantity,
                execution.ExecutionDate,
                OrderStatus = order.Status
            });
        }
    }
}
