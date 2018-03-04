using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
    public class VillageEat:BaseEntity
    {
        private ICollection<EatPicture> _eatPictures;
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
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
        /// 图片
        /// </summary>
        public virtual ICollection<EatPicture> eatPictures
        {
            get { return _eatPictures ?? (_eatPictures = new List<EatPicture>()); }
            protected set { _eatPictures = value; }
        }
        /// <summary>
        /// 对应的乡村景点
        /// </summary>
        public Village Village { get; set; }

    }
}
