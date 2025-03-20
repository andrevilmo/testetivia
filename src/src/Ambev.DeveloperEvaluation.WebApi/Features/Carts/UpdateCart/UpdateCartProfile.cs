using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Common.Model;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between Application and API UpdateCart responses
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart feature
    /// </summary>
    public UpdateCartProfile()
    { 
        CreateMap<UpdateCartRequest, UpdateCartCommand>().ReverseMap();
        CreateMap<UpdateCartResult, UpdateCartResponse>().ReverseMap();
        CreateMap<UpdateCartItemRequest, UpdateCartItemCommand>().ReverseMap();
    }
}
