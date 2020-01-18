using AutoMapper;
using BandsApi.Helpers;

namespace BandsApi.Profiles
{
    public class BandsProfile : Profile
    {
        public BandsProfile()
        {
            CreateMap<Entities.Band, Models.BandDto>()
                .ForMember(
                    dest => dest.FoundedYearsAgo,
                    opt => opt.MapFrom(src => $"{src.Founded.ToString("yyyy")} ({src.Founded.GetYearsAgo()} years ago)")
                );
            CreateMap<Models.BandForCreatingDto, Entities.Band>();
        }
    }
}
