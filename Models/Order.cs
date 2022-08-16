using FPTBook.Enums;

using System.Collections.Generic;

namespace FPTBook.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Cart Item { get; set; }
        public float Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
