using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
    public class VillageLive:BaseEntity
    {

        public ICollection<LivePicture> _livePictures;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
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
        public DbGeography Location { get; set; }
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
        /// 图片
        /// </summary>
        public virtual ICollection<LivePicture> LivePictures
        {
            get { return _livePictures ?? (_livePictures = new List<LivePicture>()); }
            protected set { _livePictures = value; }
        }
        /// <summary>
        /// 对应的乡村景点
        /// </summary>
        public Village Village { get; set; }
    }
}
