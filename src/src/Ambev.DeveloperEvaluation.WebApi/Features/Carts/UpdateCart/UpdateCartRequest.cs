using Ambev.DeveloperEvaluation.Common.Model;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Represents a request to create a new Cart in the system.
/// </summary>
public class UpdateCartRequest
{

   /// <summary>
    /// Gets the Cart's user id.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;
    
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
    public IEnumerable<UpdateCartItemRequest> Products { get; set; } 



}