using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Media
{
    /// <summary>
    /// 视频资源
    /// </summary>
    public class Vedio : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
