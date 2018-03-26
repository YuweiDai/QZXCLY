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
    public class VillageController:BaseApiController
    {

        private readonly IVillageService _villageService;
        private readonly IVillageEatService _villageEatService;
        private readonly IVillagePlayService _villagePlayService;
        private readonly IVillageLiveService _villageLiveService;

        public VillageController(IVillageService villageService, IVillageEatService villageEatService,
            IVillagePlayService villagePlayService, IVillageLiveService villageLiveService
            )
        {
            _villageService = villageService;
            _villageEatService = villageEatService;
            _villagePlayService = villagePlayService;
            _villageLiveService = villageLiveService;

        }

        [HttpGet]
        [Route("dpgd")]
        public IHttpActionResult GetVillageById(string Name)
        {
            Name = "康养衢江· 隐柿东坪";
            var response = _villageService.GetVillageByName(Name).ToModel();
            return Ok(response);
        }
    }
}