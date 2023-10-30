using Library.Domain.Models;
using Library.Libs;

namespace Library.Domain.ServiceRepository
{
    public interface IBookLibraryRepository
    {
        Task<BookLibraryDomain> FindById(string libId, string bookId);
        Task<IEnumerable<BookLibraryDomain>> FindAll();
        Task<bool> Store(string libId,RequestLibs.AddNewBookLibrary req);
        Task<bool> Update(string libId, string bookId, RequestLibs.UpdateBookLibrary req);
        Task<bool> Delete(string id, string bookId);
    }
}
