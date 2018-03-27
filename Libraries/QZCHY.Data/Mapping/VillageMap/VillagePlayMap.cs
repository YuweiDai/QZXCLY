using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
    public class VillagePlayMap:EntityTypeConfiguration<VillagePlay>
    {
        public VillagePlayMap()
        {
            this.ToTable("VillagePlay");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(255);

            this.HasMany(p => p.PlayPictures).WithRequired(pp => pp.VillagePlay);
            this.HasRequired(pp => pp.Village).WithMany(p => p.Plays);
        }
    }
}
