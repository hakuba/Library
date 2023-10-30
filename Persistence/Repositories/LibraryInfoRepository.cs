using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
    public class LibraryInfoRepository : BaseRepository<LibraryInfoDomain>
    {
        public LibraryInfoRepository(DbContext context) : base(context)
        {
        }
    }
}
