using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
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
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public virtual ICollection<PlayPicture> playPictures
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
