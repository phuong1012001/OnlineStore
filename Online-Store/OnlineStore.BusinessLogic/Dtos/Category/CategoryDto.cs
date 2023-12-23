namespace OnlineStore.BusinessLogic.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Boolean isDeleted { get; set; }

        public string? ErrorCode { get; set; }
    }
}
