using AutoMapper;

namespace RealEstateAPI.Profiles
{
    public class HousesProfile : Profile
    {
        public HousesProfile()
        {
            CreateMap<Models.Domain.House, Models.DTO.House>()
            .ReverseMap();

            CreateMap<Models.Domain.Landscape, Models.DTO.Landscape>()
                .ReverseMap();
        }
    }
}
