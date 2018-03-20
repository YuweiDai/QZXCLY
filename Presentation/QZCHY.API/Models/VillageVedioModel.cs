using QZCHY.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    public class VillageVedioModel: BaseQMEntityModel
    {

        public int VillageId { get; set; }

        public int VedioId { get; set; }

        public int Order { get; set; }

        public int Village_Id { get; set; }

        public int Vedio_Id { get; set; }

    }
}