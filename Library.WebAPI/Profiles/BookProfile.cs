using AutoMapper;

namespace Library.WebAPI.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Entities.Book, Models.BookDto>();
            CreateMap<Entities.Book, Models.BookWithoutDetailsDto>();
            CreateMap<Models.BookForCreationDto, Entities.Book>();
            CreateMap<Entities.Book, Models.BookForUpdateDto>();
            CreateMap<Models.BookForUpdateDto, Entities.Book>();
            CreateMap<Models.BookForRentDto, Entities.Book>();
        }      
    }
}
