using AutoMapper;

namespace RealEstateAPI.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
           .ReverseMap();
        }
    }
}
