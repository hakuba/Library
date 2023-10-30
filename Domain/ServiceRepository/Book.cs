using Library.Domain.Models;
using Library.Libs;

namespace Library.Domain.ServiceRepository
{
    public interface IBookRepository
    {
        Task<BookDomain> FindById(string id);
        Task<IEnumerable<BookDomain>> FindAll();
        Task<bool> Store(RequestLibs.AddNewBook req);
        Task<bool> Update(RequestLibs.UpdateBook req);
        Task<bool> Delete(string id);
    }
}
