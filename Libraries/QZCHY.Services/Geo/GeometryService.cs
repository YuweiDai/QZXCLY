using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Geo
{
    public class GeometryService : IGeometryService
    {
        private readonly double EARTH_RADIUS = 6371.0;//km 地球半径 平均值，千米

        public string CalculateDistance(double lon1, double lat1, double lon2, double lat2)
        {


            try
            {
                var d = Distance(lat1, lon1, lat2, lon2);

                return d > 1000 ? Math.Round(d / 1000, 2) + "公里" : Math.Round(d, 2) + "米";
            }
            catch (Exception e)
            {

            }

            return "null";
        }



        private double HaverSin(double theta)
        {
            var v = Math.Sin(theta / 2);
            return v * v;
        }

        /// <summary>
        /// 给定的经度1，纬度1；经度2，纬度2. 计算2个经纬度之间的距离。
        /// </summary>
        /// <param name="lat1">经度1</param>
        /// <param name="lon1">纬度1</param>
        /// <param name="lat2">经度2</param>
        /// <param name="lon2">纬度2</param>
        /// <returns>距离（公里、千米）</returns>
        private double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            //用haversine公式计算球面两点间的距离。
            //经纬度转换成弧度
            lat1 = ConvertDegreesToRadians(lat1);
            lon1 = ConvertDegreesToRadians(lon1);
            lat2 = ConvertDegreesToRadians(lat2);
            lon2 = ConvertDegreesToRadians(lon2);

            //差值
            var vLon = Math.Abs(lon1 - lon2);
            var vLat = Math.Abs(lat1 - lat2);

            //h is the great circle distance in radians, great circle就是一个球体上的切面，它的圆心即是球心的一个周长最大的圆。
            var h = HaverSin(vLat) + Math.Cos(lat1) * Math.Cos(lat2) * HaverSin(vLon);

            var distance = 2 * EARTH_RADIUS * Math.Asin(Math.Sqrt(h));

            return distance;
        }

        /// <summary>
        /// 将角度换算为弧度。
        /// </summary>
        /// <param name="degrees">角度</param>
        /// <returns>弧度</returns>
        private double ConvertDegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private double ConvertRadiansToDegrees(double radian)
        {
            return radian * 180.0 / Math.PI;
        }
    }
}