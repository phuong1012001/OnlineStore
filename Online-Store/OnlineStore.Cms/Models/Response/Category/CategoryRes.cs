namespace OnlineStore.Cms.Models.Response.Category
{
    public class CategoryRes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Boolean isDeleted { get; set; }
    }
}
