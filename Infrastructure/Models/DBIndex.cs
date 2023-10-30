using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Models
{
    public class DBIndex : DbContext
    {
        public DBIndex(DbContextOptions<DBIndex> options) : base(options) { }
        public DbSet<LibraryDBO> Libraries { get; set; }
        public DbSet<LibraryInfo> LibraryInfos { get; set; }
        public DbSet<BookLibrary> BookLibraries { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
            optionsBuilder.EnableSensitiveDataLogging();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LibraryDBO>()
               .HasOne(l => l.LibraryInfo)
               .WithOne(li => li.Library)
               .HasForeignKey<LibraryInfo>(li => li.LibraryId);
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);
            modelBuilder.Entity<BookLibrary>()
                .HasKey(bl => new { bl.BookId, bl.LibraryId });
            modelBuilder.Entity<BookLibrary>()
                .HasOne(bl => bl.Book)
                .WithMany(b=> b.BookLibrary)
                .HasForeignKey(bl => bl.BookId);
            modelBuilder.Entity<BookLibrary>()
                .HasOne(bl => bl.Library)
                .WithMany(l => l.BookLibrary)
                .HasForeignKey(bl => bl.LibraryId);
        }
    }
}
