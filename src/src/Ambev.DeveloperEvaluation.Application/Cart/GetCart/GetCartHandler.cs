using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Handler for processing GetCartCommand requests
/// </summary>
public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
{
    private readonly ICartRepository _CartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCartHandler
    /// </summary>
    /// <param name="CartRepository">The Cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCartCommand</param>
    public GetCartHandler(
        ICartRepository CartRepository,
        IMapper mapper)
    {
        _CartRepository = CartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCartCommand request
    /// </summary>
    /// <param name="request">The GetCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart details if found</returns>
    public async Task<GetCartResult> Handle(GetCartCommand request, CancellationToken cancellationToken)
    {
        var retEnum = new GetCartResult();
        var validator = new GetCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        var Id = request.Id;
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (request.Id != null && !Guid.Empty.Equals(request.Id) ) {
            var c = await _CartRepository.GetByIFilterAsync(
                request.Order,
                new Dictionary<string, string>{{"id",Id.ToString()}},
                request.Page,
                request.Size,
                cancellationToken);
            var p = _mapper.Map<List<GetCartResult>>(c);
            retEnum.Data = p;
            if (retEnum == null)
                throw new KeyNotFoundException($"Cart with ID {request.Id} not found");
        } else {
            var c = await _CartRepository.GetByIFilterAsync(
                request.Order,
                request.Filter,
                request.Page,
                request.Size,
                cancellationToken);
            var p = _mapper.Map<List<GetCartResult>>(c);
            retEnum.Data = p;
        }
        return retEnum;
    }
}
