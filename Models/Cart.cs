using System;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Cart
    {
        [Key]
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }   
        public float Price { get; set; }
        public Genre Genre { get; set; }
        public DateTime OrderedAt { get; set; } = DateTime.Now;
    }
}
