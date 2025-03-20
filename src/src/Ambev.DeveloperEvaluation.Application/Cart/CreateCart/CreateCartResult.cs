namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Represents the response returned after successfully creating a new Cart.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Cart,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateCartResult
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


    public ICollection<CreateCartItemCommand> Products;

 
}
