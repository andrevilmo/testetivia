using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class ProductRequestProfile : Profile
{
    public ProductRequestProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductRateRequest, CreateProductRateCommand>();

        CreateMap<CreateProductResult, CreateProductResponse>().ForMember(dest => dest.Rating, opt => 
                opt.MapFrom(src => new CreateProductRateRequest{
                    Rate = src.Rating.Rate,
                    Count = src.Rating.Count
                })
            );;
        
    }
}