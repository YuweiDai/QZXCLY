using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZCHY.Core.Domain.Villages;
using QZCHY.Core.Data;
using QZCHY.Services.Events;

namespace QZCHY.Services.Villages
{
    public partial class VillageLiveService : IVillageLiveService
    {
        private readonly IRepository<VillageLive> _villageLiveRepository;
        private readonly IEventPublisher _eventPublisher;

        public VillageLiveService(IRepository<VillageLive> villagePlayRepository, IEventPublisher eventPublisher)
        {
            this._villageLiveRepository = villagePlayRepository;
            this._eventPublisher = eventPublisher;

        }
        public void DeleteVillageLive(VillageLive live)
        {
            if (live == null) throw new ArgumentNullException("live");
            else
            {
                _villageLiveRepository.Delete(live);
                _eventPublisher.EntityDeleted(live);
            }
        }

        public IList<VillageLive> GetVillageLiveByVillageId(int id)
        {
            var lives = new List<VillageLive>();

            var query = from v in _villageLiveRepository.Table
                        where v.Village.Id == id
                        select v;
            lives = query.ToList();
            return lives;
        }

        public void InsertVillageLive(VillageLive live)
        {
            if (live == null) throw new ArgumentNullException("live");
            else
            {
                _villageLiveRepository.Insert(live);
                _eventPublisher.EntityInserted(live);
            }
        }

        public void UpdateVillageLive(VillageLive live)
        {
            if (live == null) throw new ArgumentNullException("live");
            else
            {
                _villageLiveRepository.Update(live);
                _eventPublisher.EntityUpdated(live);
            }
        }
    }
}
