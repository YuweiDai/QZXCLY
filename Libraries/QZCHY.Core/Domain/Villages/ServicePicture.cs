﻿using QZCHY.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Core.Domain.Villages
{
    public class ServicePicture : BaseEntity
    {
        public int ServiceId { get; set; }

        public int PictureId { get; set; }

        public bool IsLogo { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual VillageService VillageService { get; set; }
    }
}
