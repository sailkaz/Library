using AutoMapper;

namespace Library.WebAPI.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile() 
        {
            CreateMap<Entities.Author, Models.AuthorDto>();
            CreateMap<Entities.Author, Models.AuthorWithBooksDto>();
            CreateMap<Models.AuthorForCreationDto, Entities.Author>();
            CreateMap<Models.AuthorForUpdateDto, Entities.Author>();
        }
    }
}
