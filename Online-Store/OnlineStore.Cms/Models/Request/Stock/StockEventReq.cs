using OnlineStore.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Cms.Models.Request.Stock
{
    public class StockEventReq
    {
        public Types Type { get; set; }

        public string Reason { get; set; }

        public int Quantity { get; set; } 
    }
}
