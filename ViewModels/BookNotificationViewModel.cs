using FPTBook.Models;

using Newtonsoft.Json.Linq;

using System.Collections.Generic;

namespace FPTBook.ViewModels
{
    public class BookNotificationViewModel
    {
        public IEnumerable<BookDisplayViewModel> BookDisplay { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
    }
}
