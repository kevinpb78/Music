using Music.Data;
using Music.DTO;
using Music.Models;
using Music.Utilities;
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
        IEnumerable<ArtistModel> Search(string criteria);
        ArtistListModel Search(string criteria, int pageId, int pageSize);
        Task<Albums> GetAlbumsById(Guid id);
        Task<AlbumListModel> GetFirst10AlbumsById(Guid id);
    }
}
