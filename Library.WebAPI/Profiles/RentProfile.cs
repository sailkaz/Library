using AutoMapper;

namespace Library.WebAPI.Profiles
{
    public class RentProfile : Profile
    {
        public RentProfile() 
        {
            CreateMap<Models.RentForCreationDto, Entities.Rent>();
            CreateMap<Entities.Rent, Models.RentDto>();
        }       
    }
}
