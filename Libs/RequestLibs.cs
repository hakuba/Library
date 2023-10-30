using Library.Domain.Models;

namespace Library.Libs
{
    public class RequestLibs
    {
        public class AddNewBook
        {
            public string Title { get; set; }
            public Guid AuthorId { get; set; }
        }
        public class UpdateBook
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid AuthorId { get; set; }
        }
        public class AddNewAuthor
        {
            public string Name { get; set;}

        }
        public class UpdateAuthor
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        public class ILibInfoForRequest
        {
            public string address { get; set; }
            public string hoursOfOperation { get; set; }
        }
        public class AddNewLibrary
        {
            public string Name { get; set; }
            public ILibInfoForRequest LibraryInfo { get; set; }
        }
        
        public class UpdateLibrary
        {
            public string Name { get; set; }
            public ILibInfoForRequest LibraryInfo { get; set; }
        }
        public class AddNewBookLibrary
        {
            public Guid BookId { get; set; }
            public int Stock { get; set; }
        }
        public class UpdateBookLibrary
        {
            public Guid BookId { get; set; }
            public Guid LibraryId { get; set; }
            public int Stock { get; set; }
        }
    }
}
