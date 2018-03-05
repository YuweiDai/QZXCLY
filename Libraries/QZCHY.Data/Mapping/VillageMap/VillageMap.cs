using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
   public  class VillageMap:EntityTypeConfiguration<Village>
    {

        public VillageMap()
        {
            this.ToTable("Village");
            this.HasKey(p=>p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(255);

            this.HasMany(p => p.villagePictures).WithRequired(pp=>pp.Village);
            this.HasMany(p => p.Eats).WithRequired(pp => pp.Village);
            this.HasMany(p => p.Lives).WithRequired(pp => pp.Village);
            this.HasMany(p => p.Plays).WithRequired(pp => pp.Village);

        }

    }
}
