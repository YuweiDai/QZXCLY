using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
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
        /// 图片
        /// </summary>
        public virtual ICollection<LivePicture> livePictures
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
