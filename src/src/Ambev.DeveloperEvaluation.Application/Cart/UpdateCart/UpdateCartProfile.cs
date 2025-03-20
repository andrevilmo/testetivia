using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Model;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between Cart entity and UpdateCartResponse
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart operation
    /// </summary>
    public UpdateCartProfile()
    {
        
        CreateMap<Cart, UpdateCartResult>();
        
        CreateMap<UpdateCartCommand, UpdateCartResult>().ReverseMap();
        CreateMap<UpdateCartCommand , Cart>().ReverseMap();
        CreateMap<UpdateCartItemCommand , CartItem>().ReverseMap();
        CreateMap<Cart, UpdateCartResult>().ReverseMap();
        CreateMap<CartItem, UpdateCartItemCommand>().ReverseMap();
        
    }
}
