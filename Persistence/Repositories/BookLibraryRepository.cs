using AutoMapper;
using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using Library.Infrastructure.Models;
using Library.Libs;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
    public class BookLibraryRepository : IBookLibraryRepository
    {
        private readonly DBIndex _context;
        private readonly IMapper _mapper;
        public BookLibraryRepository(DBIndex context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BookLibraryDomain> FindById(string libId, string bookId)
        {
            try
            {
                var dbo = await _context.BookLibraries
                    .Include(rel => rel.Library).ThenInclude(lib => lib.LibraryInfo)
                    .Include(rel => rel.Book).ThenInclude(book => book.Author).ToListAsync();

                var rel = dbo.Where(rel => rel.LibraryId == libId && rel.BookId == bookId).First();

                var props = _mapper.Map<BookLibraryDTO>(rel);
                var domain = BookLibraryDomain.Create(props);
                return domain;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<BookLibraryDomain>> FindAll()
        {
            var dbo = await _context.BookLibraries
                .Include(rel=> rel.Library).ThenInclude(lib => lib.LibraryInfo)
                .Include(rel=>rel.Book).ThenInclude(book=> book.Author).ToListAsync();
            var dboMapper = dbo.Select(data => BookLibraryDomain.Create(_mapper.Map<BookLibraryDTO>(data))).ToList();
            return dboMapper;
        }
        public async Task<bool> Store(string libId, RequestLibs.AddNewBookLibrary req)
        {
            try
            {
                var domain = BookLibraryDomain.Create(new BookLibraryDTO
                {
                    LibraryId = libId,
                    BookId = req.BookId.ToString(),
                    Stock = req.Stock,
                });
                BookLibrary rel = new BookLibrary
                {
                    Id = domain.Id,
                    LibraryId = libId,
                    BookId = req.BookId.ToString(),
                    Stock = req.Stock,
                };
                await _context.BookLibraries.AddAsync(rel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Update(string libId, string bookId,RequestLibs.UpdateBookLibrary req)
        {
            try
            {
                var existing = await _context.BookLibraries.FirstOrDefaultAsync(x => x.LibraryId == libId && x.BookId == bookId);
                if (existing != null)
                {
                    existing.BookId = req.BookId.ToString(); ;
                    existing.LibraryId = req.LibraryId.ToString();
                    existing.Stock = req.Stock;
                    _context.BookLibraries.Update(existing);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else { throw new Exception(); }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(string libId, string bookId)
        {
            try
            {
                var dbo = await _context.BookLibraries.FirstOrDefaultAsync(x => x.LibraryId == libId &&  x.BookId == bookId);
                if (dbo != null)
                    _context.BookLibraries.Remove(dbo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
