namespace OnlineStore.Cms.Models.Response.Product
{
    public class ProductEditRes
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }

        public int CategoryId { get; set; }
    }
}
