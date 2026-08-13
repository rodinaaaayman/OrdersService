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

        [HttpGet]
        public async Task<IActionResult> GetOrders(
            [FromQuery] int? cursor,
            [FromQuery] int limit = 20)
        {
            return Ok(await _mediator.Send(new GetOrdersQuery(cursor, limit)));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Orders>> GetOrders(int Id)
        {
            var order = await _mediator.Send(
                new GetOrderByIdQuery(Id));

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder(CreateOrdersDTO dto)
        {
            var command = new PlaceOrderCommand
            {
                Id = dto.Id,
                OrderType = dto.OrderType,
                LimitPrice = dto.LimitPrice,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity,
                CommissionRate=dto.CommissionRate
            };

            var orderId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrders),
                new { Id = orderId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var result = await _mediator.Send(new CancelOrderCommand { OrderId = id });

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }
        // GET "orders by client Id"
        [HttpGet("client/{Id}/orders")]
        public async Task<IActionResult> GetClientOrders(int Id)
        {
            var result = await _mediator.Send(new GetClientOrdersQuery(Id));
            return Ok(result);
        }
    }
}
