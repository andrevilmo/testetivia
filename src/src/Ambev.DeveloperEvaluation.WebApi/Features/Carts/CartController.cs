using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing Cart operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    private readonly MongoService _mongoService;

    /// <summary>
    /// Initializes a new instance of CartsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartController(IMediator mediator, IMapper mapper, MongoService mongoService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _mongoService = mongoService;
    }

    /// <summary>
    /// Creates a new Cart
    /// </summary>
    /// <param name="request">The Cart creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        await _mongoService.CreateAsync( _mapper.Map<Cart>(response) );
        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = _mapper.Map<CreateCartResponse>(response)
        });
    }


     /// <summary>
    /// Updates a Cart
    /// </summary>
    /// <param name="request">The Cart to update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Cart details</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCart([FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        try {
            var command = _mapper.Map<UpdateCartCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);
                var cart = _mapper.Map<Cart>(response);
                await _mongoService.UpdateAsync( cart.Id, cart );
                return Ok (new ApiResponseWithData<UpdateCartResponse>
                {
                    Success = true,
                    Message = "Cart updated successfully",
                    Data = _mapper.Map<UpdateCartResponse>(response)
                });
        } catch (Exception exp) {
                return BadRequest(exp.Message + " => " + exp.StackTrace.ToString());
        }
    
    }

    /// <summary>
    /// Retrieves a Cart by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var cart = await _mongoService.GetAsync( request.Id );
        if (cart != null)
            return Ok(cart);
        var command = _mapper.Map<GetCartCommand>(request.Id);
        try {
            
            var result = await _mediator.Send(command, cancellationToken);
            var response = result.Data.ToList().Select(x=>
                    new GetCartResponse {
                        Id = x.Id,
                        Date = x.Date,
                        UserId = x.UserId,
                        Products = x.Products
                    }
                );
            cart = _mapper.Map<Cart>(_mapper.Map<Cart>(response.First()));
            await _mongoService.CreateAsync(cart);
            return Ok(response);
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message);
        }
    }

    /// <summary>
    /// Retrieves a Cart list
    /// </summary>ConfirmDelete
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart details if found</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartListBy( 
                                                    CancellationToken cancellationToken,
                                                    [FromQuery] int _page = 1, 
                                                    [FromQuery] int _size = 10, 
                                                    [FromQuery] string _order = "")
    {   
        var request = new GetCartRequest { 
                    Id = Guid.Empty, 
                    Order = "", 
                    Filter = null, 
                    Page = 1, 
                    Size = 10
                };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var p = 
            this.Request.Query.Select(x => new KeyValuePair<string,string> (x.Key, x.Value))
                .ToDictionary<string,string>();
        var command = _mapper.Map<GetCartCommand>(new GetCartCommand(pOrder: _order, pFilter: p , pPage:  _page, pSize:  _size));
        try {
            var result =     await _mediator.Send(command, cancellationToken);
            var response = result.Data.ToList().Select(x=>
                    new GetCartResponse {
                        Id = x.Id,
                        Date = x.Date,
                        UserId = x.UserId,
                        Products = x.Products
                    }
                );
            return Ok(response);
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message + "=>" + exp.StackTrace.ToString());
        }
    }

    /// <summary>
    /// Deletes a Cart by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the Cart was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try {
            var command = _mapper.Map<DeleteCartCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cart deleted successfully"
            });
        } catch (KeyNotFoundException exp) {
            return new NotFoundObjectResult(exp.Message);
        } catch (Exception exp) {
            return BadRequest(exp.Message);
        }
    }
}
