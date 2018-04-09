using QZCHY.API.Models;
using QZCHY.Core.Domain.Villages;
using QZCHY.Services.Media;
using QZCHY.Services.Villages;
using QZCHY.Web.Api.Extensions;
using QZCHY.Web.Framework.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QZCHY.API.Controllers
{
    [RoutePrefix("Strategy")]
    public class StrategyController : ApiController
    {

        private readonly IStrategyService _strategyService;
        private readonly IPictureService _pictureService;
        private readonly IVillageService _villageService;

        public StrategyController(IStrategyService strategyService, IPictureService pictureService, IVillageService villageService)
        {
            _strategyService = strategyService;
            _pictureService = pictureService;
            _villageService = villageService;
        }

        [HttpGet]
        [Route("Random")]
        public IHttpActionResult GetRandomStrategy(int pageSize = Int32.MaxValue, int pageIndex = 0)
        {

            //var sum = _strategyService.GetAllStrategy().Count() + 1;
            ////获取随机数
            //Random random = new Random();
            //List<int> result = new List<int>();
            //int temp;
            //while (result.Count < 3)
            //{
            //    temp = random.Next(1, sum);
            //    if (!result.Contains(temp))
            //    {
            //        result.Add(temp);
            //    }
            //}

            //var strategyModels = new List<StrategyModel>();
            //var strategys = new List<Strategy>();

            //foreach (var id in result)
            //{
            //    var strategy = _strategyService.GetStrategyById(id);
            //    strategys.Add(strategy);
            //}


            //foreach (var s in strategys)
            //{

            //    var picture = s.StrategyPictures.FirstOrDefault();
            //    var strategyModel = s.ToModel();
            //    strategyModel.Img = _pictureService.GetPictureUrl(picture.Picture);

            //    strategyModels.Add(strategyModel);

            //}

            var strategys = _strategyService.GetAllStrategy();

            var response = new ListResponse<StrategyModel>
            {
                Paging = new Paging
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Total = strategys.Count,
                    //   FilterCount = string.IsNullOrEmpty(query) ? properties.TotalCount : properties.Count,
                },
                Data = strategys.Select(s =>
                {
                    var strategyModel = s.ToModel();
                    return strategyModel;
                })
            };


            return Ok(response);
        }

        [HttpGet]
        [Route("Village/{villageId}")]
        public IHttpActionResult GetVillageStrategies(int villageId = 0)
        {
            var village = _villageService.GetVillageById(villageId);
            if (village == null) return NotFound();

            var strategies = village.Strategies.ToList().Select(s => s.ToModel());
            return Ok(strategies);
        }
    }
}