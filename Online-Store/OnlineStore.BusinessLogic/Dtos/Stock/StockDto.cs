﻿namespace OnlineStore.BusinessLogic.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? CategoryName { get; set; }

        public int Quantity { get; set; }
    }
}
