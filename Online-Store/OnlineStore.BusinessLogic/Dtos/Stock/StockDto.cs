using OnlineStore.BusinessLogic.Dtos.Product;

namespace OnlineStore.BusinessLogic.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public int Quantity { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class StockEventDto
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public StockDto Stock { get; set; }

        public int Type { get; set; }

        public string Reason { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
