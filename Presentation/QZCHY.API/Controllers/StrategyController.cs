using QZCHY.API.Models;
using QZCHY.Core.Domain.Villages;
using QZCHY.Services.Media;
using QZCHY.Services.Villages;
using QZCHY.Web.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QZCHY.API.Controllers
{
    [RoutePrefix("Strategy")]
    public class StrategyController: ApiController
    {

        private readonly IStrategyService _strategyService;
        private readonly IPictureService _pictureService;

        public StrategyController(IStrategyService strategyService, IPictureService pictureService)
        {
            _strategyService = strategyService;
            _pictureService = pictureService;
        }

        [HttpGet]
        [Route("Random")]
        public IHttpActionResult GetRandomStrategy()
        {

            var sum = _strategyService.GetAllStrategy().Count()+1;
            //获取随机数
            Random random = new Random();
            List<int> result = new List<int>();
            int temp;
            while (result.Count < 3)
            {
                temp = random.Next(1, sum);
                if (!result.Contains(temp))
                {
                    result.Add(temp);
                }
            }

            var strategyModels =new  List<StrategyModel>();
            var strategys = new List<Strategy>();

            foreach (var id in result) {
                var strategy = _strategyService.GetStrategyById(id);
                strategys.Add(strategy);
            }


            foreach (var s in strategys) {
             
                var picture = s.StrategyPictures.FirstOrDefault();
                var strategyModel = s.ToModel();
                strategyModel.Img = _pictureService.GetPictureUrl(picture.Picture);

                strategyModels.Add(strategyModel);

            }

            return Ok(strategyModels);
        }







    }
}