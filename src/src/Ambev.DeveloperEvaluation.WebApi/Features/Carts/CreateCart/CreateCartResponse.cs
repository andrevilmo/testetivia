using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// API response model for CreateCart operation
/// </summary>
public class CreateCartResponse
{
        /// <summary>
        /// Gets the Cart's Id.
        /// Unique product identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the Cart's user id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets the Cart's date.
        ///
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the date and time when the Cart was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the Cart's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        
        /// <summary>
        /// Gets the Cart's items
        ///
        /// </summary> 
        public IEnumerable<CreateCartItemRequest> Products { get; set; } 
}
