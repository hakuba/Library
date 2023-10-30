using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using Library.Libs;
namespace Library.Services
{

    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<bool> Add(RequestLibs.AddNewAuthor req)
        {
            var author = await _authorRepository.Store(req);

            if (!author)
                return false;
            return true;
        }
        public async Task<IEnumerable<IAuthor>> GetAll()
        {
            var authors = await _authorRepository.FindAll();
            if (authors == null)
                return null;
            var res = authors.Select(data => data.Unmarshal());
            return res;
        }

        public async Task<IAuthor> GetById(Guid id)
        {
            var author = await _authorRepository.FindById(id.ToString());
            if (author == null)
                return null;
            var res = author.Unmarshal();
            return res;
        }



        public async Task<bool> Update(RequestLibs.UpdateAuthor req)
        {
            return await _authorRepository.Update(req);
        }

        public async Task<bool> Delete(Guid id)
        {
            var author = await _authorRepository.FindById(id.ToString());
            if (author != null)
            {
                return await _authorRepository.Delete(id.ToString());
            }
            return false;
        }
    }
}