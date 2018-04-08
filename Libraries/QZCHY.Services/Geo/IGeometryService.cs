using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Geo
{
    /// <summary>
    /// 几何空间接口
    /// </summary>
    public interface IGeometryService
    {
        double CalculateDistance(double lon1, double lat1, double lon2, double lat2);
    }
}