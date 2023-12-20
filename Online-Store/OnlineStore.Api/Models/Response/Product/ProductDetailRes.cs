namespace OnlineStore.Api.Models.Responses.Product
{
    public class ProductDetailRes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }

        public int Quantity { get; set; }

        public CategoryRes Category { get; set; }

        public string? ErrorCode { get; set; }
    }

    public class CategoryRes
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
