using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Command for creating a new Cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a Cart, 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateCartResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateCartCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateCartCommand : IRequest<CreateCartResult>
{


    /// <summary>
    /// Gets the Cart's user id.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets the Cart's date.
    ///
    /// </summary>
    public DateTime Date { get; set; } = DateTime.Now;


    /// <summary>
    /// Gets the Cart's items
    ///
    /// </summary> 
    public IEnumerable<CreateCartItemCommand> Products { get; set; } = [];

    public ValidationResultDetail Validate()
    {
        var validator = new CreateCartCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}