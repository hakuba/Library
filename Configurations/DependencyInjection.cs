
using Library.Domain.Models;
using Library.Domain.ServiceRepository;
using Library.Infrastructure.Models;
using Library.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Library.Services;
using System.Text;
using AutoMapper;
namespace Library.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();

            services.AddDbContext<DBIndex>(opt =>
                opt.UseNpgsql(connectionString)
            );
            services.AddAutoMapper(typeof(DTOMapper));
            services.AddHttpClient();
            


            return services;
        }
        public static IServiceCollection AddScopedData(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IBookLibraryRepository, BookLibraryRepository>();
            services.AddScoped<AuthorService>();
            services.AddScoped<BookService>();
            services.AddScoped<LibraryService>();

            return services;
        }
    }
}
