using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
    public class Strategy : BaseEntity
    {

        private ICollection<StrategyPicture> _strategyPictures;

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Src { get; set; }
        

        /// <summary>
        /// 图片
        /// </summary>
        public virtual ICollection<StrategyPicture> StrategyPictures
        {
            get { return _strategyPictures ?? (_strategyPictures = new List<StrategyPicture>()); }
            protected set { _strategyPictures = value; }
        }

        public virtual Village Village { get; set; }
    }
}
