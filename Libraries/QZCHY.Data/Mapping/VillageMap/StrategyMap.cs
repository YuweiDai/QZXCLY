using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
   public  class StrategyMap: EntityTypeConfiguration<Strategy>
    {
        public StrategyMap()
        {
            this.ToTable("Strategy");
            this.HasKey(p => p.Id);
            this.Property(p => p.Title).IsRequired();

            this.HasMany(p => p.StrategyPictures).WithRequired(s => s.Strategy);
            //this.HasRequired(p => p.Village).WithMany(v => v.Strategies);
        }

      
    }
}
