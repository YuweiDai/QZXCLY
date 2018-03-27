using System;
using System.Collections.Generic;
using System.Linq;
using QZCHY.Core.Domain.Villages;
using QZCHY.Core.Data;
using QZCHY.Services.Events;
using QZCHY.Core.Caching;

namespace QZCHY.Services.Villages
{
    public class VillageEatService : IVillageEatService
    {
        private const string VillageEat_BY_ID_KEY = "QZCHY.villageEat.id-{0}";

        private readonly IRepository<VillageEat> _villageEatRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        public VillageEatService(ICacheManager cacheManager, 
            IRepository<VillageEat> villageEatRepository, IEventPublisher eventPublisher)
        {
            this._villageEatRepository = villageEatRepository;
            this._eventPublisher = eventPublisher;

            this._cacheManager = cacheManager;
        }


        public void DeleteVillageEat(VillageEat eat)
        {
            if (eat == null) throw new ArgumentNullException("eat");
            else
            {
                _villageEatRepository.Delete(eat);
                _eventPublisher.EntityDeleted(eat);
            }
        }

        public IList< VillageEat> GetVillageEatByVillageId(int id)
        {
            var eats = new List<VillageEat>();
            var query = from v in _villageEatRepository.Table
                        where v.Village.Id == id
                        select v;
            eats = query.ToList();
            return eats;
        }

        public void InsertVillageEat(VillageEat eat)
        {
            if (eat == null) throw new ArgumentNullException("eat");
            else
            {
                _villageEatRepository.Insert(eat);
                _eventPublisher.EntityInserted(eat);
            }
        }

        public void UpdateVillageEat(VillageEat eat)
        {
            if (eat == null) throw new ArgumentNullException("eat");
            else
            {
                _villageEatRepository.Update(eat);
                _eventPublisher.EntityUpdated(eat);
            }
        }

        public EatPicture GetEatLogoPictureById(int villageEatId)
        {
            var eatPicture = GetVillageEatById(villageEatId);
            if (eatPicture == null || eatPicture.Deleted) return null;

            return eatPicture.EatPictures.Where(ep => ep.IsLogo).SingleOrDefault();
        }

        public VillageEat GetVillageEatById(int villageEatId)
        {
            if (villageEatId == 0) return null;

            string key = string.Format(VillageEat_BY_ID_KEY, villageEatId);
            return _cacheManager.Get(key, () => _villageEatRepository.GetById(villageEatId));
        }
    }
}
