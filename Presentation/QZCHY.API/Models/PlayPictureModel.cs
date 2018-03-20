﻿using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class PlayPictureModel: BaseQMEntityModel
    {

        public int PlayId { get; set; }
        public int PictureId { get; set; }

        public bool IsLogo { get; set; }

        public int Picture_Id { get; set; }
    }
}