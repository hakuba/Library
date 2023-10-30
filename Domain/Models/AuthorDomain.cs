namespace Library.Domain.Models
{
    public interface IAuthor
    {
        string? Id { get; set; }
        string Name { get; set; }
        List<BookDomain>? Books { get; set; }

    }
    public class AuthorDomain : Entity<IAuthor>
    {
        public AuthorDomain(IAuthor props) : base(props, props.Id)
        {
            this.props = props;
        }
        public static AuthorDomain Create(IAuthor props)
        {
            var instance = new AuthorDomain(props);
            return instance;
        }
        public IAuthor Unmarshal()
        {
            return new AuthorDTO
            {
                Id = this.Id,
                Name = this.props.Name,
                Books = this.Books

            };
        }
        public string Id { get => this._id; set => Id = value; }
        public List<BookDomain>? Books
        {
            get => this.props.Books != null ? this.props.Books.Select(book => book).ToList() : null;
            set => this.props.Books = value?.Select(book => book).ToList();
        }
    }
    public class AuthorDTO: IAuthor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<BookDomain>? Books { get; set; }
    }
}
