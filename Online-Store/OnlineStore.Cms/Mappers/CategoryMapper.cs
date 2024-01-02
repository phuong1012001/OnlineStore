using AutoMapper;
using OnlineStore.BusinessLogic.Dtos.Category;
using OnlineStore.Cms.Models.Request.Category;
using OnlineStore.Cms.Models.Response.Category;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Cms.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, CategoryRes>();
            CreateMap<CategoryReq, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryDto, CategoryEditRes>();
            CreateMap<CategoryEditReq, CategoryDto>();
        }
    }
}
