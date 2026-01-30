using FluentValidation;
using Order.DTOs;
using Order.Models;

namespace Order.Validators;

public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>
{
    public UpdateOrderStatusRequestValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid order status")
            .NotEqual(OrderStatus.Pending).WithMessage("Cannot manually set status to Pending");
    }
}
