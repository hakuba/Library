using System.ComponentModel.DataAnnotations;

namespace Library.Infrastructure.Models
{
    public class Author
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
