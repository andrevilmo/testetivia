namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully creating a new Product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Product,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateProductRateResult
{
    /// <summary>
    /// Obtém a avaliação do produto
    /// </summary>
    /// <returns>A avaliação do produto.</returns>
    public string Rate { get; set; }

    /// <summary>
    /// Obtém a avaliação do produto em quantidade
    /// </summary>
    /// <returns>A avaliação do produto em quantidade.</returns>
    public int Count { get; set;}
}
