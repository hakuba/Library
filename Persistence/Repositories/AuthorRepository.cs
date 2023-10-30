using AutoMapper;
using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using Library.Infrastructure.Models;
using Library.Libs;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DBIndex _context;
        private readonly IMapper _mapper;
        public AuthorRepository(DBIndex context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AuthorDomain> FindById(string id)
        {
            var dbo = await _context.Authors.FindAsync(id);
            if (dbo == null)
            {
                return null;
            }
            var props = _mapper.Map<AuthorDTO>(dbo);
            var domain = AuthorDomain.Create(props);
            return domain;
        }
        public async Task<IEnumerable<AuthorDomain>> FindAll()
        {
            var dbo = await _context.Authors.ToListAsync();
            var dboMapper = dbo.Select(data => AuthorDomain.Create(_mapper.Map<AuthorDTO>(data))).ToList();
            return dboMapper;
        }
        public async Task<bool> Store(RequestLibs.AddNewAuthor req)
        {
            try
            {
                var domain = AuthorDomain.Create(new AuthorDTO
                {
                    Name = req.Name,
                });
                Author dbo = new Author
                {
                    Id = domain.Id,
                    Name = req.Name,
                };
                await _context.Authors.AddAsync(dbo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Update(RequestLibs.UpdateAuthor req)
        {
            try
            {
               
                var existing = await _context.Authors.FirstOrDefaultAsync(x => x.Id == req.Id.ToString());
                if( existing != null)
                {
                    existing.Name = req.Name;
                    _context.Authors.Update(existing);
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
                var dbo = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
                if(dbo != null)
                    _context.Authors.Remove(dbo);
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
