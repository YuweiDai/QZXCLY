using AutoMapper;
using QZCHY.API.Models;
using QZCHY.Core.Domain.AccountUsers;
using QZCHY.Core.Domain.Media;
using QZCHY.Core.Domain.Villages;
using QZCHY.Web.Api.Extensions;

namespace QZCHY.Web.Api.Extensions
{
    /// <summary>
    /// 实体到模型映射
    /// </summary>
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }



        public static VillageModel ToModel(this Village entity)
        {
            return entity.MapTo<Village, VillageModel>();
        }

        public static SimpleVillageGeoModel ToSimpleGeoModel(this Village entity)
        {
            return entity.MapTo<Village, SimpleVillageGeoModel>();
        }

        public static VillageGeoModel ToGeoModel(this Village entity)
        {
            return entity.MapTo<Village, VillageGeoModel>();
        }

        public static Village ToEntity(this VillageModel model)
        {
            return model.MapTo<VillageModel, Village>();
        }

        public static Village ToEntity(this VillageModel model, Village destination)
        {
            return model.MapTo(destination);
        }


        public static VillageEatModel ToModel(this VillageEat entity)
        {
            return entity.MapTo<VillageEat, VillageEatModel>();
        }

        public static EatGeoModel ToGeoModel(this VillageEat entity)
        {
            return entity.MapTo<VillageEat, EatGeoModel>();
        }

        public static VillageEat ToEntity(this VillageEatModel model)
        {
            return model.MapTo<VillageEatModel, VillageEat>();
        }

        public static VillageEat ToEntity(this VillageEatModel model, VillageEat destination)
        {
            return model.MapTo(destination);
        }

        public static SimplePlayModel ToSimpleModel(this VillagePlay entity)
        {
            return entity.MapTo<VillagePlay, SimplePlayModel>();
        }

        public static SimpleEatModel ToSimpleModel(this VillageEat entity)
        {
            return entity.MapTo<VillageEat, SimpleEatModel>();
        }

        public static SimpleLiveModel ToSimpleModel(this VillageLive entity)
        {
            return entity.MapTo<VillageLive, SimpleLiveModel>();
        }

        //
        public static VillageLiveModel ToModel(this VillageLive entity)
        {
            return entity.MapTo<VillageLive, VillageLiveModel>();
        }

        public static LiveGeoModel ToGeoModel(this VillageLive entity)
        {
            return entity.MapTo<VillageLive, LiveGeoModel>();
        }

        public static VillageLive ToEntity(this VillageLiveModel model)
        {
            return model.MapTo<VillageLiveModel, VillageLive>();
        }

        public static VillageLive ToEntity(this VillageLiveModel model, VillageLive destination)
        {
            return model.MapTo(destination);
        }

        //
        public static VillagePlayModel ToModel(this VillagePlay entity)
        {
            return entity.MapTo<VillagePlay, VillagePlayModel>();
        }

        public static PlayGeoModel ToGeoModel(this VillagePlay entity)
        {
            return entity.MapTo<VillagePlay, PlayGeoModel>();
        }

        public static VillagePlay ToEntity(this VillagePlayModel model)
        {
            return model.MapTo<VillagePlayModel, VillagePlay>();
        }

        public static VillagePlay ToEntity(this VillagePlayModel model, VillagePlay destination)
        {
            return model.MapTo(destination);
        }

        //
        public static VillageServiceModel ToModel(this VillageService entity)
        {
            return entity.MapTo<VillageService, VillageServiceModel>();
        }

        public static ServiceGeoModel ToGeoModel(this VillageService entity)
        {
            return entity.MapTo<VillageService, ServiceGeoModel>();
        }

        public static VillageService ToEntity(this VillageServiceModel model)
        {
            return model.MapTo<VillageServiceModel, VillageService>();
        }

        public static VillageService ToEntity(this VillageServiceModel model, VillageService destination)
        {
            return model.MapTo(destination);
        }
    

        //
        public static VillagePictureModel ToModel(this VillagePicture entity)
        {
            return entity.MapTo<VillagePicture, VillagePictureModel>();
        }

        public static VillagePicture ToEntity(this VillagePictureModel model)
        {
            return model.MapTo<VillagePictureModel, VillagePicture>();
        }

        public static VillagePicture ToEntity(this VillagePictureModel model, VillagePicture destination)
        {
            return model.MapTo(destination);
        }

        //
        public static EatPictureModel ToModel(this EatPicture entity)
        {
            return entity.MapTo<EatPicture, EatPictureModel>();
        }

        public static EatPicture ToEntity(this EatPictureModel model)
        {
            return model.MapTo<EatPictureModel, EatPicture>();
        }

        public static EatPicture ToEntity(this EatPictureModel model, EatPicture destination)
        {
            return model.MapTo(destination);
        }

        //
        public static PlayPictureModel ToModel(this PlayPicture entity)
        {
            return entity.MapTo<PlayPicture, PlayPictureModel>();
        }

        public static PlayPicture ToEntity(this PlayPictureModel model)
        {
            return model.MapTo<PlayPictureModel, PlayPicture>();
        }

        public static PlayPicture ToEntity(this PlayPictureModel model, PlayPicture destination)
        {
            return model.MapTo(destination);
        }

        //
        public static LivePictureModel ToModel(this LivePicture entity)
        {
            return entity.MapTo<LivePicture, LivePictureModel>();
        }

        public static LivePicture ToEntity(this LivePictureModel model)
        {
            return model.MapTo<LivePictureModel, LivePicture>();
        }

        public static LivePicture ToEntity(this LivePictureModel model, LivePicture destination)
        {
            return model.MapTo(destination);
        }

        //
        public static ServicePictureModel ToModel(this ServicePicture entity)
        {
            return entity.MapTo<ServicePicture, ServicePictureModel>();
        }

        public static ServicePicture ToEntity(this ServicePictureModel model)
        {
            return model.MapTo<ServicePictureModel, ServicePicture>();
        }

        public static ServicePicture ToEntity(this ServicePictureModel model, ServicePicture destination)
        {
            return model.MapTo(destination);
        }



        public static StrategyModel ToModel(this Strategy entity)
        {
            return entity.MapTo<Strategy, StrategyModel>();
        }

        public static Strategy ToEntity(this StrategyModel model)
        {
            return model.MapTo<StrategyModel, Strategy>();
        }

        public static Strategy ToEntity(this StrategyModel model, Strategy destination)
        {
            return model.MapTo(destination);
        }

    }
}