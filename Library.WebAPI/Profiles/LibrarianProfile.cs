using AutoMapper;

namespace Library.WebAPI.Profiles
{
    public class LibrarianProfile : Profile
    {
        public LibrarianProfile() 
        {
            CreateMap<Entities.Librarian, Models.LibrarianDto>();
            CreateMap<Models.LibrarianForCreationDto, Entities.Librarian>();
            CreateMap<Models.LibrarianForUpdateDto, Entities.Librarian>();
        }
    }
}
