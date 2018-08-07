using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Models
{
    public class ArtistModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string[] Aliases { get; set; }
    }
}
