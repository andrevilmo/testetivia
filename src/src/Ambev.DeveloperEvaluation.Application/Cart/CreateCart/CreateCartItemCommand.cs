using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

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
public class CreateCartItemCommand : IRequest<CreateCartResult>
{
        /// <summary>
        /// Gets the Cart's item Id.
        /// Unique product identifier
        /// </summary>
        public string Id { get;  } 

        /// <summary>
        /// Gets the Cart's item product
        /// </summary>
        public GetProductCommand Product { get; set; } 

        /// <summary>
        /// Gets the Cart's item product id
        /// </summary>
        public Guid ProductId { get; set; } 

        /// <summary>
        /// Gets the Cart's item quantity.
        ///
        /// </summary>
        public int Quantity { get; set; } 

        /// <summary>
        /// Gets the date and time when the Cart was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the Cart's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }


        
        /// <summary>
        /// Gets the Cart
        /// </summary>
        public CreateCartCommand Cart { get; set; } 


         
        /// <summary>
        /// Gets the Cart's id
        /// </summary>
        public Guid CartId { get; set; } 
 
 

    public ValidationResultDetail Validate()
    {
        var validator = new CreateCartItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}