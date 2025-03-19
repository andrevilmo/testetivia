using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for Product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, length between 3 and 50 characters
    /// - Description: Required, length between 3 and 50 characters
    /// </remarks>
    public CreateProductRequestValidator()
    {
        RuleFor(Product => Product.Description).NotEmpty().Length(3, 50);
        RuleFor(Product => Product.Title).NotEmpty().Length(3, 50);
   }
}