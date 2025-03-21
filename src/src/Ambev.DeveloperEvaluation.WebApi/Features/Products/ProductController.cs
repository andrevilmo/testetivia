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
using System.Text.Json;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing Product operations
/// </summary>
[ApiController]

[Authorize]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IConnectionMultiplexer _redis;
    private readonly   IDatabase dbCache;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper, IConnectionMultiplexer redis)
    {
        _mediator = mediator;
        _mapper = mapper;
        _redis = redis;
         dbCache = redis.GetDatabase();
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

            var chaveCache = "PRODUTOS_BY_ID_"
                + Convert.ToBase64String( System.Text.ASCIIEncoding.UTF8.GetBytes( JsonSerializer.Serialize(request)));
            var cache = dbCache.StringGet(chaveCache);
            if (cache.HasValue)
                return Ok(JsonSerializer.Deserialize<GetProductResponse>(cache));
            var ret =  _mapper.Map<GetProductResponse>(response); 
                        
            dbCache.StringSet(chaveCache,JsonSerializer.Serialize(ret));
            return Ok( ret );
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message);
        }
    }

    /// <summary>
    /// Retrieves products from category
    /// </summary>
    /// <param name="category">The catgory to retrieve</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product list if found</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductList( 
                                                    CancellationToken cancellationToken,
                                                    [FromQuery] int _page = 1, 
                                                    [FromQuery] int _size = 10, 
                                                    [FromQuery] string _order = "", 
                                                    [FromRoute] string category = "")
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
        p.Add("category",category);
        var command = _mapper.Map<GetProductCommand>(new GetProductCommand(pOrder: _order, pFilter: p , pPage:  _page, pSize:  _size));
        try {
            var chaveCache = "PRODUTOS_CATEGORIA_"
                + Convert.ToBase64String( System.Text.ASCIIEncoding.UTF8.GetBytes( JsonSerializer.Serialize(request)));
            var cache = dbCache.StringGet(chaveCache); 
            if (cache.HasValue)
                return Ok(JsonSerializer.Deserialize<List<GetProductResponse>>(cache));
            var ret = _mediator.Send(command, cancellationToken).Result.Data.Select(x => 
                            _mapper.Map<GetProductResponse>(x) 
                         
                         ).ToList<GetProductResponse>();
                         
             dbCache.StringSet(chaveCache,JsonSerializer.Serialize(ret));
            return Ok( ret );
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message + "=>" + exp.StackTrace.ToString());
        }
       
    }

    /// <summary>
    /// Retrieves a Product by search
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product details if found</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductListBySearch( 
                                                    CancellationToken cancellationToken,
                                                    [FromQuery] int _page = 1, 
                                                    [FromQuery] int _size = 10, 
                                                    [FromQuery] string _order = "")
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
                var chaveCache = "PRODUTOS_BY_SEARCH_"
                        + Convert.ToBase64String( System.Text.ASCIIEncoding.UTF8.GetBytes( JsonSerializer.Serialize(request)));
                    var cache = dbCache.StringGet(chaveCache);
                    if (cache.HasValue)
                        return Ok(JsonSerializer.Deserialize<List<GetProductResponse>>(cache));
                    var ret =  _mediator.Send(command, cancellationToken).Result.Data.Select(x => 
                            _mapper.Map<GetProductResponse>(x)); 
                                
                    dbCache.StringSet(chaveCache,JsonSerializer.Serialize(ret));
                    return Ok( ret );

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
