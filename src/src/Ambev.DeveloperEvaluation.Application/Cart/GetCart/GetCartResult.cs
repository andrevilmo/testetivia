using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Response model for GetCart operation
/// </summary>
public class GetCartResult
{
  /// <summary>
  /// Gets or sets the unique identifier of the newly created Cart.
  /// </summary>
  /// <value>A GUID that uniquely identifies the created Cart in the system.</value>
  public Guid Id { get; set; }

  /// <summary>
  /// Gets the Cart's user id.
  /// </summary>
  public Guid UserId { get; set; } = Guid.Empty;

  /// <summary>
  /// Gets the Cart's date.
  ///
  /// </summary>
  public DateTime Date { get; set; } = DateTime.Now;


  public ICollection<CartItem> Products;


  public ICollection<GetCartResult> Data;

}
