using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// API response model for GetProduct operation
/// </summary>
public class GetProductResponse
{
        /// <summary>
        /// Gets or sets the unique identifier of the newly created Product.
        /// </summary>
        /// <value>A GUID that uniquely identifies the created Product in the system.</value>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets the Product's title name.
        /// Must not be null or empty and should contain both first and last names.
        /// </summary>
        public string Title { get; set; } 

        /// <summary>
        /// Gets the Product's description  .
        ///
        /// </summary>
        public string Description { get; set; }  


        /// <summary>
        /// Gets the Product's category  .
        ///
        /// </summary>
        public string Category { get; set; }  
        
        /// <summary>
        /// Gets the Product's image  .
        ///
        /// </summary>
        public string Image { get; set; }  

        /// <summary>
        /// Gets the products price .
        ///
        /// </summary>
        public decimal Price { get; set; } 

        /// <summary>
        /// Gets the products rate .
        ///
        /// </summary>
        public CreateProductRateRequest Rating { get; set; }  
 
}
