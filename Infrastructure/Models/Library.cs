using System.ComponentModel.DataAnnotations;

namespace Library.Infrastructure.Models
{
    public class LibraryDBO
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LibraryInfoId { get; set; }
        public LibraryInfo? LibraryInfo { get; set; }
        public ICollection<BookLibrary>? BookLibrary { get; set; }
    }
}
