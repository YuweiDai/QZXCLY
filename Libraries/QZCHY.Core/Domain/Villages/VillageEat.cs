using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
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
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }        
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
        /// 人均
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 农家乐星级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 一次性可接待人数，为0则表明不详细
        /// </summary>
        public int ReceptionNumber { get; set; }

        /// <summary>
        /// 特色标签，注入制定疗休养之类的标签，用分号隔开
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 特色菜
        /// </summary>
        public string Suggestion { get; set; }

        /// <summary>
        /// 坐落位置
        /// </summary>
        public DbGeography Location { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        public int Order { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public virtual ICollection<EatPicture> EatPictures
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
