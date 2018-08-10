using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Music.Controllers;
using Music.Data;
using Music.DTO;
using Music.Models;
using Music.Services;
using System;
using System.Collections.Generic;
using Xunit;


namespace Music.Test
{
    public class ArtistServiceTest
    {
        [Fact]
        public void GetReleasesByArtistId_WhenCalled_ReturnsListArtists()
        {
            try
            {
                //Arrange
                var id = Guid.Parse("65f4f0c5-ef9e-490c-aee3-909e7ae6b2ab");
                Mock<IArtistRepository> mockRepository = new Mock<IArtistRepository>();
                var albumListModel = new AlbumListModel();
                var artist = new Artist() { Id = id, Name = "Artist1", Country = "US", Aliases = "" };

                mockRepository.Setup(m => m.GetById(id)).Returns(value: artist);

                var mockMapper = new Mock<IMapper>();
                mockMapper.Setup(x => x.Map<Albums, AlbumListModel>(It.IsAny<Albums>()))
                    .Returns(albumListModel);

                ArtistService service = new ArtistService(mockRepository.Object, mockMapper.Object);

                //Act
                var result = service.GetAlbumsById(id).Result;

                //Assert
                Assert.NotNull(result);


            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }
    }
}
