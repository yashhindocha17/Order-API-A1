using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public Order? Order { get; set; }
    }
}
