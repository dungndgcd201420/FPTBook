﻿using FPTBook.Data;
using FPTBook.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBook.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required(ErrorMessage = "There is no title for this book")]
        [StringLength(255)]
        public string Title { get; set; }   
        public float Price { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public BookStatus BookStatus { get; set; } 
        [Required]
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public byte[] ImageData { get; set; }
    }
}
