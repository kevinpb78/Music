using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.DTO
{
    public class AlbumDTO
    {
        public Guid ReleaseId { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public string Label { get; set; }

        public int NumberOfTracks { get; set; }

        public IEnumerable<OtherArtistDTO> OtherArtists { get; set; }
    }
}
