using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DataAccess.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Thumbnail { get; set; }

        public float UnitPrice { get; set; }

        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public bool isDeleted { get; set; }
    }
}
