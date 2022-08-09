using Microsoft.AspNetCore.Identity;

namespace FPTBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
