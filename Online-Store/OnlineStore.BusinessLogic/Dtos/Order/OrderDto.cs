using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.BusinessLogic.Dtos.Auth;

namespace OnlineStore.BusinessLogic.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int? ClerkId { get; set; }

        public UserDto Clerk { get; set; }

        public int? CustomerId { get; set; }

        public UserDto Customer { get; set; }

        public float Total { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public bool isDeleted { get; set; }

        public ICollection<OrderDetailDto> OrderDetails { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class OrderDetailDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public OrderDto Order { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public float UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
