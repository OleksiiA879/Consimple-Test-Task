﻿namespace Consimple_Test_Task.Models
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }
        public Orders Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; } 
    }
}
