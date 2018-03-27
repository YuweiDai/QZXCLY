using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
   public  class VillageEatMap:EntityTypeConfiguration<VillageEat>
    {

        public VillageEatMap()
        {
            this.ToTable("VillageEat");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(255);

            this.HasMany(p => p.EatPictures).WithRequired(pp => pp.VillageEat);
            this.HasRequired(pp => pp.Village).WithMany(p => p.Eats);
        }

    }
}
