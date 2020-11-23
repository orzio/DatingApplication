using AutoMapper;
using DatingApp.api.Data;
using DatingApp.api.Dto;
using DatingApp.api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.api.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForListDTO>()
                    .ForMember(dest => dest.PhotoUrl, opt =>
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                    .ForMember(dest => dest.Age, opt =>
                opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForDetailedDTO>()
                    .ForMember(dest => dest.photoUrl, opt =>
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                    .ForMember(dest => dest.Age, opt =>
                opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoForDetailedDTO>();
            CreateMap<UserForUpdateDTO, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
        }
    }
}
