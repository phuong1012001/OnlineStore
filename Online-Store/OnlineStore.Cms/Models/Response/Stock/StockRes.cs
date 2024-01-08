namespace OnlineStore.Cms.Models.Response.Stock
{
    public class StockRes
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? CategoryName { get; set; }

        public int Quantity { get; set; }
    }
}
