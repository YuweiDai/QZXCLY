using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
   public  class VillageLiveMap:EntityTypeConfiguration<VillageLive>
    {
        public VillageLiveMap()
        {
            this.ToTable("VillageLive");
            this.HasKey(p => p.Id);
            this.Property(p => p.Title).IsRequired().HasMaxLength(255);

         //   this.HasMany(p => p.livePictures).WithRequired(pp => pp.VillageLive);
          //  this.HasRequired(pp => pp.Village).WithMany(p => p.Lives);
        }

    }
}
