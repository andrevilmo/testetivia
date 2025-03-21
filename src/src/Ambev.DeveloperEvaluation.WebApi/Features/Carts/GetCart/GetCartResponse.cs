using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// API response model for GetCart operation
/// </summary>
public class GetCartResponse
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
        /// Gets the cart discount .
        ///
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the Cart's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        public ICollection<CartItem> Products { get; set; }
 
 
}
