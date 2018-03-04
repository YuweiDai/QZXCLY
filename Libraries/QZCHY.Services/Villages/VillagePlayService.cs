using QZCHY.Core.Data;
using QZCHY.Core.Domain.Villages;
using QZCHY.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Villages
{
    public partial class VillagePlayService : IVillagePlayService
    {
        private readonly IRepository<VillagePlay> _villagePlayRepository;
        private readonly IEventPublisher _eventPublisher;

        public VillagePlayService(IRepository<VillagePlay> villagePlayRepository, IEventPublisher eventPublisher)
        {
            this._villagePlayRepository = villagePlayRepository;
            this._eventPublisher = eventPublisher;

        }

        public void DeleteVillagePaly(VillagePlay play)
        {
            if (play == null) throw new ArgumentNullException("play");
            else
            {
                _villagePlayRepository.Delete(play);
                _eventPublisher.EntityDeleted(play);
            }
            throw new NotImplementedException();
        }

        public VillagePlay GetVillagePlayById(int id)
        {
            var query = from v in _villagePlayRepository.Table
                        where v.Id == id
                        select v;
            return query.FirstOrDefault();
        }

        public void InsertVillagePlay(VillagePlay play)
        {
            if (play == null) throw new ArgumentNullException("play");
            else
            {
                _villagePlayRepository.Insert(play);
                _eventPublisher.EntityInserted(play);
            }
        }

        public void UpdateVillagePlay(VillagePlay play)
        {
            if (play == null) throw new ArgumentNullException("play");
            else
            {
                _villagePlayRepository.Update(play);
                _eventPublisher.EntityUpdated(play);
            }
                       
        }
    }
}
