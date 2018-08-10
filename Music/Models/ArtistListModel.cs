using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Models
{
    public class ArtistListModel
    {
        [JsonProperty(PropertyName = "results")]
        public List<ArtistModel> Artists { get; set; }

        [JsonProperty(PropertyName = "numberOfSearchResults")]
        public int TotalItems { get; set; }

        [JsonProperty(PropertyName = "page")]
        public int PageId { get; set; }

        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "numberOfPages")]
        public int PageCount { get; set; }
    }
}
