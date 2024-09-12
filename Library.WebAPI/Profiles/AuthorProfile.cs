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

        //private static AuthorDto AuthorToDto(this Author authorDao)
        //{
        //    return new AuthorDto
        //    { 
        //        Id = authorDao.Id,
        //        FirstName = authorDao.FirstName, 
        //        LastName = authorDao.LastName 
        //    };
        //}

        //public static IEnumerable<AuthorDto> AuthorsToDto(this IEnumerable<Author> authorsDao)
        //{
        //    if(authorsDao == null) 
        //        return Enumerable.Empty<AuthorDto>();

        //    return authorsDao.Select(x => AuthorToDto(x));
        //}

    }
}
