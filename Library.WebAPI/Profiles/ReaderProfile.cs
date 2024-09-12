using AutoMapper;

namespace Library.WebAPI.Profiles
{
    public class ReaderProfile : Profile
    {
        public ReaderProfile() 
        {
            CreateMap<Entities.Reader, Models.ReaderDto>();
            CreateMap<Models.ReaderForCreationDto, Entities.Reader>();
            CreateMap<Models.ReaderForUpdateDto, Entities.Reader>();
        }
    }
}
