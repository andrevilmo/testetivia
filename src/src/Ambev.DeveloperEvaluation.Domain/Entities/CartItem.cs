using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Common.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a Cart in the system with authentication and profile information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class CartItem : BaseEntity, ICartItem
{
    /// <summary>
    /// Gets the Cart's item product
    /// </summary>
    public Product Product { get; set; } = null;

    /// <summary>
    /// Gets the Cart's item product id
    /// </summary>
    public Guid ProductId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets the Cart's item quantity.
    ///
    /// </summary>
    public int Quantity { get; set; } = 0;
 

    /// <summary>
    /// Gets the Cart's id
    /// </summary>
    public Guid CartId { get; set; } = Guid.Empty;



    /// <summary>
    /// Gets the Cart's id
    /// </summary>
    public Cart Cart { get; set; } = new Cart();

    /// <summary>
    /// Gets the date and time when the Cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the Cart's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the unique identifier of the Cart.
    /// </summary>
    /// <returns>The Cart's ID as a string.</returns>
    string ICartItem.Id => Id.ToString();

    IProduct ICartItem.Product { get => Product; set { this.Product = (Product) value; } }

    ICart ICartItem.Cart { get => Cart; set  {this.Cart = (Cart) value; } }




    /// <summary>
    /// Initializes a new instance of the Cart class.
    /// </summary>
    public CartItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the Cart entity using the CartValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title format and length</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new CartItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}