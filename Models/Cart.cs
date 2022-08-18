using System;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Cart
    {
        
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}
