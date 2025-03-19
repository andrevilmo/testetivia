using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    { 
        RuleFor(Product => Product.Description)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Description cannot be longer than 50 characters.");         
    }
}
