using FPTBook.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<Cart> CartList { get; set; }
        public float Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime FinishedAt { get; set; } = DateTime.Now;
    }
}
