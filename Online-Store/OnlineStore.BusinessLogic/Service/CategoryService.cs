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
        Task<List<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategory(int id);
        Task CreateCategory(CategoryDto categoryDto);
        Task<List<CategoryDto>> GetSearch(string searchString);
        Task<CategoryResultDto> SaveCategory(CategoryDto categoryDto);
        Task<CategoryResultDto> DeleteCategory(int id);

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

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = _context.Category
                .ToList();

            var result = _mapper.Map<List<CategoryDto>>(categories);

            return result;
        }

        public async Task<CategoryDto> GetCategory(int id)
        {
            var result = new CategoryDto();

            var category = await _context.Category
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundCategory;
                return result;
            }
            result = _mapper.Map<CategoryDto>(category);

            return result;
        }

        public async Task CreateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _context.Category.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryDto>> GetSearch(string searchString)
        {
            var categories = _context.Category
                .Where(x => x.Name!.Contains(searchString))
                .ToList();

            var result = _mapper.Map<List<CategoryDto>>(categories);

            return result;
        }

        public async Task<CategoryResultDto> SaveCategory(CategoryDto categoryDto)
        {
            var result = new CategoryResultDto();

            var category = await _context.Category
                    .FirstOrDefaultAsync(x => x.Id == categoryDto.Id);

            if (category == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundCategory;
                return result;
            }

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.Image = categoryDto.Image;

            _context.Category.Update(category);
            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }

        public async Task<CategoryResultDto> DeleteCategory(int id)
        {
            var result = new CategoryResultDto();

            var category = await _context.Category
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundCategory;
                return result;
            }

            category.isDeleted = true;
            _context.Category.Update(category);
            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }
    }
}
