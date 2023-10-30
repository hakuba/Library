using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.Domain.Models
{
    public interface IBook
    {
        string? Id { get; set; }
        string Title { get; set; }
        IAuthor? Author { get; set; }
        string AuthorId { get; set; }
        List<IBookLibrary>? bookLibrary { get; set; }

    }
    public class BookDomain: Entity<IBook>
    {
        public BookDomain(IBook props) : base(props, props.Id)
        {
            this.props = props;
        }
        public static BookDomain Create(IBook props)
        {
            var instance = new BookDomain(props);
            return instance;
        }
        public IBook Unmarshal()
        {
            return new BookDTO
            {
                Id = this.Id,
                Title = this.props.Title,
                AuthorId = this.props.AuthorId,
                Author = this.Author,

            };
        }
        public string Id { get=> this._id; set => Id= value; }
        public IAuthor? Author { 
            get => this.props.Author;
            set => this.props.Author = value;
        }
    }
    public class BookDTO : IBook
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public IAuthor? Author { get; set; }
        public string AuthorId { get; set; }
        public List<IBookLibrary>? bookLibrary { get; set; }
    }
}
