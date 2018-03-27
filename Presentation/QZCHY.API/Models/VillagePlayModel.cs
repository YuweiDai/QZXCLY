using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillagePlayModel: BaseQMEntityModel
    {

        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 全景id
        /// </summary>
        public int PanoramaId { get; set; }

        /// <summary>
        /// 语音讲解路径
        /// </summary>
        public string AudioUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 坐落位置
        /// </summary>
        public string Location { get; set; }

        public int Order { get; set; }

        public virtual ICollection<PlayPictureModel> PlayPictures { get; set; }

        public int Village_Id { get; set; }

    }
}