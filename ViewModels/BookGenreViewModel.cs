using FPTBook.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace FPTBook.ViewModels
{
    public class BookGenreViewModel
    {
        public Book Book{ get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
  }
}
