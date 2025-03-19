using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Common.Model;
namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a Product in the system with authentication and profile information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity, IProduct
{
    /// <summary>
    /// Gets the Product's title name.
    /// Must not be null or empty and should contain both first and last names.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets the Product's description  .
    ///
    /// </summary>
    public string Description { get; set; } = string.Empty;


    /// <summary>
    /// Gets the Product's category  .
    ///
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets the Product's image  .
    ///
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets the products price .
    ///
    /// </summary>
    public decimal Price { get; set; } = 0;

    /// <summary>
    /// Gets the products rate .
    ///
    /// </summary>
    public IProductRate Rating { get; set; } = new ProductRate();



    /// <summary>
    /// Gets the date and time when the Product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the Product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the unique identifier of the Product.
    /// </summary>
    /// <returns>The Product's ID as a string.</returns>
    string IProduct.Id => Id.ToString();

    /// <summary>
    /// Gets the products rate id.
    ///
    /// </summary>
    public string RatingId { get;  } 


    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the Product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title format and length</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}