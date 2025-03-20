using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// API response model for UpdateCart operation
/// </summary>
public class UpdateCartResponse
{
       /// <summary>
        /// Gets the Cart's Id.
        /// Unique product identifier
        /// </summary>
        public string Id { get;  } 

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
  

 
}
