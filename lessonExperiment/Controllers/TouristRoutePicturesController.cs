using AutoMapper;
using lessonExperiment.Dtos;
using lessonExperiment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using lessonExperiment.Models;

namespace lessonExperiment.Controllers
{
    [Route("api/touristRoutes/{touristRouteId}/pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private IMapper _mapper;

        public TouristRoutePicturesController(
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper
        )
        {
            _touristRouteRepository = touristRouteRepository ??
                throw new ArgumentNullException(nameof(touristRouteRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetPictureListForTouristRoute(Guid touristRouteId)
        {
            // From lecture 5.9
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游线路不存在");
            }

            var picturesFromRepo = _touristRouteRepository.GetPicturesByTouristRouteId(touristRouteId);
            if (picturesFromRepo == null || picturesFromRepo.Count() == 0)
            {
                return NotFound("照片不存在");
            }

            return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));

        }

        [HttpGet("{pictureId}", Name = "GetPicture")]
        public IActionResult GetPicture(Guid touristRouteId, int pictureId)
        {
            // From lecture 5.10
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游线路不存在");
            }

            var pictureFromRepo = _touristRouteRepository.GetPicture(pictureId);
            if (pictureFromRepo == null)
            {
                return NotFound("相片不存在");
            }
            return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
        }

        [HttpPost]
        public IActionResult CreateTouristRoutePicture(
            [FromRoute] Guid touristRouteId,
            [FromBody] TouristRoutePictureForCreationDto touristRoutePictureForCreationDto
        )
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游线路不存在");
            }

            var pictureModel = _mapper.Map<TouristRoutePicture>(touristRoutePictureForCreationDto);
            _touristRouteRepository.AddTouristRoutePicture(touristRouteId, pictureModel);
            _touristRouteRepository.Save();
            var pictureToReturn = _mapper.Map<TouristRoutePictureDto>(pictureModel);
            return CreatedAtRoute(
                "GetPicture",
                new
                {
                    touristRouteId = pictureModel.TouristRouteId,
                    pictureId = pictureModel.Id
                },
                pictureToReturn
            );
        }
    }
}
