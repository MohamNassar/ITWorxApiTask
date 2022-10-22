using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Services.MapperProfile
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryForPatchDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
            CreateMap<JsonPatchDocument<CategoryForPatchDto>, JsonPatchDocument<Category>>();
            CreateMap<Operation<CategoryForPatchDto>, Operation<Category>>();
        }
    }
}
