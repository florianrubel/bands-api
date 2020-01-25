using AutoMapper;
using BandsApi.Helpers;
using BandsApi.Models;
using BandsApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BandsApi.Controllers
{
    [ApiController]
    [Route("api/band-collections")]
    public class BandCollectionsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandCollectionsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ??
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetBandCollection")]
        public IActionResult GetBandCollection([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            IEnumerable<Entities.Band> bandEntitites = _bandAlbumRepository.GetBands(ids);

            if (ids.Count() != bandEntitites.Count())
            {
                return NotFound();
            }

            IEnumerable<BandDto> bandsToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntitites);

            return Ok(bandsToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<BandDto>> CreateBandCollection([FromBody]IEnumerable<BandForCreatingDto> bandCollection)
        {
            IEnumerable<Entities.Band> bandEntities = _mapper.Map<IEnumerable<Entities.Band>>(bandCollection);
            foreach (Entities.Band band in bandEntities)
            {
                _bandAlbumRepository.AddBand(band);
            }
            _bandAlbumRepository.Save();

            IEnumerable<BandDto> bandCollectionToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);
            string idsString = string.Join(",", bandCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetBandCollection", new { ids = idsString }, bandCollectionToReturn);
        }
    }
}
