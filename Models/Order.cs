using FPTBook.Enums;

namespace FPTBook.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
