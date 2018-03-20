using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZCHY.Core.Domain.Villages;

namespace QZCHY.Services.Villages
{
    public partial interface IVillageService
    {

        void InsertVillage(Village village);

        void UpdateVillage(Village village);

        void DeleteVillage(Village village);

        Village GetVillageById(int id);

        Village GetVillageByName(string name);

    }
}
