using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductRateValidator : AbstractValidator<ProductRate>
{
    public ProductRateValidator()
    { 
        RuleFor(ProductRate => ProductRate.Rate)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Rate must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Rate cannot be longer than 50 characters.");         
    }
}
