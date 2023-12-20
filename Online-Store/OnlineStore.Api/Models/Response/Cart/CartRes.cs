namespace OnlineStore.Api.Models.Response.Cart
{
    public class CartRes
    {
        public CartDetailRes[] CartDetails { get; set; }

        public float Total { get; set; }
    }

    public class CartDetailRes
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductCartRes Product { get; set; }

        public int Quantity { get; set; }

        public float Total { get; set; }
    }

    public class ProductCartRes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }
    }
}
