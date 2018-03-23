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
        [Route("dpgd")]
        public IHttpActionResult GetVillageById(string Name)
        {
             Name = "康养衢江· 隐柿东坪";
            var response = _villageService.GetVillageByName(Name).ToModel();

            foreach (var img in response.VillagePictures) {

                img.Href = _pictureService.GetPictureUrl(img.Id);

            }



            return Ok(response);
        }


    }
}