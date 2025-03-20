using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Command for retrieving a Cart by their ID or by listing
/// </summary>
public record GetCartCommand : IRequest<GetCartResult>
{
    /// <summary>
    /// The unique identifier of the Cart to retrieve
    /// </summary>
    public Guid Id { get;  set; }


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

    /// <summary>
    /// Initializes a new instance of GetCartCommand
    /// </summary>
    /// <param name="pOrder">The order of the Cart to retrieve</param>
    /// <param name="pFilter">The filter of the Cart to retrieve</param>
    /// <param name="pPage">The page of the Cart to retrieve</param>
    /// <param name="pSize">The size of the Cart to retrieve</param>
    public GetCartCommand(String pOrder = "", Dictionary<string,string> pFilter = null, int pPage = 1, int pSize = 10)   
    {
        Order = pOrder;
        Filter = pFilter;
        Page = pPage;
        Size = pSize;
    }

      /// <summary>
    /// Initializes a new instance of GetCartCommand
    /// </summary>
    /// <param name="id">The ID of the Cart to retrieve</param> 
    public GetCartCommand(Guid id)   
    {
        Id = id;
        Order = "";
        Filter = null;
        Page = 1;
        Size = 10;
    }
}
