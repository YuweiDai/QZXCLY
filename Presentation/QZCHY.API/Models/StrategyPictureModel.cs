using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class StrategyPictureModel: BaseQMEntityModel
    {
        public int Strategy_Id { get; set; }
        public int Picture_Id { get; set; }

    }
}