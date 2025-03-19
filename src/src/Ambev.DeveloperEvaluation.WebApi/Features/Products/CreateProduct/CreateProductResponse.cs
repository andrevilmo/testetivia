using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// API response model for CreateProduct operation
/// </summary>
public class CreateProductResponse
{
        /// <summary>
        /// Gets the Product's identifier.
        /// Must not be null or empty and should contain both first and last names.
        /// </summary>
        public string Id { get; set; } 

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
