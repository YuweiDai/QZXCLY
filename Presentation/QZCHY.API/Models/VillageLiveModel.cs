using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillageLiveModel: BaseQMEntityModel
    {

        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Person { get; set; }
        /// <summary>
        /// 坐落位置
        /// </summary>
        public string Longitude { get; set; }
        public string Latitude { get; set; }
   
        public int PanoramaId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 一次性可接待人数，为0则表明不详细
        /// </summary>
        public int BedsNumber { get; set; }

        /// <summary>
        /// 房价信息
        /// </summary>
        public string RoomPrice { get; set; }

        /// <summary>
        /// 特色标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 民宿的设施，诸如空调、热水器，分号隔开
        /// </summary>
        public string Facilities { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// 景区全景标识
        /// </summary>
        public string Panorama { get; set; }

        public virtual ICollection<LivePictureModel> LivePictures { get; set; }

        public int Vliiage_Id { get; set; }

        public string Logo { get; set; }

    }


    public class SimpleLiveModel:BaseQMEntityModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Tel { get; set; }

        public int BedsNumber { get; set; }

        public string Logo { get; set; }

        /// <summary>
        /// 景区全景标识
        /// </summary>
        public string Panorama { get; set; }

        public int Panoramaid { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}