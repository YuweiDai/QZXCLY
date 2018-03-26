using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QZCHY.API.Controllers
{
    /// <summary>
    /// 通用性控制器
    /// </summary>
    [RoutePrefix("Common")]
    public class CommonController : ApiController
    {
        public CommonController()
        {

        }


        [HttpGet]
        [Route("Distance")]
        public IHttpActionResult CalculateDistance(double lon, double lat, double lon1, double lat1)
        {
            DbGeography point1 = null, point2 = null;

            try
            {
                point1 = DbGeography.FromText(string.Format("POINT( {0} {1})", lon, lat));
                point2 = DbGeography.FromText(string.Format("POINT( {0} {1})", lon1, lat1));
            }
            catch
            {

            }

            if (point1 == null || point2 == null) return BadRequest("输入的参数有误");

            var distance = point1.Distance(point2);
            return Ok(distance);
        }
    }
}
