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
    public class VillageService : IVillageService
    {

        private readonly IRepository<Village> _villageRepository;
        private readonly IEventPublisher _eventPublisher;

        public VillageService(IRepository<Village> villageRepository, IEventPublisher eventPublisher) {
           this. _villageRepository = villageRepository;
           this. _eventPublisher = eventPublisher;

        }


        public void DeleteVillage(Village village)
        {
            if (village == null) throw new ArgumentNullException("village");
            else
            {
                _villageRepository.Delete(village);
                _eventPublisher.EntityDeleted(village);
            }         
        }

        public Village GetVillageById(int id)
        {
            var query = from v in _villageRepository.Table
                        where v.Id == id
                        select v;
            return query.FirstOrDefault();
        }

        public void InsertVillage(Village village)
        {
            if (village == null) throw new ArgumentNullException("village");
            else
            {
                _villageRepository.Insert(village);
                _eventPublisher.EntityInserted(village);
            }
        }

        public void UpdateVillage(Village village)
        {
            if (village == null) throw new ArgumentNullException("village");
            else
            {
                _villageRepository.Update(village);
                _eventPublisher.EntityUpdated(village);
            }
        }

        public Village GetVillageByName(string name)
        {
            var query = from v in GetAllVillages()
                        where v.Name == name
                        select v;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 获取所有的乡村
        /// </summary>
        /// <returns></returns>
        public IQueryable<Village> GetAllVillages()
        {
            var query = from v in _villageRepository.Table
                        where !v.Deleted
                        select v;

            return query;
        }

        public IList<Village> GetVillagesByRegionId(int id)
        {
            var query = from v in _villageRepository.Table
                        where (int)v.Region== id
                        select v;

            var villages = query.ToList();
            return villages;
          
        }

        public IList<Village> GetVillagesByMonth(int month)
        {
            var query = from v in _villageRepository.Table
                        where v.HotMonth.Contains(month.ToString()+";")
                        select v;
            var villages = query.ToList();         

            return villages;
        }

        public IList<Village> GetHotVillages(int month)
        {
            var query = from v in _villageRepository.Table
                        where !v.HotMonth.Contains(month.ToString() + ";")
                        select v;
            var villages = query.ToList();
          
            return villages;
        }
    }
}
