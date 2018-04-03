using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
   public class Village:BaseEntity
    {
        private ICollection<VillageService> _villageServices;
        private ICollection<VillagePlay> _villagePlays;
        private ICollection<VillageLive> _villageLives;
        private ICollection<VillageEat> _villageEats;
        private ICollection<VillagePicture> _villagePictures;
        private ICollection<Strategy> _strategies;

        /// <summary>
        /// 景区名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 开放时间
        /// </summary>
        public string OpenTime { get; set; }
        /// <summary>
        /// 景区介绍
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 景区标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 门票，免费即为0
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 旅游路线
        /// </summary>
        public string TourRoute { get; set; }

        /// <summary>
        /// 交通路线
        /// </summary>
        public string Triffic { get; set; }

        /// <summary>
        /// 景区A级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 旅游空间路线
        /// </summary>
        public System.Data.Entity.Spatial.DbGeography GeoTourRoute { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 对应全景的名称Id
        /// </summary>
        public string Panorama { get; set; }

        /// <summary>
        /// 录音url
        /// </summary>
        public string VideoUrl { get; set; }


        public Region Region { get; set; }

        /// <summary>
        /// 坐落位置
        /// </summary>
        public System.Data.Entity.Spatial.DbGeography Location { get; set; }

        public virtual ICollection<VillageService> Services
        {
            get { return _villageServices ?? (_villageServices = new List<VillageService>()); }
            protected set { _villageServices = value; }
        }

        public virtual ICollection<VillagePlay> Plays
        {
            get { return _villagePlays ?? (_villagePlays = new List<VillagePlay>()); }
            protected set { _villagePlays = value; }
        }

        public virtual ICollection<VillageLive> Lives
        {
            get { return _villageLives ?? (_villageLives = new List<VillageLive>()); }
            protected set { _villageLives = value; }
        }

        public virtual ICollection<VillageEat> Eats
        {
            get { return _villageEats ?? (_villageEats = new List<VillageEat>()); }
            protected set { _villageEats = value; }
        }

        public virtual ICollection<VillagePicture> VillagePictures
        {
            get { return _villagePictures ?? (_villagePictures = new List<VillagePicture>()); }
            protected set { _villagePictures = value; }
        }

        public virtual ICollection<Strategy> Strategies
        {
            get { return _strategies ?? (_strategies = new List<Strategy>()); }
            protected set { _strategies = value; }
        }
    }
}
