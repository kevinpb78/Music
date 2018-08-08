using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Data
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MusicContext _context;

        public ArtistRepository(MusicContext context)
        {
            _context = context;
        }

        public IEnumerable<Artist> GetAll()
        {
            return _context.Artists;
        }

        public Artist GetById(Guid id)
        {
            return _context.Artists
                .FirstOrDefault(artist => artist.Id == id);
        }

        public void Create(Artist artist)
        {
            throw new NotImplementedException();
        }

        public void Delete(Artist artist)
        {
            throw new NotImplementedException();
        }

        public void Update(Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}
