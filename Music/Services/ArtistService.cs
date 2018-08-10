using AutoMapper;
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
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Albums> GetAlbumsById(Guid id)
        {
            var response = await GetMusicBrainzReleasesByArtistId(id);
            var content = await ReadResponseContent(response);
            return ConvertJsonStringToObject(content);
        }

        public async Task<AlbumListModel> GetFirst10AlbumsById(Guid id)
        {
            var albums = await GetAlbumsById(id);

            if (albums.Releases.Count > 10)
            {
                albums.Releases = albums.Releases.OrderBy(r => r.Date).Take(10).ToList();
            }

            var album = _mapper.Map<AlbumListModel>(albums);

            return album;
        }

        private Albums ConvertJsonStringToObject(string content)
        {
            return JsonConvert.DeserializeObject<Albums>(content);
        }

        private async Task<string> ReadResponseContent(HttpResponseMessage response)
        {
            using (var content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        private async Task<HttpResponseMessage> GetMusicBrainzReleasesByArtistId(Guid id)
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
                .Where(x => x.Name.ToLower().Contains(criteria.ToLower()))
                .OrderBy(x => x.Name);

            var searchResult = _mapper.Map<IEnumerable<ArtistModel>>(artists);
            return searchResult;
        }

        public ArtistListModel Search(string criteria, int pageId, int pageSize)
        {
            var artists = GetAll()
                .Where(x => x.Name.ToLower().Contains(criteria.ToLower()))
                .OrderBy(x => x.Name).AsQueryable();

            var pagedArtist = new PagedList<Artist>(artists, pageId, pageSize);

            var searchArtists = _mapper.Map<ArtistListModel>(pagedArtist);

            return searchArtists;
        }
    }
}
