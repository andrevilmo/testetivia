using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class GetProductResult
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
