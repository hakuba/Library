using Library.Domain.Models;
using Library.Libs;

namespace Library.Domain.ServiceRepository
{
    public interface ILibraryRepository
    {
        Task<LibraryDomain> FindById(string id);
        Task<IEnumerable<LibraryDomain>> FindAll();
        Task<bool> Store(RequestLibs.AddNewLibrary req);
        Task<bool> Update(string id,RequestLibs.UpdateLibrary req);
        Task<bool> Delete(string id);
    }
}
