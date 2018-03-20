using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class StrategyModel: BaseQMEntityModel
    {

        public string Title { get; set; }


        public string Src { get; set; }

        public int Village_Id { get; set; }

        public StrategyPictureModel Img { get; set; }

    }
}