using System;
using System.Linq;
using QZCHY.Core.Infrastructure;
using AutoMapper;
using QZCHY.API.Models;
using QZCHY.Core.Domain.Villages;

namespace QZCHY.Web.Api.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public int Order
        {
            get { return 0; }
        }

        public void Execute()
        {
            //对象映射     
             Mapper.CreateMap<VillageModel, Village>()
                  .ForMember(dest => dest.Location, mo => mo.Ignore())
                  .ForMember(dest => dest.Region, mo => mo.Ignore())
           .ForMember(dest => dest.GeoTourRoute, mo => mo.Ignore());

            Mapper.CreateMap<Village, VillageModel>()
                 .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Region, mo => mo.Ignore())
                 .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<Village, SimpleVillageGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<Village, VillageGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude))
                  .ForMember(dest => dest.GeoTourRoute, mo => mo.MapFrom(src => src.GeoTourRoute == null ? "" : src.GeoTourRoute.AsText()));

            Mapper.CreateMap<VillageEatModel, VillageEat>();

            Mapper.CreateMap<VillageEat, VillageEatModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<VillageEat, EatGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            //
            Mapper.CreateMap<VillagePlayModel, VillagePlay>();
            Mapper.CreateMap<VillagePlay, VillagePlayModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<VillagePlay, PlayGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            //
            Mapper.CreateMap<VillageLiveModel, VillageLive>();

            Mapper.CreateMap<VillageLive, VillageLiveModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<VillageLive, LiveGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            //
            Mapper.CreateMap<VillageServiceModel, VillageService>();

            Mapper.CreateMap<VillageService, VillageServiceModel>();

            Mapper.CreateMap<VillageService, ServiceGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            //
            Mapper.CreateMap<VillagePictureModel, VillagePicture>();

            Mapper.CreateMap<VillagePicture, VillagePictureModel>();
     
            //
            Mapper.CreateMap<EatPictureModel, EatPicture>();

            Mapper.CreateMap<EatPicture, EatPictureModel>();

            //
            Mapper.CreateMap<LivePictureModel, LivePicture>();

            Mapper.CreateMap<LivePicture, LivePictureModel>();

            //
            Mapper.CreateMap<PlayPictureModel, PlayPicture>();

            Mapper.CreateMap<PlayPicture, PlayPictureModel>();

            //
            Mapper.CreateMap<ServicePictureModel, ServicePicture>();

            Mapper.CreateMap<ServicePicture, ServicePictureModel>();



            //simple
            Mapper.CreateMap<VillagePlay, SimplePlayModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));
            Mapper.CreateMap<VillageEat, SimpleEatModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));
            Mapper.CreateMap<VillageLive, SimpleLiveModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<StrategyModel, Strategy>();

            Mapper.CreateMap<Strategy, StrategyModel>();
            Mapper.CreateMap<Village, VillageSimpleModel>();
            Mapper.CreateMap<Village, HotVillageListModel>();

        }
    }
}