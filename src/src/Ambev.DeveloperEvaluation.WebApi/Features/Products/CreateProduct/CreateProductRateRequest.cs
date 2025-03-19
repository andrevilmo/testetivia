using Ambev.DeveloperEvaluation.Common.Model;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents a request to create a new Product in the system.
/// </summary>
public class CreateProductRateRequest
{
       /// <summary>
        /// Gets the Product's title name.
        /// Must not be null or empty and should contain both first and last names.
        /// </summary>
        public string Rate { get; set; } 

        /// <summary>
        /// Gets the Product's description  .
        ///
        /// </summary>
        public int Count { get; set; }  

 
}