using OrdersService.Application.Services.orders.Commands.PlaceOrder;
using OrdersService.Application.Services.orders.Commands.CancelOrder;
using OrdersService.Application.Services.orders.Queries.GetOrders;
using OrdersService.Application.DTOs;
using OrdersService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders(
    [FromQuery] int? cursor,
    [FromQuery] int limit = 20)
        {
            return Ok(await _mediator.Send(new GetOrdersQuery(cursor, limit)));
        }
        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            var order = await _mediator.Send(
                new GetOrderByIdQuery(id));

            return Ok(order);
        }

        //POST: api/Orders
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(CreateOrdersDTO dto)
        {
            var command = new PlaceOrderCommand
            {
                Id = dto.Id,
                OrderType = dto.OrderType,
                LimitPrice = dto.LimitPrice,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity
            };

            var order = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrders),
                new { id = order.OrderId },
                order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var result = await _mediator.Send(
                new CancelOrderCommand(id));


            if (!result)
            {
                return NotFound();
            }


            return NoContent();
        }
    }
}
