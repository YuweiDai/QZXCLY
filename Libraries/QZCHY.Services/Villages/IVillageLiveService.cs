using QZCHY.Core.Domain.Villages;
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

        IList<VillageLive> GetVillageLiveByVillageId(int id);

        LivePicture GetLiveLogoPictureById(int id);

        VillageLive GetVillageLiveById(int id);
    }
}