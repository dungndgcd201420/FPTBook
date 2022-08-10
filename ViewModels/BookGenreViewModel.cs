using FPTBook.Models;

using System.Collections.Generic;


namespace FPTBook.ViewModels
{
    public class BookGenreViewModel
    {
        public Book Book{ get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    
    }
}
