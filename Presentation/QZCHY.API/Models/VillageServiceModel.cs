using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillageServiceModel: BaseQMEntityModel
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
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        public virtual ICollection<ServicePictureModel> ServicePictures { get; set; }

        public int Village_Id { get; set; }
    }
}