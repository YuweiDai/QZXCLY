using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
    public class VillageServiceMap : EntityTypeConfiguration<VillageService>
    {
        public VillageServiceMap()
        {
            this.ToTable("VillageService");
            this.HasKey(p => p.Id);
            this.Property(p => p.Title).IsRequired().HasMaxLength(255);

            //  this.HasMany(p => p.playPictures).WithRequired(pp => pp.VillagePlay);
            //  this.HasRequired(pp => pp.Village).WithMany(p => p.Plays);
        }
    }
}
