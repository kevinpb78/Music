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
        private readonly IArtistService _service;

        public ArtistController(IArtistService service)
        {
            _service = service;
        }

        // GET: api/Artists
        [HttpGet]
        public IActionResult GetAll()
        {
            var artists =  _service.GetAll();

            if (artists == null)
            {
                return NotFound();
            }

            return Ok(artists);
        }

        // GET: api/Artist/b625448e-bf4a-41c3-a421-72ad46cdb831
        [HttpGet("{id:guid}")]
        public IActionResult GetArtist([FromRoute] Guid id)
        {
            var artist = _service.GetById(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        //Get api/Artist/Search/criteria
        [HttpGet]
        [Route("search/{criteria}")]
        public IActionResult Search([FromRoute] string criteria)
        {
            var artists = _service.Search(criteria);

            if (artists == null)
            {
                return NotFound();
            }

            return Ok(artists);
        }

        [HttpGet]
        [Route("search/{criteria}/{pageid:int}/{pagesize:int}")]
        public IActionResult Search([FromRoute] string criteria, [FromRoute] int pageId, [FromRoute] int pageSize)
        {
            var artists = _service.Search(criteria, pageId, pageSize);

            if (artists == null)
            {
                return NotFound();
            }



            return Ok(artists);
        }

        //Get api/Artist/b625448e-bf4a-41c3-a421-72ad46cdb831/albums
        [HttpGet]
        [Route("{id:Guid}/albums")]
        public async Task<IActionResult> Albums([FromRoute] Guid id)
        {
            var albums = await _service.GetFirst10AlbumsById(id);

            if (albums == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(albums);
            }
        }

        
    }
}