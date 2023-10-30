using System.ComponentModel.DataAnnotations;

namespace Library.Infrastructure.Models
{
    public class Book
    {
        [Key]
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public Author Author {  get; set; }
        public ICollection<BookLibrary>? BookLibrary { get; set; }
        public string Title { get; set; }
    }
}
