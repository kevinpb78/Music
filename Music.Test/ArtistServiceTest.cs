using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Controllers;
using Music.Data;
using Music.Services;
using System;
using Xunit;


namespace Music.Test
{
    public class ArtistServiceTest
    {
        private readonly IArtistRepository _repository;
        private readonly IArtistService _service;

        public ArtistServiceTest(IArtistRepository repository, IArtistService service)
        {
            _repository = repository;
            _service = service;
        }
        
        [Fact]
        public void GetReleasesByArtistId_WhenCalled_ReturnsListArtists()
        {
            //Arrange
            var id = new Guid("144ef525-85e9-40c3-8335-02c32d0861f3");

            //Act
            //var result = _service.GetMusicBrainzReleasesByArtistId(id).Result;

            //Assert


        }
    }
}
