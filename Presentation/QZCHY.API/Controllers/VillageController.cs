using QZCHY.API.Models;
using QZCHY.Services.Geo;
using QZCHY.Services.Media;
using QZCHY.Services.Villages;
using QZCHY.Web.Api.Extensions;
using QZCHY.Web.Framework.Controllers;
using QZCHY.Web.Framework.Response;
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
            foreach (var picture in village.VillagePictures.Take(3))
            {
                var vpm = new VillagePictureModel
                {
                    Src = _pictureService.GetPictureUrl(picture.Picture)
                };
                villageModel.VillagePictures.Add(vpm);
            }

            //处理攻略
            foreach (var strategyModel in villageModel.Strategies)
            {
                var strategy = village.Strategies.Where(s => s.Id == strategyModel.Id).SingleOrDefault();
                var strategyLogoPicture = strategy.StrategyPictures.FirstOrDefault();

                strategyModel.Img = _pictureService.GetPictureUrl(strategyLogoPicture.Picture);
            }

            villageModel.Eats = null;
            villageModel.Plays = null;
            villageModel.Lives = null;

            return Ok(villageModel);
        }

        [HttpGet]
        [Route("{Id}/EatList")]
        public IHttpActionResult GetEatList(int id)
        {
            var village = _villageService.GetVillageById(id);
            if (village == null) return NotFound();

            var eatModelList = new List<SimpleEatModel>();
            foreach (var eat in village.Eats.Where(e => !e.Deleted))
            {
                var eatModel = eat.ToSimpleModel();
                var eatLogoPicture = _villageEatService.GetEatLogoPictureById(eat.Id);
                if (eatLogoPicture != null)
                    eatModel.Logo = _pictureService.GetPictureUrl(eatLogoPicture.Picture);

                eatModel.Panorama = village.Panorama;
                eatModelList.Add(eatModel);
            }

            return Ok(eatModelList);
        }

        [HttpGet]
        [Route("{Id}/PlayList")]
        public IHttpActionResult GetPlayList(int id)
        {
            var village = _villageService.GetVillageById(id);
            if (village == null) return NotFound();

            var playModelList = new List<SimplePlayModel>();
            foreach (var play in village.Plays.Where(e => !e.Deleted))
            {
                var playModel = play.ToSimpleModel();
                var playLogoPicture = _villagePlayService.GetPlayLogoPictureById(play.Id);
                if (playLogoPicture != null)
                    playModel.Logo = _pictureService.GetPictureUrl(playLogoPicture.Picture);

                playModel.Panorama = village.Panorama;
                playModelList.Add(playModel);
            }

            return Ok(playModelList);
        }

        [HttpGet]
        [Route("{Id}/LiveList")]
        public IHttpActionResult GetLiveList(int id)
        {
            var village = _villageService.GetVillageById(id);
            if (village == null) return NotFound();

            var liveModelList = new List<SimpleLiveModel>();
            foreach (var live in village.Lives.Where(e => !e.Deleted))
            {
                var liveModel = live.ToSimpleModel();
                var liveLogoPicture = _villageLiveService.GetLiveLogoPictureById(live.Id);
                if (liveLogoPicture != null)
                    liveModel.Logo = _pictureService.GetPictureUrl(liveLogoPicture.Picture);

                liveModel.Panorama = village.Panorama;
                liveModelList.Add(liveModel);
            }

            return Ok(liveModelList);
        }

        [HttpGet]
        [Route("{Id}/Pictures")]
        public IHttpActionResult GetPictures(int id)
        {
            var village = _villageService.GetVillageById(id);
            if (village == null) return NotFound();

            var images = new List<VillagePictureModel>();

            foreach(var villagePicture in village.VillagePictures)
            {
                var model = new VillagePictureModel
                {
                    Src = _pictureService.GetPictureUrl(villagePicture.Picture)
                };

                images.Add(model);
            }

            return Ok(images);
        }

        [HttpGet]
        [Route("Region/{regionId}")]
        public IHttpActionResult GetVillagesByRegionId(int regionId)
        {

            var villages = _villageService.GetVillagesByRegionId(regionId);

            var simpleVillages = new List<VillageSimpleModel>();
            foreach (var village in villages) {

                var simpleVillage = village.ToSimpleModel();
                var logePicture = village.VillagePictures.Where(p => p.IsLogo).FirstOrDefault();
                simpleVillage.Logo = _pictureService.GetPictureUrl(logePicture.Picture);
                simpleVillages.Add(simpleVillage);
            }

            return Ok(simpleVillages);
        }

        [HttpGet]
        [Route("Eat/{Id}")]
        public IHttpActionResult GetVillageEatById(int id = 0) {

            var eat = _villageEatService.GetVillageEatById(id);

            if (eat == null || eat.Deleted) return NotFound();

            var eatModel = eat.ToModel();
            if (eatModel.Panoramaid != 0)
                eatModel.Panorama = id > 16 ? "ch" : "dp";// eat.Village.Panorama;
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
            if (live == null || live.Deleted) return NotFound();

            var liveModel = live.ToModel();
            if (liveModel.PanoramaId != 0)
                liveModel.Panorama = id > 13 ? "ch" : "dp"; // live.Village.Panorama;

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
            if (play == null || play.Deleted) return NotFound();

            var playModel = play.ToModel();
            if (playModel.PanoramaId != 0)
                playModel.Panorama = id > 16 ? "ch" : "dp"; // play.Village.Panorama;
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

            villageModel.Distance = _geometryService.CalculateDistance(lon, lat, villageModel.Longitude, villageModel.Latitude).ToString();

            //遍历各个节点获取logo
            foreach (var service in villageModel.Services)
            {
                var serviceLogoPicture = _villageServiceService.GetServiceLogoPictureById(service.Id);
                if (serviceLogoPicture != null)
                    service.Logo = _pictureService.GetPictureUrl(serviceLogoPicture.Picture);                
            }

            var eats = village.Eats.Where(e => !e.Deleted);
            villageModel.Eats.Clear();
            foreach (var eat in eats)
            {
                var eatModel = eat.ToGeoModel();
                var eatLogoPicture = _villageEatService.GetEatLogoPictureById(eat.Id);
                if (eatLogoPicture != null)
                    eatModel.Logo = _pictureService.GetPictureUrl(eatLogoPicture.Picture);

                villageModel.Eats.Add(eatModel);
            }

            var plays = village.Plays.Where(p => !p.Deleted);
            villageModel.Plays.Clear();
            foreach (var play in plays)
            {
                var playModel = play.ToGeoModel();
                var playLogoPicture = _villagePlayService.GetPlayLogoPictureById(play.Id);
                if (playLogoPicture != null)
                    playModel.Logo = _pictureService.GetPictureUrl(playLogoPicture.Picture);

                villageModel.Plays.Add(playModel);
            }

            var lives = village.Lives.Where(l => !l.Deleted);
            villageModel.Lives.Clear();
            foreach (var live in lives)
            {
                var liveModel = live.ToGeoModel();
                var liveLogoPicture = _villageLiveService.GetLiveLogoPictureById(live.Id);
                if (liveLogoPicture != null)
                    liveModel.Logo = _pictureService.GetPictureUrl(liveLogoPicture.Picture);

                villageModel.Lives.Add(liveModel);
            }

            return Ok(villageModel);
        }

        [HttpGet]
        [Route("MonthVillage")]
        public IHttpActionResult GetMonthVillage()
        {

            var month = DateTime.Now.Month;
            var villages = _villageService.GetVillagesByMonth(month);

            var MonthVillages = new List<VillageSimpleModel>();
            foreach (var village in villages) {
                var logoPicture = village.VillagePictures.Where(vp => vp.IsLogo).FirstOrDefault();
                var monthVillage = village.ToSimpleModel();
                monthVillage.Logo = _pictureService.GetPictureUrl(logoPicture.Picture);
                MonthVillages.Add(monthVillage);
            }
        

            return Ok(MonthVillages);
        }

        [HttpGet]
        [Route("NearVillage")]
        public IHttpActionResult GetNearVillage(double lat,double lon) {

            var villages = _villageService.GetAllVillages();
            var id = 0;
            var villageModels = new List<VillageSimpleModel>();

         
          
            foreach (var village in villages) {
                
                var l = _geometryService.CalculateDistance(lon,lat,Convert.ToDouble( village.Location.Longitude),Convert.ToDouble( village.Location.Latitude));
                var villageModel = village.ToSimpleModel();
                if (l <= 5) {
                    villageModel.Long = l;
                    villageModels.Add(villageModel);
                }

            }

            if (villageModels.Count() > 0) {
                var min = villageModels[0].Long;
                for (int i = 1; i < villageModels.Count(); i++)
                {
                    if (min > villageModels[i].Long)
                    {
                        min = villageModels[i].Long;
                        id = villageModels[i].Id;
                    }
                }
            }


            return Ok(id);

        }

        [HttpGet]
        [Route("ListVillage")]
        public IHttpActionResult GetHotVillage(int pageSize = Int32.MaxValue, int pageIndex = 0)
        {
            var month = DateTime.Now.Month;
            var villages = _villageService.GetHotVillages(month);

            var response = new ListResponse<HotVillageListModel>
            {
                Paging = new Paging
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Total = villages.Count,
                 //   FilterCount = string.IsNullOrEmpty(query) ? properties.TotalCount : properties.Count,
                },
                Data = villages.Select(s =>
                {
                    var villageModel = s.ToHotListModel();              
                    return villageModel;
                })
            };

            return Ok(response);
        }



        [HttpGet]
        [Route("PlayPicture")]
        public IHttpActionResult GetPictureUrl()
        {
          //  var village = _villageService.GetVillageById(1);


            var play = _villagePlayService.GetVillagePlayById(1);
            var village = play.Village.ToModel();

            //var plays = _villagePlayService.GetVillagePlayByVillageId(2);
            //var logos =new  List<string>();
            //foreach (var play in plays)
            //{
            //    var playPicture = play.PlayPictures.Where(p => p.IsLogo).FirstOrDefault();
            //    if (playPicture != null) {
            //        var logo = _pictureService.GetPictureUrl(playPicture.Picture);
            //        logos.Add(play.Name+ logo);
            //    } 

            //}





            return Ok(village);

        }


    }
}