using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Services.MapperProfile
{
  
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductForPatchDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<JsonPatchDocument<ProductForPatchDto>, JsonPatchDocument<Product>>();
            CreateMap<Operation<ProductForPatchDto>, Operation<Product>>();
        }
    }
}
