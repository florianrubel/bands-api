using AutoMapper;
using BandsApi.Entities;
using BandsApi.Models;
using BandsApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BandsApi.Controllers
{
    [ApiController]
    [Route("api/bands/{bandId}/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ??
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> GetAlbumsForBand(Guid bandId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }
            IEnumerable<Entities.Album> albums = _bandAlbumRepository.GetAlbums(bandId);
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        [HttpGet("{albumId}", Name = "GetAlbumForBand")]
        public ActionResult<AlbumDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }

            Entities.Album album = _bandAlbumRepository.GetAlbum(bandId, albumId);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlbumDto>(album));
        }

        [HttpPost]
        public ActionResult<AlbumDto> CreateAlbumForBand(Guid bandId, [FromBody] AlbumForCreatingDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }

            Album albumEntity = _mapper.Map<Album>(album);
            _bandAlbumRepository.AddAlbum(bandId, albumEntity);
            _bandAlbumRepository.Save();

            AlbumDto albumToReturn = _mapper.Map<AlbumDto>(albumEntity);

            return CreatedAtRoute("GetAlbumForBand", new { bandId = bandId, albumId = albumToReturn.Id }, albumToReturn);
        }

        [HttpPut("{albumId}")]
        public ActionResult UpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] AlbumForUpdatingDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }

            Album albumEntity = _bandAlbumRepository.GetAlbum(bandId, albumId);

            if (albumEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(album, albumEntity);
            _bandAlbumRepository.UpdateAlbum(albumEntity);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        [HttpPatch("{albumId}")]
        public ActionResult PartiallyUpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] JsonPatchDocument<AlbumForUpdatingDto> patchDocument)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }

            Album albumEntity = _bandAlbumRepository.GetAlbum(bandId, albumId);

            if (albumEntity == null)
            {
                return NotFound();
            }

            AlbumForUpdatingDto albumToPatch = _mapper.Map<AlbumForUpdatingDto>(albumEntity);
            patchDocument.ApplyTo(albumToPatch, ModelState);

            if (!TryValidateModel(albumToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(albumToPatch, albumEntity);
            _bandAlbumRepository.UpdateAlbum(albumEntity);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }

            Album albumEntity = _bandAlbumRepository.GetAlbum(bandId, albumId);

            if (albumEntity == null)
            {
                return NotFound();
            }

            _bandAlbumRepository.DeleteAlbum(albumEntity);
            _bandAlbumRepository.Save();

            return NoContent();
        }
    }
}