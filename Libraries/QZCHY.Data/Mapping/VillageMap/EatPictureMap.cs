﻿using QZCHY.Core.Domain.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Data.Mapping.VillageMap
{
    public class EatPictureMap:EntityTypeConfiguration<EatPicture>
    {
        public EatPictureMap()
        {
            this.ToTable("EatPicture");
            this.HasKey(p => p.Id);

            this.HasRequired(p => p.Picture).WithMany().HasForeignKey(pp => pp.PictureId);
            this.HasRequired(p => p.VillageEat).WithMany().HasForeignKey(pp => pp.EatId);
        }
    }
}
