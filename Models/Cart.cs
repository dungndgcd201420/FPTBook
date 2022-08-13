using System;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Cart
    {
        [Key]
        public string UserId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

        public DateTime OrderedAt { get; set; } = DateTime.Now;
    }
}
