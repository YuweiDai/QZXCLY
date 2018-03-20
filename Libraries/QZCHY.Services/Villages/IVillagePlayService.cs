using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Villages
{
   public partial  interface IVillagePlayService
    {
        void InsertVillagePlay(VillagePlay play);

        void UpdateVillagePlay(VillagePlay play);

        void DeleteVillagePaly(VillagePlay play);

       IList <VillagePlay> GetVillagePlayByVillageId(int id);
    }
}
