using AutoMapper;
using lessonExperiment.Dtos;
using lessonExperiment.Models;

namespace lessonExperiment.Profiles
{
    public class TouristRoutePictureProfile : Profile
    {
        public TouristRoutePictureProfile()
        {
            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
            CreateMap<TouristRoutePictureForCreationDto, TouristRoutePicture>();
        }
    }
}
