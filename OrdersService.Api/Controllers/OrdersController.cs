using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.DTOs;
using OrdersService.Application.Services.orders.Commands.CancelOrder;
using OrdersService.Application.Services.orders.Commands.PlaceOrder;
using OrdersService.Application.Services.orders.Queries.GetClientOrders;
using OrdersService.Application.Services.orders.Queries.GetOrders;
using OrdersService.Domain.Models;


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

            var orderId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrders),
                new { id = orderId },
                new { id = orderId });
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
        // GET: api/Orders/client/5
        [HttpGet("client/{Id}/orders")]
        public async Task<IActionResult> GetClientOrders(int Id)
        {
            var result = await _mediator.Send(new GetClientOrdersQuery(Id));
            return Ok(result);
        }
    }
}
