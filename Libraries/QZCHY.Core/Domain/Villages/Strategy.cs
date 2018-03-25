using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
    public class Strategy : BaseEntity
    {

        public string Title { get; set; }

 
        public DateTime Date { get; set; }


        public string Src { get; set; }

        public virtual StrategyPicture StrategyPicture { get; set; }

        public virtual Village Village { get; set; }

    }
}
