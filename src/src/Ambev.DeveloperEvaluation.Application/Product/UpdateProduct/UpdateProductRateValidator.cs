using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for Product creation command.
/// </summary>
public class UpdateProductRateCommandValidator : AbstractValidator<UpdateProductRateCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include: 
    /// - Rate: Required, must be between 3 and 50 characters 
    /// </remarks>
    public UpdateProductRateCommandValidator()
    { 
        RuleFor(ProductRate => ProductRate.Rate).NotEmpty().Length(3, 50); 
 
    }
}