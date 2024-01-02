using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Category;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.BusinessLogic.Service
{
    public interface ICategoryService
    {
        Task<CategoryDto[]> GetCategories(string? searchString);
        Task<(string ErrorCode, CategoryDto CategoryDto)> GetCategory(int id);
        Task<string> CreateCategory(CategoryDto categoryDto);
        Task<string> UpdateCategory(CategoryDto categoryDto);
        Task<string> DeleteCategory(int id);

    }

    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public CategoryService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CategoryDto[]> GetCategories(string? searchString)
        {
            var categoriesDb = _context.Categorys;

            if (!string.IsNullOrEmpty(searchString))
            {
                var categories = categoriesDb.Where(x => x.Name!.Contains(searchString));
                return _mapper.Map<CategoryDto[]>(categories.ToArray());
            }

            return _mapper.Map<CategoryDto[]>(categoriesDb.ToArray());
        }

        public async Task<(string ErrorCode, CategoryDto CategoryDto)> GetCategory(int id)
        {
            var categoryDto = new CategoryDto();

            var category = await _context.Categorys
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return (ErrorCodes.CategoryNotFound, categoryDto);
            }

            categoryDto = _mapper.Map<CategoryDto>(category);

            return (null, categoryDto);
        }

        public async Task<string> CreateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _context.Categorys.Update(category);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> UpdateCategory(CategoryDto categoryDto)
        {
            var category = await _context.Categorys
                    .FirstOrDefaultAsync(x => x.Id == categoryDto.Id);
            if (category == null)
            {
                return ErrorCodes.CategoryNotFound;
            }

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.Image = categoryDto.Image;

            _context.Categorys.Update(category);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> DeleteCategory(int id)
        {
            var category = await _context.Categorys
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return ErrorCodes.CategoryNotFound;
            }

            category.isDeleted = true;
            _context.Categorys.Update(category);
            await _context.SaveChangesAsync();

            return string.Empty;
        }
    }
}
