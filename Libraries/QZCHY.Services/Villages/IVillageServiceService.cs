using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Villages
{
    public interface IVillageServiceService
    {
        IList<QZCHY.Core.Domain.Villages.VillageService> GetVillageServicesByVillageId(int villageServiceId);

        ServicePicture GetServiceLogoPictureById(int villageServiceId);

        QZCHY.Core.Domain.Villages.VillageService GetVillageServiceById(int villageServiceId);
    }
}
