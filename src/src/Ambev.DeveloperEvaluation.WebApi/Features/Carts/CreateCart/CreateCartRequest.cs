using Ambev.DeveloperEvaluation.Common.Model;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents a request to create a new Cart in the system.
/// </summary>
public class CreateCartRequest
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
    public IEnumerable<CreateCartItemRequest> Products { get; set; } 




}