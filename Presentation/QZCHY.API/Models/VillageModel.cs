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

        public string Region { get; set; }
 
        public string Tags { get; set; }

        public double Price { get; set; }

        public string Triffic { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Logo { get; set; }
        public string HotMonth { get; set; }

        public string TourRoute { get; set; }

        public string RoutePicutre { get; set; }

        public string VideoUrl { get; set; }

        public string Panorama { get; set; }

   

        public IList<StrategyModel> Strategies { get; set; }

        public virtual ICollection<VillagePictureModel> VillagePictures { get; set; }

        public IList<SimpleEatModel> Eats { get; set; }

        public IList<SimpleLiveModel> Lives { get; set; }

        public IList<SimplePlayModel> Plays { get; set; }
    }


    public class VillageSimpleModel: BaseQMEntityModel
    {

        public string Name { get; set; }

        public string Logo { get; set; }
        public double Long { get; set; }
    }

    public class HotVillageListModel {

        public string Name { get; set; }
        public string Logo { get; set; }
        public string Tags { get; set; }
        public string Desc { get; set; }

    }


}