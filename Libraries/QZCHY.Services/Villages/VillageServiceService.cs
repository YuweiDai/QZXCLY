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
    public class VillageServiceService : IVillageServiceService
    {
        private const string VillageService_BY_ID_KEY = "QZCHY.villageService.id-{0}";

        private readonly IRepository<QZCHY.Core.Domain.Villages.VillageService> _villageServiceRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        public VillageServiceService(ICacheManager cacheManager,
            IRepository<QZCHY.Core.Domain.Villages.VillageService> villageServiceRepository, IEventPublisher eventPublisher)
        {
            this._villageServiceRepository = villageServiceRepository;
            this._eventPublisher = eventPublisher;

            this._cacheManager = cacheManager;
        }

        public ServicePicture GetServiceLogoPictureById(int villageServiceId)
        {
            var eatPicture = GetVillageServiceById(villageServiceId);
            if (eatPicture == null || eatPicture.Deleted) return null;

            return eatPicture.ServicePictures.Where(ep => ep.IsLogo).SingleOrDefault();
        }

        public IList<QZCHY.Core.Domain.Villages.VillageService> GetVillageServicesByVillageId(int id)
        {
            throw new NotImplementedException();
        }

        public Core.Domain.Villages.VillageService GetVillageServiceById(int villageServiceId)
        {
            if (villageServiceId == 0) return null;

            string key = string.Format(VillageService_BY_ID_KEY, villageServiceId);
            return _cacheManager.Get(key, () => _villageServiceRepository.GetById(villageServiceId));
        }
    }
}
