using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DataAccess.Entities
{
    [Table("ProductImages")]
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [StringLength(50)]
        public string Path { get; set; }
       
    }
}
