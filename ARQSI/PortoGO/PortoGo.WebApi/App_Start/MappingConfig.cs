using PortoGo.WebApi.Models;
using PortoGO.DB.Domain;
using System;

namespace PortoGo.WebApi
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Location, LocationViewModel>()
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Coordinates.Latitude))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Coordinates.Longitude))
                    .ForMember(dest => dest.Altitude, opt => opt.MapFrom(src => src.Coordinates.Altitude))
                    .ReverseMap();

                cfg.CreateMap<PointOfInterest, PoiViewModel>().ReverseMap();
                cfg.CreateMap<CreateRouteViewModel, Route>();

                //cfg.CreateMap<Road, RoadViewModel>()
                //    .ForMember(dest => dest., opt => opt.MapFrom(src => new LocationViewModel
                //    {
                //        Longitude = src.FromCoordinates.Longitude,
                //        Latitude = src.FromCoordinates.Latitude,
                //        Altitude = src.FromCoordinates.Altitude
                //    }))
                //    .ForMember(dest => dest.ToLocation, opt => opt.MapFrom(src => new LocationViewModel
                //    {
                //        Longitude = src.ToCoordinates.Longitude,
                //        Latitude = src.ToCoordinates.Latitude,
                //        Altitude = src.ToCoordinates.Altitude
                //    }))
                //    .ReverseMap();
                cfg.CreateMap<Road, RoadViewModel>().ReverseMap();

                cfg.CreateMap<GpsCoordinate, GpsCoordinatesViewModel>().ReverseMap();
                cfg.CreateMap<Hashtag, HashtagViewModel>().ReverseMap();
                cfg.CreateMap<Visit, VisitViewModel>().ReverseMap();
                cfg.CreateMap<Route, RouteViewModel>().ReverseMap();
                cfg.CreateMap<User, UserInfoViewModel>()
                    .ForMember(dest => dest.Roles, opt => opt.Ignore())
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            });
        }
    }
}
