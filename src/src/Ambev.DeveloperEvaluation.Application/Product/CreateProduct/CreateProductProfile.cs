using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Model;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Product entity and CreateProductResponse
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductRateCommand, IProductRate>().ConstructUsing(x => 
                        (IProductRate)new ProductRate());
        CreateMap<CreateProductRateResult, IProductRate>().ConstructUsing(x => 
                (IProductRate)new ProductRate());

        CreateMap<ProductRate, CreateProductRateResult>();


        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Rating, opt => 
                opt.MapFrom(src => new ProductRate{Rate = src.Rating.Rate,Count = src.Rating.Count})
            );

        CreateMap<Product, CreateProductResult>();
    }
}
