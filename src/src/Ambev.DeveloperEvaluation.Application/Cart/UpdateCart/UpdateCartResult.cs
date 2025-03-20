namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Represents the response returned after successfully creating a new Cart.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Cart,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Cart.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created Cart in the system.</value>
    public Guid Id { get; set; }

    
    /// <summary>
    /// Gets or sets the Title of the Cart to be created.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description for the Cart.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image for the Cart.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price for the Cart.
    /// </summary>
    public decimal Price { get; set; } = 0;

 
}
