using Library.Domain.Models;
using Library.Libs;

namespace Library.Domain.ServiceRepository
{
    public interface IAuthorRepository
    {
        Task<AuthorDomain> FindById(string id);
        Task<IEnumerable<AuthorDomain>> FindAll();
        Task<bool> Store(RequestLibs.AddNewAuthor req);
        Task<bool> Update(RequestLibs.UpdateAuthor req);
        Task<bool> Delete(string id);
    }
}
