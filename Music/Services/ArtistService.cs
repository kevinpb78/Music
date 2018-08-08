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
        private readonly IArtistRepository _repository;

        public ArtistService(IArtistRepository repository)
        {
            _repository = repository;
        }

        public async Task<RootDTO> GetAlbumsById(Guid id)
        {
            var response = await GetMusicBrainzReleasesByArtistId(id);
            var content = await ReadResponseContent(response);
            var root = ConvertJsonStringToObject(content);
            return root;
        }

        public async Task<RootDTO> GetFirst10AlbumsById(Guid id)
        {
            var albums = await GetAlbumsById(id);

            if (albums.Releases.Count > 10)
            {
                albums.Releases = albums.Releases.OrderBy(r => r.Date).Take(10).ToList();
            }

            return albums;
        }

        private RootDTO ConvertJsonStringToObject(string content)
        {
            return JsonConvert.DeserializeObject<RootDTO>(content);
        }

        private async Task<string> ReadResponseContent(HttpResponseMessage response)
        {
            using (var content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        public async Task<HttpResponseMessage> GetMusicBrainzReleasesByArtistId(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri("http://musicbrainz.org/ws/2/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("User-Agent", "C# App");

                var uri = $"release/?query=arid:{id}&fmt=json";

                return await client.GetAsync(uri);
            }
        }

        public IEnumerable<Artist> GetAll()
        {
            return _repository.GetAll();
        }

        public Artist GetById(Guid id)
        {
                return _repository.GetById(id);
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
