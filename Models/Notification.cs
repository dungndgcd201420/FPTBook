using FPTBook.Enums;

using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Notification
    {
        [Key]
        public int NotiId { get; set; }
        public string Message { get; set; }
        public NotiCheck NotiStatus { get; set; }
    }
}
