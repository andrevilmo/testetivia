using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new Product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a Product,  
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateProductRateCommand : IRequest<CreateProductRateResult>
{
    /// <summary>
    /// Gets or sets the Rate of the Product rate to be created.
    /// </summary>
    public string Rate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the count for the Product rate.
    /// </summary>
    public int Count { get; set; } = 0;
 
 

    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductRateCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}