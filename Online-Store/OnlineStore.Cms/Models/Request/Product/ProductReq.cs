namespace OnlineStore.Cms.Models.Request.Product
{
    public class ProductReq
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
    }
}
