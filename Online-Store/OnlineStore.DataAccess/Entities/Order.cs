using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DataAccess.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ClerkId { get; set; }

        public User Clerk { get; set; }

        public int? CustomerId { get; set; }

        public User Customer { get; set; }

        public float Total { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public bool isDeleted { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
