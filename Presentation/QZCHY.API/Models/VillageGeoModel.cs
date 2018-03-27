using QZCHY.Web.Framework.Mvc;
using System.Collections.Generic;

namespace QZCHY.API.Models
{
    /// <summary>
    /// 初始地图界面模型
    /// </summary>
    public class SimpleVillageGeoModel : BaseQMEntityModel
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    /// <summary>
    /// 单个景区空间模型
    /// </summary>
    public class VillageGeoModel : BaseQMEntityModel
    {
        public string Name { get; set; }

        public string Logo { get; set; }

        public string Distance { get; set; }

        public string Panorama { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        /// <summary>
        /// 旅游空间路线
        /// </summary>
        public string GeoTourRoute { get; set; }

        public IList<ServiceGeoModel> Services { get; set; }

        public IList<PlayGeoModel> Plays { get; set; }
        public IList<LiveGeoModel> Lives { get; set; }

        public IList<EatGeoModel> Eats { get; set; }
    }

    /// <summary>
    /// 景区服务设施、吃、喝、玩基类
    /// </summary>
    public class SpotItemGeoModel:BaseQMEntityModel
    {
        public string Name { get; set; }

        public string Logo { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    /// <summary>
    /// 公共服务设施
    /// </summary>
    public class ServiceGeoModel:SpotItemGeoModel
    {

    }

    /// <summary>
    /// 玩的地方
    /// </summary>
    public class PlayGeoModel : SpotItemGeoModel
    {
        public string AudioUrl { get; set; }

        public string PanoramaId { get; set; }
    }

    /// <summary>
    /// 吃的地方
    /// </summary>
    public class EatGeoModel : ServiceGeoModel
    {

        public string Level { get; set; }

        public double Price { get; set; }

        public string Tel { get; set; }

        public string PanoramaId { get; set; }
    }

    /// <summary>
    /// 住的地方
    /// </summary>
    public class LiveGeoModel : SpotItemGeoModel
    {

        public string Tel { get; set; }

        public string PanoramaId { get; set; }
    }
}