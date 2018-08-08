using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Music.Controllers;
using Music.Data;
using Music.Services;
using Xunit;

namespace Music.Test
{
    public class ArtistControllerTest
    {
        private readonly Mapper _mapper;
        private readonly IArtistService _service;
        private readonly ArtistController _controller;
        private readonly IConfigurationProvider _configuration;
        private readonly MusicContext _context;
        private readonly IArtistRepository _repository;

        public ArtistControllerTest()
        {
            _mapper = new Mapper(_configuration);
            _service = new ArtistService(_repository);
            _controller = new ArtistController(_service, _mapper);
        }

        [Fact]
        public void Search_WhenCalled_ReturnsListArtists()
        {
            //Arrange
            //var controller = new ArtistController(new ArtistService(new MusicContext(new DbContextOptions<MusicContext>())), new Mapper(_configuration));
            var criteria = "john";

            //Act
            var okResult = _controller.Search(criteria) as OkObjectResult;

            //Assert


        }
    }
}
