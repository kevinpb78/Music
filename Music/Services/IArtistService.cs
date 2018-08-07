using Music.Data;
using Music.DTO;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Services
{
    public interface IArtistService
    {
        IEnumerable<Artist> GetAll();
        Artist GetById(Guid id);
        IEnumerable<ArtistModel> Search(string id);
        Task<RootDTO> GetAlbumsById(Guid id);
    }
}
