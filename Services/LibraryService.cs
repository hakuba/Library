using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using Library.Libs;

namespace Library.Services
{
    public class LibraryService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookLibraryRepository _relationRepository;
        public LibraryService(ILibraryRepository libraryRepository, IBookRepository bookRepository, IBookLibraryRepository relationRepository)
        {
            _libraryRepository = libraryRepository;
            _bookRepository = bookRepository;
            _relationRepository = relationRepository;
        }
        public async Task<bool> Add(RequestLibs.AddNewLibrary req)
        {
            var library = await _libraryRepository.Store(req);

            if (!library)
                return false;
            return true;
        }
        public async Task<IEnumerable<ILibrary>> GetAll()
        {
            var libs = await _libraryRepository.FindAll();
            var res = libs.Select(data => data.Unmarshal());
            return res;
        }

        public async Task<ILibrary> GetById(Guid id)
        {
            var library = await _libraryRepository.FindById(id.ToString());
            var res = library.Unmarshal();
            return res;
        }



        public async Task<bool> Update(string id, RequestLibs.UpdateLibrary req)
        {
            var lib = await _libraryRepository.FindById(id);
            if (lib == null)
                return false;
            var res = await _libraryRepository.Update(id, req);
            return res;
        }

        public async Task<bool> Delete(Guid id)
        {
            var library = await _libraryRepository.FindById(id.ToString());
            if (library != null)
            {
                return await _libraryRepository.Delete(id.ToString());
            }
            return false;
        }
        public async Task<bool> AddBook(Guid id, RequestLibs.AddNewBookLibrary req)
        {
            var lib = await _libraryRepository.FindById(id.ToString());
            if (lib == null)
                return false;
            var book = await _bookRepository.FindById(req.BookId.ToString());
            if (book == null)
                return false;
            var rel = await _relationRepository.Store(id.ToString(), req);
            var res = true;
            return res;
        }
        public async Task<IEnumerable<IBookLibrary>> ListBook(Guid id)
        {
            var relations = await _relationRepository.FindAll();
            var res = relations.Select(data => data.Unmarshal());
            return res;
        }
        public async Task<IBookLibrary> GetRelationById(Guid id , Guid bookId)
        {

            var relation = await _relationRepository.FindById(id.ToString(), bookId.ToString());
            var res = relation.Unmarshal();
            return res;
        }
        public async Task<bool> UpdateBook(Guid id, Guid bookId, RequestLibs.UpdateBookLibrary req)
        {

            var lib = await _libraryRepository.FindById(req.LibraryId.ToString());
            if (lib == null)
                return false;
            var book = await _bookRepository.FindById(req.BookId.ToString());
            if (book == null)
                return false;
            var res = await _relationRepository.Update(id.ToString(), bookId.ToString(),req);

            return res;
        }
        public async Task<bool> DeleteBook(Guid id, Guid bookId)
        {
            var lib = await _libraryRepository.FindById(id.ToString());
            if (lib == null)
                return false;
            var book = await _bookRepository.FindById(bookId.ToString());
            if (book == null)
                return false;
            var res = await _relationRepository.Delete(id.ToString(), bookId.ToString());
            return res;
        }

    }
}