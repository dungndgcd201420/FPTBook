using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FPTBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }

        public List<Book> books { get; set; }

    }
}
