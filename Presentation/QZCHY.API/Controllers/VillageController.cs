﻿using QZCHY.API.Models;
using QZCHY.Services.Geo;
using QZCHY.Services.Media;
using QZCHY.Services.Villages;
using QZCHY.Web.Api.Extensions;
using QZCHY.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QZCHY.API.Controllers
{
    [RoutePrefix("Villages")]
    public class VillageController : ApiController
    {

        private readonly IVillageService _villageService;
        private readonly IVillageEatService _villageEatService;
        private readonly IVillagePlayService _villagePlayService;
        private readonly IVillageLiveService _villageLiveService;
        private readonly IVillageServiceService _villageServiceService;
        private readonly IPictureService _pictureService;
        private readonly IGeometryService _geometryService;

        public VillageController(IVillageService villageService, IVillageEatService villageEatService,
            IVillagePlayService villagePlayService, IVillageLiveService villageLiveService,
            IVillageServiceService villageServiceService, IPictureService pictureService,
            IGeometryService geometryService
            )
        {
            _villageService = villageService;
            _villageEatService = villageEatService;
            _villagePlayService = villagePlayService;
            _villageLiveService = villageLiveService;
            _villageServiceService = villageServiceService;
            _pictureService = pictureService;

            _geometryService = geometryService;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetVillageById(int id = 0)
        {
            var village = _villageService.GetVillageById(id);
            if (village == null) return NotFound();

            var villageModel = village.ToModel();

            var logoPicture = village.VillagePictures.Where(vp => vp.IsLogo).FirstOrDefault();
            var routePicture = village.VillagePictures.Where(vp => vp.IsRoute).FirstOrDefault();

            if (logoPicture != null)
                villageModel.Logo = _pictureService.GetPictureUrl(logoPicture.Picture);

            if (routePicture != null)
                villageModel.RoutePicutre = _pictureService.GetPictureUrl(routePicture.Picture);

            villageModel.VillagePictures = new List<VillagePictureModel>();
            foreach (var picture in village.VillagePictures)
            {
                var vpm = new VillagePictureModel
                {
                    Src = _pictureService.GetPictureUrl(picture.Id)
                };
            }


            foreach (var eat in villageModel.Eats)
            {
                var eatLogoPicture = _villageEatService.GetEatLogoPictureById(eat.Id);
                if (eatLogoPicture != null)
                    eat.Logo = _pictureService.GetPictureUrl(eatLogoPicture.Picture);
            }

            foreach (var play in villageModel.Plays)
            {
                var playLogoPicture = _villagePlayService.GetPlayLogoPictureById(play.Id);
                if (playLogoPicture != null)
                    play.Logo = _pictureService.GetPictureUrl(playLogoPicture.Picture);
            }

            foreach (var live in villageModel.Lives)
            {
                var liveLogoPicture = _villageLiveService.GetLiveLogoPictureById(live.Id);
                if (liveLogoPicture != null)
                    live.Logo = _pictureService.GetPictureUrl(liveLogoPicture.Picture);
            }

            //处理攻略
            foreach (var strategyModel in villageModel.Strategies)
            {
                var strategy = village.Strategies.Where(s => s.Id == strategyModel.Id).SingleOrDefault();
                var strategyLogoPicture = strategy.StrategyPictures.FirstOrDefault();

                strategyModel.Img = _pictureService.GetPictureUrl(strategyLogoPicture.Picture);
            }

            return Ok(villageModel);
        }


        [HttpGet]
        [Route("Eat/{Id}")]
        public IHttpActionResult GetVillageEatById(int id = 0) {

            var eat = _villageEatService.GetVillageEatById(id);

            if (eat == null) return NotFound();
            var location = eat.Location.ToString().Split('(')[1].Substring(0, eat.Location.ToString().Split('(')[1].Length-1);

            var eatModel = eat.ToModel();
            eatModel.Lon = location.Split(' ')[0];
            eatModel.Lat = location.Split(' ')[1];
            //得到Logo
            var logoPicture = eat.EatPictures.Where(p => p.IsLogo).FirstOrDefault();
            if (logoPicture != null) eatModel.Logo = _pictureService.GetPictureUrl(logoPicture.Picture);

            //得到图片集
            var eatPictures = eat.EatPictures.Where(p => p.EatId == id ).ToList();
            var eatPicturesModel = new List<EatPictureModel>();
            foreach (var picture in eatPictures) {
                var eatPictureModel = new EatPictureModel();
                eatPictureModel.Name = "主题";
                eatPictureModel.Img = _pictureService.GetPictureUrl(picture.Picture);
                eatPicturesModel.Add(eatPictureModel);
            }

            eatModel.EatPictures = eatPicturesModel;

            return Ok(eatModel);
        }

        [HttpGet]
        [Route("Live/{Id}")]
        public IHttpActionResult GetVillageLiveById(int id = 0)
        {

            var live = _villageLiveService.GetVillageLiveById(id);
            if (live == null) return NotFound();
            var location = live.Location.ToString().Split('(')[1].Substring(0, live.Location.ToString().Split('(')[1].Length - 1);

            var liveModel = live.ToModel();
            liveModel.Lon = location.Split(' ')[0];
            liveModel.Lat = location.Split(' ')[1];
            //得到Logo
            var logoPicture = live.LivePictures.Where(p => p.IsLogo).FirstOrDefault();
            if (logoPicture != null) liveModel.Logo = _pictureService.GetPictureUrl(logoPicture.Picture);

            //得到图片集
            var livePictures = live.LivePictures.Where(p => p.LiveId == id).ToList();
            var livePicturesModel = new List<LivePictureModel>();
            foreach (var picture in livePictures)
            {
                var livePictureModel = new LivePictureModel();
                livePictureModel.Name = "主题";
                livePictureModel.Img = _pictureService.GetPictureUrl(picture.Picture);
                livePicturesModel.Add(livePictureModel);
            }

            liveModel.LivePictures = livePicturesModel;

            return Ok(liveModel);
        }


        [HttpGet]
        [Route("Play/{Id}")]
        public IHttpActionResult GetVillagePlayById(int id = 0)
        {

            var play = _villagePlayService.GetVillagePlayById(id);
            if (play == null) return NotFound();
            var location = play.Location.ToString().Split('(')[1].Substring(0, play.Location.ToString().Split('(')[1].Length - 1);

            var playModel = play.ToModel();
            playModel.Lon = location.Split(' ')[0];
            playModel.Lat = location.Split(' ')[1];
            //得到Logo
            var logoPicture = play.PlayPictures.Where(p => p.IsLogo).FirstOrDefault();
            if (logoPicture != null) playModel.Logo = _pictureService.GetPictureUrl(logoPicture.Picture);

            //得到图片集
            var playPictures = play.PlayPictures.Where(p => p.PlayId == id).ToList();
            var playPicturesModel = new List<PlayPictureModel>();
            foreach (var picture in playPictures)
            {
                var playPictureModel = new PlayPictureModel();
                playPictureModel.Name = "主题";
                playPictureModel.Img = _pictureService.GetPictureUrl(picture.Picture);
                playPicturesModel.Add(playPictureModel);
            }

            playModel.PlayPictures = playPicturesModel;

            return Ok(playModel);
        }




        [HttpGet]
        [Route("Geo")]
        public IHttpActionResult GetAllVillageInMap()
        {
            var villages = _villageService.GetAllVillages().ToList();

            var geoVillages = villages.Select(p =>
            {
                var g = p.ToSimpleGeoModel();

                return g;
            });


            return Ok(geoVillages);
        }

        [HttpGet]
        [Route("Geo/{Id}")]
        public IHttpActionResult GetVillageByIdInMap(int id = 0, double lon = 0, double lat = 0)
        {
            var village = _villageService.GetVillageById(id);
            if (village == null) return NotFound();

            var villageModel = village.ToGeoModel();

            var logoPicture = village.VillagePictures.Where(vp => vp.IsLogo).FirstOrDefault();

            if (logoPicture != null)
                villageModel.Logo = _pictureService.GetPictureUrl(logoPicture.Picture);            

            villageModel.Distance = _geometryService.CalculateDistance(lon, lat, villageModel.Longitude, villageModel.Latitude);

            //遍历各个节点获取logo
            foreach (var service in villageModel.Services)
            {
                var serviceLogoPicture = _villageServiceService.GetServiceLogoPictureById(service.Id);
                if (serviceLogoPicture != null)
                    service.Logo = _pictureService.GetPictureUrl(serviceLogoPicture.Picture);                
            }

            foreach (var eat in villageModel.Eats)
            {
                var eatLogoPicture = _villageEatService.GetEatLogoPictureById(eat.Id);
                if (eatLogoPicture != null)
                    eat.Logo = _pictureService.GetPictureUrl(eatLogoPicture.Picture);
            }

            foreach (var play in villageModel.Plays)
            {
                var playLogoPicture = _villagePlayService.GetPlayLogoPictureById(play.Id);
                if (playLogoPicture != null)
                    play.Logo = _pictureService.GetPictureUrl(playLogoPicture.Picture);
            }

            foreach (var live in villageModel.Lives)
            {
                var liveLogoPicture = _villageLiveService.GetLiveLogoPictureById(live.Id);
                if (liveLogoPicture != null)
                    live.Logo = _pictureService.GetPictureUrl(liveLogoPicture.Picture);
            }

            return Ok(villageModel);
        }


    }
}