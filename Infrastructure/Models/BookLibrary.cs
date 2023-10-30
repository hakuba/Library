using Library.Domain.Models;

namespace Library.Infrastructure.Models
{
    public class BookLibrary
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string LibraryId { get; set; }
        public LibraryDBO Library { get; set; }
        public int Stock { get; set; }
    }
}
