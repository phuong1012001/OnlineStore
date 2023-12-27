using OnlineStore.DataAccess.Entities;
using OnlineStore.BusinessLogic.Dtos.Category;
using OnlineStore.BusinessLogic.Dtos.Auth;

namespace OnlineStore.BusinessLogic.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }

        public int CreatedBy { get; set; }

        public UserDto User { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public bool isDeleted { get; set; }

        public int Quantity { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class ProductImageDto
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public string Path { get; set; }
    }
}