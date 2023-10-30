namespace Library.Domain.Models
{
    public interface ILibraryInfo
    {
        string Id { get; set; }
        string Address { get; set; }
        string HoursOfOperation { get; set; }

        // 1-1 relationship with Library
        public ILibrary Library { get; set; }
    }
    public class LibraryInfoDomain : Entity<ILibraryInfo>
    {
        public LibraryInfoDomain(ILibraryInfo props) : base(props, props.Id)
        {
            this.props = props;
        }
        public static LibraryInfoDomain Create(ILibraryInfo props)
        {
            var instance = new LibraryInfoDomain(props);
            return instance;
        }
        public ILibraryInfo Unmarshal()
        {
            return new LibraryInfoDTO{
                Id = this._id,
                Address = this.props.Address,
                HoursOfOperation = this.props.HoursOfOperation
            };
        }
        public string Id { get => this._id; set => Id = value; }


    }
    public class LibraryInfoDTO : ILibraryInfo
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string HoursOfOperation { get; set; }

        // 1-1 relationship with Library
        public ILibrary Library { get; set; }
    }
}
