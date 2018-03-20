using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillageModel: BaseQMEntityModel
    {

        public int Village_Id { get; set; }

        public string Name { get; set; }     

        public string Address { get; set; }

        public string Phone { get; set; }

        public string OpenTime { get; set; }

        public string Desc { get; set; }
 
        public string Tags { get; set; }

        public double Price { get; set; }

        public string TourRoute { get; set; }

        public string Traffic { get; set; }

        public string Icon { get; set; }

        public string Location { get; set; }

        /// <summary>
        /// 旅游空间路线
        /// </summary>
        public string GeoTourRoute { get; set; }

        public IList<VillagePictureModel> Pictures { get; set; }

        public IList<VillageEatModel> Eats { get; set; }

        public IList<VillageLiveModel> Lives { get; set; }
        public IList<VillagePlayModel> Plays { get; set; }
        public IList<VillageServiceModel> Services { get; set; }

        public IList<VillageVedioModel> Vedios { get; set; }
    }
}