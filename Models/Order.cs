using FPTBook.Enums;

using System.Collections.Generic;

namespace FPTBook.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<Cart> CartItems { get; set; }
        public decimal OrderTotal { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
