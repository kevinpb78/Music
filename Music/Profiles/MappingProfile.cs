using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Music.Data;
using Music.DTO;
using Music.Models;
using Music.Utilities;

namespace Music.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArtistCredit, Models.OtherArtist>()
                .ForMember(dest => dest.Id, (IMemberConfigurationExpression<ArtistCredit, Models.OtherArtist, string> opt) => opt.MapFrom(scr => scr.OtherArtist.Id))
                .ForMember(dest => dest.Name, (IMemberConfigurationExpression<ArtistCredit, Models.OtherArtist, string> opt) => opt.MapFrom(scr => scr.OtherArtist.Name));

            CreateMap<Release, AlbumModel>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.LabelInfo.FirstOrDefault().Label.Name))
                .ForMember(dest => dest.OtherArtists, opt => opt.MapFrom(src => src.ArtistCredit
                    .Select(ac => ac.OtherArtist)));

            CreateMap<Albums, AlbumListModel>();

            CreateMap<Artist, ArtistModel>();

            CreateMap<PagedList<Artist>, ArtistListModel>();
        }
    }
}
