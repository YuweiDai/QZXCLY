﻿using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillageEatModel: BaseQMEntityModel
    {

        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Person { get; set; }

        /// <summary>
        /// 人均
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 农家乐星级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 一次性可接待人数，为0则表明不详细
        /// </summary>
        public int ReceptionNumber { get; set; }

        /// <summary>
        /// 特色标签，注入制定疗休养之类的标签，用分号隔开
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 特色菜
        /// </summary>
        public string Suggestion { get; set; }

        /// <summary>
        /// 坐落位置
        /// </summary>
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        public int Order { get; set; }

        public virtual ICollection<EatPictureModel> EatPictures { get; set; }

        public int Village_Id { get; set; }

        public string Logo { get; set; }

        /// <summary>
        /// 景区全景标识
        /// </summary>
        public string Panorama { get; set; }

        public int Panoramaid { get; set; }
    }

    public class SimpleEatModel : BaseQMEntityModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Tel { get; set;}

        public double Price { get; set; }

        public int Level { get; set; }

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