using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Validator for CreateCartRequest that defines validation rules for Cart creation.
/// </summary>
public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, length between 3 and 50 characters
    /// - Description: Required, length between 3 and 50 characters
    /// </remarks>
    public CreateCartRequestValidator()
    {
        RuleFor(x => 
                x.Products
                .Select(x=>new {ProductId=x.ProductId, Qtd=x.Quantity})
                .GroupBy(y=>y.ProductId)
                .Select(i=>new {Id=i.Key,Qtd=i.Sum(g=>g.Qtd)})
                .Where(f=>f.Qtd>20)
            ).Must(list => list.Count() > 0)
            .WithMessage("Products total count cannot more than 20 ");
        RuleFor(x => x.Products)
            .NotEmpty()
            .WithMessage("Cart ID is required");
    }
}