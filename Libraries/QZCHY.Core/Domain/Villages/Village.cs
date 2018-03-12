using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
   public class Village:BaseEntity
    {

        private ICollection<VillagePlay> _villagePlays;
        private ICollection<VillageLive> _villageLives;
        private ICollection<VillageEat> _villageEats;
        private ICollection<VillagePicture> _villagePictures;

        /// <summary>
        /// 景点名称
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
        /// 坐落位置
        /// </summary>
        public DbGeography Location { get; set; }

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
    }
}
