using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.Villages
{
    public partial interface IStrategyService
    {


        void InsertStrategy(Strategy stra);

        void UpdateStrategy(Strategy stra);

        void DeleteStrategy(Strategy stra);

      IList< Strategy> GetStrategyByVillageId(int id);
    }
}
