﻿using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Villages
{
    public partial interface IVillageEatService
    {
        void InsertVillageEat(VillageEat eat);

        void UpdateVillageEat(VillageEat eat);

        void DeleteVillageEat(VillageEat eat);

        IList<VillageEat> GetVillageEatByVillageId(int id);

        EatPicture GetEatLogoPictureById(int id);

        VillageEat GetVillageEatById(int id);
    }
}
