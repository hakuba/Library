using System.ComponentModel.DataAnnotations;

namespace Library.Infrastructure.Models
{
    public class LibraryInfo
    {
        [Key]
        public string Id { get; set; }
        public string LibraryId { get; set; }
        public LibraryDBO Library { get; set; }
        public string Address { get; set; }
        public string HoursOfOperation { get; set; }
    }
}
