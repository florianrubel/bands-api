using AutoMapper;
using BandsApi.Entities;
using BandsApi.Models;

namespace BandsApi.Profiles
{
    public class AlbumsProfile : Profile
    {
        public AlbumsProfile()
        {
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<AlbumForCreatingDto, Album>();
        }
    }
}
