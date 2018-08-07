using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Music.DTO;
using Music.Models;

namespace Music.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArtistCreditDTO, OtherArtist>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.OtherArtist.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(scr => scr.OtherArtist.Name));

            CreateMap<ReleaseDTO, AlbumModel>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.LabelInfo.FirstOrDefault().Label.Name))
                .ForMember(dest => dest.OtherArtists, opt => opt.MapFrom(src => src.ArtistCredit
                    .Select(ac => ac.OtherArtist)));

            CreateMap<RootDTO, AlbumListModel>();
        }
    }
}
