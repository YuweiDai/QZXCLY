﻿using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
    public class StrategyPicture:BaseEntity
    {
        public int StrategyId { get; set; }
        public int PictureId { get; set; }

        public virtual Strategy Strategy { get; set; }
        public virtual Picture Picture { get; set; }

    }
}
