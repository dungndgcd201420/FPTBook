using FPTBook.Enums;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Genre can not be null")]
        [StringLength(255)]
        public string Description { get; set; }
        public GenreApproval Status { get; set; }
        public List<Book> Books { get; set; }
    }
}
