using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
    public class VillagePlay:BaseEntity
    {
        public ICollection<PlayPicture> _playPictures;
        /// <summary>
        /// 名称
        /// </summary>
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
        /// 语音讲解路径
        /// </summary>
        public string AudioUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 坐落位置
        /// </summary>
        public DbGeography Location { get; set; }

        public int Order { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public virtual ICollection<PlayPicture> PlayPictures
        {
            get { return _playPictures ?? (_playPictures = new List<PlayPicture>()); }
            protected set { _playPictures = value; }
        }
        /// <summary>
        /// 对应的乡村景点
        /// </summary>
        public Village Village { get; set; }

    }
}
