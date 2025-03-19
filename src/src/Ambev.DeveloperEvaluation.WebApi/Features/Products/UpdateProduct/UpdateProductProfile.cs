using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Common.Model;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Application and API UpdateProduct responses
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct feature
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRateRequest, UpdateProductRateCommand>();
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<UpdateProductResult, UpdateProductResponse>();
        CreateMap<UpdateProductRateCommand, IProductRate>();
        CreateMap<UpdateProductRateResult , UpdateProductRateRequest>();
        CreateMap<UpdateProductRateRequest, UpdateProductRateResult >();
        CreateMap<UpdateProductResponse, UpdateProductResult>().ForMember(dest => dest.Rating, opt => 
                opt.MapFrom(src => 
                    new UpdateProductRateResult{
                        Rate = src.Rating.Rate,
                        Count = src.Rating.Count
                    })
            );
    }
}
