using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Validator for CreateCartCommand that defines validation rules for Cart creation command.
/// </summary>
public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include: 
    /// - Description: Required, must be between 3 and 50 characters 
    /// - Title: Required, must be between 3 and 50 characters 
    /// </remarks>
    public CreateCartCommandValidator()
    {
        
 
    }
}