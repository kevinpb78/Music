using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.DTO
{
    public class ArtistCreditDTO
    {
        [JsonProperty(PropertyName = "artist")]
        public OtherArtistDTO OtherArtist { get; set; }
    }
}
