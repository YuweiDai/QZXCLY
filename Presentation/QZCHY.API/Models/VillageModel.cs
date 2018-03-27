using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillageModel: BaseQMEntityModel
    {
        public string Name { get; set; }     

        public string Address { get; set; }

        public string Phone { get; set; }

        public string OpenTime { get; set; }

        public string Desc { get; set; }
 
        public string Tags { get; set; }

        public double Price { get; set; }

        public string Traffic { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Logo { get; set; }

        public string VideoUrl { get; set; }

        /// <summary>
        /// 旅游空间路线
        /// </summary>
        public string GeoTourRoute { get; set; }

        public virtual ICollection<VillagePictureModel> VillagePictures { get; set; }

        public IList<VillageEatModel> Eats { get; set; }

        public IList<VillageLiveModel> Lives { get; set; }

        public IList<VillagePlayModel> Plays { get; set; }
    }
}