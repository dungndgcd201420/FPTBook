using FPTBook.Enums;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public List<Cart> CartList { get; set; }
        public float Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
