using FPTBook.Enums;
using FPTBook.Models;

namespace FPTBook.ViewModels
{
    public class BookDisplayViewModel
    {
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public float Price { get; set; }
    public Genre Genre{ get; set; }
    public BookStatus BookStatus { get; set; }
    public string ImageUrl { get; set; }
  }
}
