using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Profile for mapping GetProduct feature requests to commands
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct feature
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Guid, Application.Products.GetProduct.GetProductCommand>()
            .ConstructUsing(id => new Application.Products.GetProduct.GetProductCommand(id));
       
        CreateMap<GetProductResponse, GetProductResult>().ForMember(dest => dest.Rating, opt => 
                opt.MapFrom(src => 
                    new CreateProductRateResult{
                        Rate = src.Rating.Rate,
                        Count = src.Rating.Count
                    })
            );;
        CreateMap<GetProductResult,GetProductResponse>().ForMember(dest => dest.Rating, opt => 
                opt.MapFrom(src => 
                    new CreateProductRateResult{
                        Rate = src.Rating.Rate,
                        Count = src.Rating.Count
                    })
            );;
    }
}
