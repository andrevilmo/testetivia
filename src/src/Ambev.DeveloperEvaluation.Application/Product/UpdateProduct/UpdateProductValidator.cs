using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for Product creation command.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include: 
    /// - Description: Required, must be between 3 and 50 characters 
    /// - Title: Required, must be between 3 and 50 characters 
    /// </remarks>
    public UpdateProductCommandValidator()
    {
        RuleFor(Product => Product.Description).NotEmpty().Length(3, 50);
        RuleFor(Product => Product.Title).NotEmpty().Length(3, 50); 
 
    }
}