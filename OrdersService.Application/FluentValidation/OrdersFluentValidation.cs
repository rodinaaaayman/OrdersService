using OrdersService.Application.DTOs;
using FluentValidation;
using OrdersService.Domain.Enums;

namespace OrdersService.Application.FluentValidation
{
    public class OrdersFluentValidation
    {
        public class CreateOrderDTOValidator : AbstractValidator<CreateOrdersDTO>
        {
            public CreateOrderDTOValidator()
            {
                RuleFor(x => x.LimitPrice)
                    .NotNull()
                    .GreaterThan(0)
                    .When(x => x.OrderType == OrderTypes.Limit)
                    .WithMessage("Limit Price is required and must be greater than 0 for Limit orders.");
                
            }
        }
    }
}
