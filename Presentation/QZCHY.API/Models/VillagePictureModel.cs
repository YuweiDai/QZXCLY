using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillagePictureModel: BaseQMEntityModel
    {

        public int Village_Id { get; set; }

        public int Picture_Id { get; set; }

        public bool IsLogo { get; set; }

        public string Href { get; set; }
    }
}