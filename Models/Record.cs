using System;

namespace FPTBook.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecordId { get; set; }
        public ApplicationUser FullName { get; set; }
        public ApplicationUser Address { get; set; }
        public DateTime OrderedAt { get; set; }

    }
}
