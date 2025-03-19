using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Common.Model;
namespace Ambev.DeveloperEvaluation.Domain.Entities;
/*
    "id": "integer",
    "title": "string",
    "price": "number",
    "description": "string",
    "category": "string",
    "image": "string",
    "rating": {
      "rate": "number",
      "count": "integer"
    }
*/

/// <summary>
/// Represents a Product in the system with authentication and profile information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class ProductRate : BaseEntity, IProductRate
{
    /// <summary>
    /// Gets the Product's rate name. 
    /// </summary> 
    public string Rate { get; set; } = string.Empty;

    /// <summary>
    /// Gets the Product's rating count  .
    ///
    /// </summary>
    public int Count { get; set; } = 0;
   

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
    string IProductRate.Id => Id.ToString();


    /// <summary>
    /// Gets the unique identifier of the Product.
    /// </summary>
    /// <returns>The Product's ID as a string.</returns>
    public Guid ProductId  { get; set; }

    /// <summary>
    /// Gets the unique Product.
    /// </summary>
    /// <returns>The Product.</returns>
    public IProduct Product  { get; set; }

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public ProductRate()
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
        var validator = new ProductRateValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}