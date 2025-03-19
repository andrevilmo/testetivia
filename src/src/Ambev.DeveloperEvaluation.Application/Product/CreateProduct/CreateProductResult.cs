namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Represents the response returned after successfully creating a new Product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Product,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Product.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created Product in the system.</value>
    public Guid Id { get; set; }

    
    /// <summary>
    /// Gets or sets the Title of the Product to be created.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description for the Product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image for the Product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price for the Product.
    /// </summary>
    public decimal Price { get; set; } = 0;


     /// <summary>
    /// Gets or sets the Rate for the Product.
    /// </summary>
    public CreateProductRateResult Rating { get; set; } = new CreateProductRateResult();
}
