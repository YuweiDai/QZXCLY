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
   public  class StrategyService:IStrategyService
    {

        private readonly IRepository<Strategy> _strategyRepository;
        private readonly IEventPublisher _eventPublisher;

        public StrategyService(IRepository<Strategy> strategyRepository, IEventPublisher eventPublisher)
        {
            this._strategyRepository = strategyRepository;
            this._eventPublisher = eventPublisher;

        }

        public void InsertStrategy(Strategy stra)
        {
            if (stra == null) throw new ArgumentNullException("stra");
            else
            {
                _strategyRepository.Insert(stra);
                _eventPublisher.EntityInserted(stra);
            }
        }

        public void UpdateStrategy(Strategy stra)
        {
            if (stra == null) throw new ArgumentNullException("stra");
            else
            {
                _strategyRepository.Update(stra);
                _eventPublisher.EntityUpdated(stra);
            }
        }

        public void DeleteStrategy(Strategy stra)
        {
            if (stra == null) throw new ArgumentNullException("stra");
            else
            {
                _strategyRepository.Delete(stra);
                _eventPublisher.EntityDeleted(stra);
            }
        }

        public IList< Strategy> GetStrategyByVillageId(int id)
        {
            var response = new List<Strategy>();
            var query = from v in _strategyRepository.Table
                        where v.Village.Id == id
                        select v;
            response = query.ToList();
            return response;
        }
    }
}
