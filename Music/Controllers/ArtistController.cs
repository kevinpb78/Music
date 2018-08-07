using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Models;
using Music.Services;

namespace Music.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ArtistController : Controller
    {
        private readonly MusicContext _context;
        private readonly IArtistService _service;
        private readonly IMapper _mapper;

        public ArtistController(MusicContext context, IArtistService service, IMapper mapper)
        {
            _context = context;
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Artists
        [HttpGet]
        public IEnumerable<Artist> GetAll()
        {
            return _service.GetAll();
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public IActionResult GetArtist([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = _service.GetById(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        //Get api/Artist/Search/searchparam
        [HttpGet]
        [Route("search/{criteria}")]
        public IActionResult Search([FromRoute] string criteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(String.IsNullOrEmpty(criteria))
            {
                return NotFound();
            }

            var artists = _service.Search(criteria);

            if (artists == null)
            {
                return NotFound();
            }

            return Ok(artists);
        }

        [HttpGet]
        [Route("search/{criteria}/{pageid:int}/{pagesize:int}")]
        public IActionResult Search([FromRoute] string criteria, [FromRoute] int pageid, [FromRoute] int pagesize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (String.IsNullOrEmpty(criteria))
            {
                return NotFound();
            }

            var artists = _service.Search(criteria, pageid, pagesize);

            if (artists == null)
            {
                return NotFound();
            }

            return Ok(artists);
        }

        //Get api/Artist/5/albums
        [HttpGet]
        [Route("{id}/albums")]
        public async Task<IActionResult> Albums([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artistReleases = await _service.GetAlbumsById(id);

            artistReleases.Releases = artistReleases.Releases.OrderBy(r => r.Date).Take(10).ToList();

            var albums = _mapper.Map<AlbumListModel>(artistReleases);


            if (albums == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(albums);
            }

            
        }

        // PUT: api/Artist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist([FromRoute] Guid id, [FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.Id)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Artist
        [HttpPost]
        public async Task<IActionResult> PostArtist([FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok(artist);
        }

        private bool ArtistExists(Guid id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}