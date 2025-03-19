using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Common.Model;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct feature
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductRateRequest, CreateProductRateCommand>();
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();
        CreateMap<CreateProductRateCommand, IProductRate>();
        CreateMap<CreateProductRateResult , CreateProductRateRequest>();
        CreateMap<CreateProductRateRequest, CreateProductRateResult >();
        CreateMap<CreateProductResponse, CreateProductResult>().ForMember(dest => dest.Rating, opt => 
                opt.MapFrom(src => 
                    new CreateProductRateResult{
                        Rate = src.Rating.Rate,
                        Count = src.Rating.Count
                    })
            );
    }
}
