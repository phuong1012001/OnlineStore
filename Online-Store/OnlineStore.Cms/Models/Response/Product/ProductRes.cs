using OnlineStore.BusinessLogic.Dtos.Category;

namespace OnlineStore.Cms.Models.Response.Product
{
    public class ProductRes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; }

        public Boolean isDeleted { get; set; }
    }
}
