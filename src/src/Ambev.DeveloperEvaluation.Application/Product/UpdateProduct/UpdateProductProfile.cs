using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Model;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Product entity and UpdateProductResponse
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct operation
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRateCommand, IProductRate>().ConstructUsing(x => 
                        (IProductRate)new ProductRate());
        CreateMap<UpdateProductRateResult, IProductRate>().ConstructUsing(x => 
                (IProductRate)new ProductRate());

        CreateMap<ProductRate, UpdateProductRateResult>();


        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Rating, opt => 
                opt.MapFrom(src => new ProductRate{Rate = src.Rating.Rate,Count = src.Rating.Count})
            );

        CreateMap<Product, UpdateProductResult>();
    }
}
