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
            //Index
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, CategoryRes>();

            //Create
            CreateMap<CategoryReq, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            //Edit
            CreateMap<CategoryDto, CategoryEditRes>();
            CreateMap<CategoryEditReq, CategoryDto>();
        }
    }
}
