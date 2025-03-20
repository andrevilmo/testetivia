using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Cart entity operations
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// Creates a new Cart in the repository
    /// </summary>
    /// <param name="Cart">The Cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Cart</returns>
    Task<Cart> CreateAsync(Cart Cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates Cart in the repository
    /// </summary>
    /// <param name="Cart">The Cart to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Cart</returns>
    Task<Cart> UpdateAsync(Cart Cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Cart by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart if found, null otherwise</returns>
    List<Cart>? GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Cart list 
    /// </summary>
    /// <param name="pOrder">The order to retrieve Cart</param>
    /// <param name="pFilter">The filter to retrieve Cart</param>
    /// <param name="pPage">The page to retrieve list</param>
    /// <param name="pSize">The size to retrieve Carts</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart list if found, null otherwise</returns>
    Task<List<Cart>?> GetByIFilterAsync(  String pOrder, 
                                                    Dictionary<string,string> pFilter, 
                                                    int pPage, 
                                                    int pSize, 
                                                    CancellationToken cancellationToken );

    /// <summary>
    /// Deletes a Cart from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Cart was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
