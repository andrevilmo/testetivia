namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Request model for getting a Cart by ID
/// </summary>
public class GetCartRequest
{
    /// <summary>
    /// The unique identifier of the Cart to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the Cart to retrieve
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
