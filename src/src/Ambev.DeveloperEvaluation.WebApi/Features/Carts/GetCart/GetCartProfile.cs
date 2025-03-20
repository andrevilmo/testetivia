using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Profile for mapping GetCart feature requests to commands
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart feature
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<Guid, Application.Carts.GetCart.GetCartCommand>()
        .ConstructUsing(id => new Application.Carts.GetCart.GetCartCommand(id));
        CreateMap<GetCartResult , GetCartResponse>().ReverseMap();
        CreateMap<GetCartResponse, GetCartResult>().ReverseMap();
        
    }
}
