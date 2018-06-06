using PortoGO.DB.Domain;
using PortoGO.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGO.Web
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
                cfg.CreateMap<PointOfInterest, PoiViewModel>()
                    .ReverseMap();
                cfg.CreateMap<BusinessHours, BusinessHoursViewModel>().ReverseMap();
                cfg.CreateMap<Hashtag, HashtagViewModel>().ReverseMap();
                cfg.CreateMap<Visit, VisitViewModel>().ReverseMap();
                cfg.CreateMap<GpsCoordinate, GpsCoordinatesViewModel>().ReverseMap();

                cfg.CreateMap<CreatePoiViewModel, PointOfInterest>();

                cfg.CreateMap<PointOfInterest, AddPoiToVisitViewModel>();

                cfg.CreateMap<Route, RouteViewModel>().ReverseMap();
            });
        }
    }
}