using Music.Data;
using Music.DTO;
using Music.Models;
using Music.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Music.Services
{
    public class ArtistService : IArtistService
    {
        private MusicContext _context;

        public ArtistService(MusicContext context)
        {
            _context = context;
        }

        public async Task<RootDTO> GetAlbumsById(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("User-Agent", "C# App");

                string uri = $"?query=arid:{id}&fmt=json";
                var url = new Uri($"http://musicbrainz.org/ws/2/release/" + uri);
                var response = await client.GetAsync(url);
                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                var root = JsonConvert.DeserializeObject<RootDTO>(json);

                return root;
            }
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

        public IEnumerable<ArtistModel> Search(string criteria)
        {
            var artists = GetAll()
                .Where(x => x.Name.ToLower().Contains(criteria.ToLower()));

            var listResult = artists
                .Select(x => new ArtistModel
                {
                    Name = x.Name,
                    Country = x.Country,
                    Aliases = x.Aliases.Split(',')
                });

            return listResult;
        }

        public async Task<IEnumerable<Artist>> SearchAsync(string criteria, int pageId, int pageSize)
        {
            var artists = GetAll()
                .Where(x => x.Name.ToLower().Contains(criteria.ToLower()));

            return await PaginatedList<Artist>.CreateAsync(artists.AsQueryable(), pageId, pageSize);
        }
    }
}
