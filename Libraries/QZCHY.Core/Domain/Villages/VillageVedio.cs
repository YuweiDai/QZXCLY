using QZCHY.Core.Domain.Media;

namespace QZCHY.Core.Domain.Villages
{
    public class VillageVedio : BaseEntity
    {

        public int VillageId { get; set; }

        public int VedioId { get; set; }

        public int Order { get; set; }

        public virtual Vedio Vedio { get; set; }

        public virtual Village Village { get; set; }
    }
}
