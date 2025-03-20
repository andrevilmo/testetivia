using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Model;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
          

/// <summary>
/// Profile for mapping between Cart entity and CreateCartResponse
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCart operation
    /// </summary>
    public CreateCartProfile()
    { 
        CreateMap<CreateCartCommand, CreateCartResult>().ReverseMap();
        CreateMap<CreateCartCommand , Cart>().ReverseMap();
        CreateMap<CreateCartItemCommand , CartItem>().ReverseMap();
        CreateMap<Cart, CreateCartResult>().ReverseMap();
        CreateMap<CartItem, CreateCartItemCommand>().ReverseMap();
    }
}
