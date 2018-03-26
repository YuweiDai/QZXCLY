using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZCHY.Core.Domain.Villages;

namespace QZCHY.Services.Villages
{
    public partial interface IVillageService
    {

        void InsertVillage(Village village);

        void UpdateVillage(Village village);

        void DeleteVillage(Village village);

        Village GetVillageById(int id);

        Village GetVillageByName(string name);

        /// <summary>
        /// 获取所有的乡村旅游区
        /// </summary>
        /// <returns></returns>
        IQueryable<Village> GetAllVillages();

    }
}
