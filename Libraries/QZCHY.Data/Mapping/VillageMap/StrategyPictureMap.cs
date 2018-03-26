using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
    public class StrategyPictureMap : EntityTypeConfiguration<StrategyPicture>
    {

        public StrategyPictureMap() {
            this.ToTable("StrategyPicture");
            this.HasKey(p => p.Id);


            this.HasRequired(p => p.Strategy).WithMany().HasForeignKey(pp => pp.StrategyId);
            this.HasRequired(p => p.Picture).WithMany().HasForeignKey(pp => pp.PictureId);
        }

    }
}
