namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Request model for getting a Product by ID
/// </summary>
public class GetProductRequest
{
    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public string Order { get;  set; }

    /// <summary>
    /// The Filter to retrieve
    /// </summary>
    public Dictionary<string,string> Filter { get;  set; }


    /// <summary>
    /// The page of pagination
    /// </summary>
    public int Page { get; set;  }

    /// <summary>
    /// The Size of pagination
    /// </summary>
    public int Size { get; set; }
}
