using AutoMapper;
using PivoGo.Application.CQRS.Commands.Products.CreateProduct;
using PivoGo.Domain.ProductAggregate;
using PivoGo.Application.CQRS.Commands.Products.UpdateProduct;

namespace PivoGo.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}
