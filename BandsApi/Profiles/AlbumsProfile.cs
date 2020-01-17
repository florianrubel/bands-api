using AutoMapper;
using BandsApi.Helpers;

namespace BandsApi.Profiles
{
    public class AlbumsProfile : Profile
    {
        public AlbumsProfile()
        {
            CreateMap<Entities.Album, Models.AlbumDto>().ReverseMap();
        }
    }
}
