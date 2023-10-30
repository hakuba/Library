using AutoMapper;
using Library.Domain.Models;
using Library.Infrastructure.Models;

public class DTOMapper : Profile
{
    public DTOMapper()
    {
        CreateMap<Book,BookDomain>();
        CreateMap<Book, BookDTO>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author != null ? new AuthorDTO
        {
            Id = src.Author.Id,
            Name = src.Author.Name,
        } : null));
        CreateMap<LibraryDBO, LibraryDTO>().ForMember(dest => dest.LibraryInfo, opt => opt.MapFrom(src => src.LibraryInfo != null ? new LibraryInfoDTO
        {
            Id = src.LibraryInfo.Id,
            Address = src.LibraryInfo.Address,
            HoursOfOperation = src.LibraryInfo.HoursOfOperation,
        } : null)).PreserveReferences();

        CreateMap<BookLibrary, BookLibraryDTO>()
            .ForMember(dest => dest.Library, opt => opt.MapFrom(src => src.Library != null ? new LibraryDTO
        {
            Id= src.Library.Id,
            Name = src.Library.Name,
            LibraryInfo = new LibraryInfoDTO { 
                Address = src.Library.LibraryInfo.Address,
                HoursOfOperation = src.Library.LibraryInfo.HoursOfOperation,
            },
        } : null))
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book != null ? new BookDTO
        {
            Id = src.Book.Id,
            Title = src.Book.Title,
            Author = new AuthorDTO
            {
                Name = src.Book.Author.Name,
                
            },
        } : null));
        CreateMap<Author, AuthorDTO>().ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
        CreateMap<Author, AuthorDomain>().ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
    }
}