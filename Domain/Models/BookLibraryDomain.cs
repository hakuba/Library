namespace Library.Domain.Models
{
    public interface IBookLibrary
    {
        string Id { get; set; }
        string BookId { get; set; }
        string LibraryId { get; set; }
        int Stock { get; set; }
        IBook? Book { get; set; }
        ILibrary? Library { get; set; }
        
    }
    public class BookLibraryDomain : Entity<IBookLibrary>
    {
        public BookLibraryDomain(IBookLibrary props) : base(props, props.Id)
        {
            this.props = props;
        }
        public static BookLibraryDomain Create(IBookLibrary props)
        {
            var instance = new BookLibraryDomain(props);
            return instance;
        }
        public IBookLibrary Unmarshal()
        {
            return new BookLibraryDTO
            {
                Id = this.Id,
                BookId = this.props.BookId,
                Book = this.props.Book,
                LibraryId = this.props.LibraryId,
                Library = this.props.Library,
                Stock = this.props.Stock,

            };
        }
        public string Id { get => this._id; set => Id = value; }

    }
    public class BookLibraryDTO : IBookLibrary
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public string LibraryId { get; set; }
        public int Stock { get; set; }
        public IBook? Book { get; set; }
        public ILibrary? Library { get; set; }
    }
}
