using OnlineStore.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DataAccess.Entities
{
    [Table("StockEvents")]
    public class StockEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StockId { get; set; }

        public Stock Stock { get; set; }

        public Types Type { get; set; }

        [StringLength(50)]
        public string Reason { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset CreatedAt { get; set; }       
    }
}
