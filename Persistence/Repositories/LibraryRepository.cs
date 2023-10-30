using AutoMapper;
using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using Library.Infrastructure.Models;
using Library.Libs;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly DBIndex _context;
        private readonly IMapper _mapper;
        public LibraryRepository(DBIndex context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<LibraryDomain> FindById(string id)
        {
            try
            {
                var dbo = await _context.Libraries.Include(l => l.LibraryInfo).ToListAsync();

                var lib = dbo.Where(l =>l.Id == id).First();

                var props = _mapper.Map<LibraryDTO>(lib);
                var domain = LibraryDomain.Create(props);
                return domain;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<LibraryDomain>> FindAll()
        {
            var dbo = await _context.Libraries.Include(l=> l.LibraryInfo).ToListAsync();
            var dboMapper = dbo.Select(data => LibraryDomain.Create(_mapper.Map<LibraryDTO>(data))).ToList();
            return dboMapper;
        }
        public async Task<bool> Store(RequestLibs.AddNewLibrary req)
        {
            try
            {
                var domainLib = LibraryDomain.Create(new LibraryDTO
                {
                    Name = req.Name,
                });
                
                var domainLibInfo = LibraryInfoDomain.Create(new LibraryInfoDTO
                {
                    Address = req.LibraryInfo.address,
                    HoursOfOperation = req.LibraryInfo.hoursOfOperation,
                });
                LibraryDBO dboLib = new LibraryDBO
                {
                    Id = domainLib.Id,
                    Name = req.Name,
                    LibraryInfoId = domainLibInfo.Id,
                };
                LibraryInfo dboLibInfo = new LibraryInfo
                {
                    Id = domainLibInfo.Id,
                    Address = req.LibraryInfo.address,
                    LibraryId =domainLib.Id,
                    HoursOfOperation = req.LibraryInfo.hoursOfOperation,
                };
                await _context.Libraries.AddAsync(dboLib);
                await _context.LibraryInfos.AddAsync(dboLibInfo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Update(string id, RequestLibs.UpdateLibrary req)
        {
            try
            {

                var existing = await _context.Libraries.Include(l=>l.LibraryInfo).FirstOrDefaultAsync(x => x.Id == id);
                if (existing != null)
                {
                    existing.Name = req.Name;
                    existing.LibraryInfo.Address = req.LibraryInfo.address;
                    existing.LibraryInfo.HoursOfOperation = req.LibraryInfo.hoursOfOperation;
                    _context.Libraries.Update(existing);
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
                var dbo = await _context.Libraries.FirstOrDefaultAsync(x => x.Id == id);
                if (dbo != null)
                    _context.Libraries.Remove(dbo);
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
