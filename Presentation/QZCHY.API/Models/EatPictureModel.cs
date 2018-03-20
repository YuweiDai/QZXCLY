using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class EatPictureModel: BaseQMEntityModel
    {
        public int EatId { get; set; }
        public int PictureId { get; set; }

        public bool IsLogo { get; set; }
        public int Picture_Id { get; set; }
    }
}