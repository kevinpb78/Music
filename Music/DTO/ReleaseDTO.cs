using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.DTO
{
    public class ReleaseDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string ReleaseId { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public string Date { get; set; }

        [JsonProperty(PropertyName = "artist-credit")]
        public List<ArtistCreditDTO> ArtistCredit { get; set; }

        [JsonProperty(PropertyName = "label-info")]
        public List<LabelInfoDTO> LabelInfo { get; set; }

        [JsonProperty(PropertyName = "track-count")]
        public int NumberOfTracks { get; set; }
    }
}
