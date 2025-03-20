using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing Product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new Product
    /// </summary>
    /// <param name="request">The Product creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(response)
        });
    }


     /// <summary>
    /// Updates a Product
    /// </summary>
    /// <param name="request">The Product to update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Product details</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        try {
            var command = _mapper.Map<UpdateProductCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok (new ApiResponseWithData<UpdateProductResponse>
                {
                    Success = true,
                    Message = "Product updated successfully",
                    Data = _mapper.Map<UpdateProductResponse>(response)
                });
        } catch (Exception exp) {
                return BadRequest(exp.Message + " => " + exp.StackTrace.ToString());
        }
    
    }

    /// <summary>
    /// Retrieves a Product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var command = _mapper.Map<GetProductCommand>(request.Id);
        try {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponseWithData<GetProductResponse>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = _mapper.Map<GetProductResponse>(response)
            });
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message);
        }
    }

     /// <summary>
    /// Retrieves a Product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product details if found</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductList([FromQuery] int _page, 
                                                    [FromQuery] int _size, 
                                                    [FromQuery] string _order, 
                                                    CancellationToken cancellationToken)
    {   
        var request = new GetProductRequest { 
                    Id = Guid.Empty, 
                    Order = "", 
                    Filter = null, 
                    Page = 1, 
                    Size = 10
                };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var p = 
            this.Request.Query.Select(x => new KeyValuePair<string,string> (x.Key, x.Value))
                .ToDictionary<string,string>();
        var command = _mapper.Map<GetProductCommand>(new GetProductCommand(pOrder: _order, pFilter: p , pPage:  _page, pSize:  _size));
        try {
 
            return Ok(    _mediator.Send(command, cancellationToken).Result.Data.Select(x => 
                            _mapper.Map<GetProductResponse>(x) 
                         
                         ).ToList<GetProductResponse>());
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message + "=>" + exp.StackTrace.ToString());
        }
    }

    /// <summary>
    /// Deletes a Product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the Product was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try {
            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message);
        }
    }
}
