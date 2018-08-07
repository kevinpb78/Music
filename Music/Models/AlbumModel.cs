using Music.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Models
{
    public class AlbumModel
    {
        public Guid ReleaseId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Label { get; set; }
        public string NumberOfTracks { get; set; }
        public List<OtherArtist> OtherArtists { get; set; }
    }
}
