﻿using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Villages
{
    public partial interface IVillageLiveService
    {

        void InsertVillageLive(VillageLive live);

        void UpdateVillageLive(VillageLive live);

        void DeleteVillageLive(VillageLive live);

        VillageLive GetVillageLiveById(int id);
    }
}
