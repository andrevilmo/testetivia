using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new Product in the repository
    /// </summary>
    /// <param name="Product">The Product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product</returns>
    Task<Product> CreateAsync(Product Product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates Product in the repository
    /// </summary>
    /// <param name="Product">The Product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Product</returns>
    Task<Product> UpdateAsync(Product Product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Product list 
    /// </summary>
    /// <param name="pOrder">The order to retrieve product</param>
    /// <param name="pFilter">The filter to retrieve product</param>
    /// <param name="pPage">The page to retrieve list</param>
    /// <param name="pSize">The size to retrieve products</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product list if found, null otherwise</returns>
    Task<List<Product>?> GetByIFilterAsync(  String pOrder, 
                                                    Dictionary<string,string> pFilter, 
                                                    int pPage, 
                                                    int pSize, 
                                                    CancellationToken cancellationToken );

    /// <summary>
    /// Deletes a Product from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Product was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
