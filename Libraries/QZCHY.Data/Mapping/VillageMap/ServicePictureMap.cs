using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
    public class ServicePictureMap : EntityTypeConfiguration<ServicePicture>
    {

        public ServicePictureMap()
        {
            this.ToTable("ServicePicture");
            this.HasKey(p => p.Id);

            this.HasRequired(p => p.Picture).WithMany().HasForeignKey(pp => pp.PictureId);
            this.HasRequired(p => p.VillageService).WithMany().HasForeignKey(pp => pp.ServiceId);
        }
    }
}
