using System;
using System.Linq;
using QZCHY.Core.Infrastructure;
using AutoMapper;
using QZCHY.Core.Domain.AccountUsers;
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
           .ForMember(dest => dest.GeoTourRoute, mo => mo.Ignore());

             Mapper.CreateMap<Village, VillageModel>()
                   .ForMember(dest => dest.Location, mo => mo.MapFrom(src => src.Location == null ? "" : src.Location.AsText()))
                   .ForMember(dest => dest.GeoTourRoute, mo => mo.MapFrom(src => src.GeoTourRoute == null ? "" : src.GeoTourRoute.AsText()));

            Mapper.CreateMap<Village, SimpleVillageGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<Village, VillageGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            Mapper.CreateMap<VillageEatModel, VillageEat>();

            Mapper.CreateMap<VillageEat, VillageEatModel>();

            Mapper.CreateMap<VillageEat, EatGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));


            //
            Mapper.CreateMap<VillagePlayModel, VillagePlay>();

            Mapper.CreateMap<VillagePlay, VillagePlayModel>();

            Mapper.CreateMap<VillagePlay, PlayGeoModel>()
                  .ForMember(dest => dest.Longitude, mo => mo.MapFrom(src => src.Location.Longitude))
                  .ForMember(dest => dest.Latitude, mo => mo.MapFrom(src => src.Location.Latitude));

            //
            Mapper.CreateMap<VillageLiveModel, VillageLive>();

            Mapper.CreateMap<VillageLive, VillageLiveModel>();

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
            Mapper.CreateMap<VillageVedioModel, VillageVedio>();

            Mapper.CreateMap<VillageVedio, VillageVedioModel>();

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




        }
    }
}