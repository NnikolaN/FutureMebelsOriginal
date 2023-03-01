
using System;
using System.Collections.Generic;

namespace FutureMebelsOriginal.Data
{
    public class Order
    {
        public int Id { get; set; }

        public int ArticulId { get; set; }

        public Articul Articuls { get; set; }

        public int CustomerId { get; set; }
        public Customer Customers { get; set; }

        public int Quantity { get; set; }

        public DateTime RegisterOn { get; set; }

    }
}
