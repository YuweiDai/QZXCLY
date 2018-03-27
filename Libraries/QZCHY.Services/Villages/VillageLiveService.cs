using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZCHY.Core.Domain.Villages;
using QZCHY.Core.Data;
using QZCHY.Services.Events;
using QZCHY.Core.Caching;

namespace QZCHY.Services.Villages
{
    public partial class VillageLiveService : IVillageLiveService
    {
        private const string VillageLive_BY_ID_KEY = "QZCHY.villageLive.id-{0}";

        private readonly IRepository<VillageLive> _villageLiveRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        public VillageLiveService(ICacheManager cacheManager, 
            IRepository<VillageLive> villagePlayRepository, IEventPublisher eventPublisher)
        {
            this._villageLiveRepository = villagePlayRepository;
            this._eventPublisher = eventPublisher;

            this._cacheManager = cacheManager;
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

        public LivePicture GetLiveLogoPictureById(int villageLiveId)
        {
            var eatPicture = GetVillageLiveById(villageLiveId);
            if (eatPicture == null || eatPicture.Deleted) return null;

            return eatPicture.LivePictures.Where(ep => ep.IsLogo).SingleOrDefault();
        }

        public VillageLive GetVillageLiveById(int villageLiveId)
        {
            if (villageLiveId == 0) return null;

            string key = string.Format(VillageLive_BY_ID_KEY, villageLiveId);
            return _cacheManager.Get(key, () => _villageLiveRepository.GetById(villageLiveId));
        }
    }
}
