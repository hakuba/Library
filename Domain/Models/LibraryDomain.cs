namespace Library.Domain.Models
{
    public interface ILibrary
    {
        string Id { get; set; }
        string Name { get; set; }
        List<IBookLibrary>? BookLibrary { get; set; }
        ILibraryInfo? LibraryInfo { get; set; }
    }
    public class LibraryDomain : Entity<ILibrary>
    {
        public LibraryDomain(ILibrary props) : base(props, props.Id)
        {
            this.props = props;
        }
        public static LibraryDomain Create(ILibrary props)
        {
            var instance = new LibraryDomain(props);
            return instance;
        }
        public ILibrary Unmarshal()
        {
            return new LibraryDTO
            {
                Id = this._id,
                Name = this.props.Name,
                LibraryInfo = this.LibraryInfo.Unmarshal(),
            };
        }
        public string Id { get => this._id; set => Id = value; }
        public LibraryInfoDomain? LibraryInfo
        {
            get => this.props.LibraryInfo != null ? LibraryInfoDomain.Create(this.props.LibraryInfo) : null;
            set => this.props.LibraryInfo = value.Unmarshal();
        }
        public List<BookLibraryDomain >? BookLibrary
        {
            get => this.props.BookLibrary != null ? this.props.BookLibrary.Select(book => BookLibraryDomain.Create(book)).ToList() : null;
            set => this.props.BookLibrary = value?.Select(book => book.Unmarshal()).ToList();
        }

    }
    public class LibraryDTO : ILibrary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<IBookLibrary>? BookLibrary { get; set; }
        public ILibraryInfo? LibraryInfo { get; set; }
    }
}
