using FluentValidation;
using Cart.DTOs;

namespace Cart.Validators;

public class UpdateBasketRequestValidator : AbstractValidator<UpdateBasketRequest>
{
    public UpdateBasketRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name is required")
            .MaximumLength(100).WithMessage("User name must not exceed 100 characters");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Items list cannot be null");

        RuleForEach(x => x.Items).SetValidator(new ShoppingCartItemDtoValidator());
    }
}

public class ShoppingCartItemDtoValidator : AbstractValidator<ShoppingCartItemDto>
{
    public ShoppingCartItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0")
            .LessThanOrEqualTo(1000).WithMessage("Quantity must not exceed 1000");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("Color is required")
            .MaximumLength(50).WithMessage("Color must not exceed 50 characters");
    }
}
