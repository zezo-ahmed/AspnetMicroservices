using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.ChechoutOrder
{
    public class ChechoutOrderCommandValidator : AbstractValidator<ChechoutOrderCommand>
    {
        public ChechoutOrderCommandValidator()
        {
            RuleFor(o => o.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(o => o.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(o => o.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
        }
    }
}
