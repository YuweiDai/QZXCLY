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
    public class VillageController: ApiController
    {

        private readonly IVillageService _villageService;
        private readonly IVillageEatService _villageEatService;
        private readonly IVillagePlayService _villagePlayService;
        private readonly IVillageLiveService _villageLiveService;
        private readonly IPictureService _pictureService;

        public VillageController(IVillageService villageService, IVillageEatService villageEatService,
            IVillagePlayService villagePlayService, IVillageLiveService villageLiveService, IPictureService pictureService
            )
        {
            _villageService = villageService;
            _villageEatService = villageEatService;
            _villagePlayService = villagePlayService;
            _villageLiveService = villageLiveService;
            _pictureService = pictureService;
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
                villageModel.Logo = _pictureService.GetPictureUrl(logoPicture.Picture, 320);

            if (routePicture != null)
                villageModel.Route = _pictureService.GetPictureUrl(routePicture.Picture, 320);

            return Ok(villageModel);
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
        public IHttpActionResult GetVillageByIdInMap(int id=0)
        {
            var village = _villageService.GetVillageById(id);
            if (village == null) return NotFound();

            var villageModel = village.ToGeoModel();

            var logoPicture = village.VillagePictures.Where(vp => vp.IsLogo).FirstOrDefault();
            var routePicture = village.VillagePictures.Where(vp => vp.IsRoute).FirstOrDefault();

            if (logoPicture != null)
                villageModel.Logo = _pictureService.GetPictureUrl(logoPicture.Picture, 320);

            return Ok(villageModel);
        }
    }
}