using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Libs;
using Library.Infrastructure.Models;

namespace Library.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<IBook>> GetAll()
        {
            var books = await _bookRepository.FindAll();
            var res = books.Select(data => data.Unmarshal());
            return res;
        }

        public async Task<IBook> GetById(Guid id)
        {
            var book = await _bookRepository.FindById(id.ToString());
            if(book == null)
            {
                return null; 
            }
            var res = book.Unmarshal();
            return res;
        }

        public async Task<bool> Add(RequestLibs.AddNewBook req)
        {
            var author = await _authorRepository.FindById(req.AuthorId.ToString());
            if (author == null)
                return false;
            var book = await _bookRepository.Store(req);
            if (!book)
                return false;
            return true;
        }

        public async Task<bool> Update(RequestLibs.UpdateBook req)
        {
            var author = await _authorRepository.FindById(req.AuthorId.ToString());
            if (author == null)
                return false;
            return await _bookRepository.Update(req);
        }

        public async Task<bool> Delete(Guid id)
        {
            var book = await _bookRepository.FindById(id.ToString());
            if (book != null)
            {
                return await _bookRepository.Delete(id.ToString());
            }
            return false;
        }
    }
}