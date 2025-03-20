using Ambev.DeveloperEvaluation.Common.Model;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents a request to create a new Product in the system.
/// </summary>
public class CreateCartItemRequest
{

    /// <summary>
    /// Gets the Cart's item product id
    /// </summary>
    public Guid ProductId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets the Cart's item quantity.
    ///
    /// </summary>
    public int Quantity { get; set; } = 0;

 
}