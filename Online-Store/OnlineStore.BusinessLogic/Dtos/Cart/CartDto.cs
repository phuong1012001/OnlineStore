namespace OnlineStore.BusinessLogic.Dtos.Cart
{
    public class CartDto
    {
        public CartDetailDto[] CartDetails { get; set; }

        public float Total { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class CartDetailDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductCartDto Product { get; set; }

        public int Quantity { get; set; }

        public float Total { get; set; }
    }

    public class ProductCartDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }
    }
}
