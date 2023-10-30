using AutoMapper;
using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using Library.Infrastructure.Models;
using Library.Libs;
using Microsoft.EntityFrameworkCore;


public class BookRepository : IBookRepository
{
    private readonly DBIndex _context;
    private readonly IMapper _mapper;

    public BookRepository(DBIndex context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookDomain> FindById(string id)
    {
        try
        {
            var dbo = await _context.Books.Include(b => b.Author).ToListAsync();

            var book = dbo.Where(b => b.Id == id).First();
            
            var props = _mapper.Map<BookDTO>(book);
            var domain = BookDomain.Create(props);
            return domain;
        }catch(Exception ex)
        {
            return null;
        }
        
    }

    public async Task<IEnumerable<BookDomain>> FindAll()
    {

        var dbo = await _context.Books.Include(b=> b.Author).ToListAsync();
        var dboMapper = dbo.Select(data => BookDomain.Create(_mapper.Map<BookDTO>(data))).ToList();
        return dboMapper;
    }

    public async Task<bool> Store(RequestLibs.AddNewBook req)
    {
        try
        {
            var bookDomain = BookDomain.Create(new BookDTO
            {
                Title= req.Title,
                AuthorId = req.AuthorId.ToString(),
            });
            Book book = new Book
            {
                Id = bookDomain.Id,
                Title = req.Title,
                AuthorId = req.AuthorId.ToString(),
            };
                
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(RequestLibs.UpdateBook req)
    {
        try
        {
            var existing = await _context.Books.FirstOrDefaultAsync(x => x.Id == req.Id.ToString());
            if (existing != null)
            {
                existing.Title = req.Title;
                existing.AuthorId = req.AuthorId.ToString();
                _context.Books.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            else { throw new Exception(); }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Delete(string id)
    {
        try
        {
            var dbo = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (dbo != null)
                _context.Books.Remove(dbo);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
