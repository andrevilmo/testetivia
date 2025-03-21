using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartRequest that defines validation rules for Cart creation.
/// </summary>
public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, length between 3 and 50 characters
    /// - Description: Required, length between 3 and 50 characters
    /// </remarks>
    public UpdateCartRequestValidator()
    { 
            RuleFor(x => 
                x.Products
                .Select(x=>new {ProductId=x.ProductId, Qtd=x.Quantity})
                .GroupBy(y=>y.ProductId)
                .Select(i=>new {Id=i.Key,Qtd=i.Sum(g=>g.Qtd)})
                .Where(f=>f.Qtd>20).ToList()
            ).Must(list => 
                list.Count() < 1
            )
            .WithMessage("Products total count cannot more than 20 ");
            RuleFor(x => x.Products)
            .NotEmpty()
            .WithMessage("Cart ID is required");
    }
}