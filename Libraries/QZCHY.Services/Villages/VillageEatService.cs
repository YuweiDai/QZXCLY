﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZCHY.Core.Domain.Villages;
using QZCHY.Core.Data;
using QZCHY.Services.Events;

namespace QZCHY.Services.Villages
{
    public class VillageEatService : IVillageEatService
    {
        private readonly IRepository<VillageEat> _villageEatRepository;
        private readonly IEventPublisher _eventPublisher;

        public VillageEatService(IRepository<VillageEat> villageEatRepository, IEventPublisher eventPublisher)
        {
            this._villageEatRepository = villageEatRepository;
            this._eventPublisher = eventPublisher;

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

        public VillageEat GetVillageEatById(int id)
        {
            var query = from v in _villageEatRepository.Table
                        where v.Id == id
                        select v;
            return query.FirstOrDefault();
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
    }
}
