using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.medical

{
    public sealed class MedicalAvailabilityModes : BaseEntity
    {
        public short SAvailabilityModelID { get; set; }

        public string AvailabilityMode { get; set; } = string.Empty;
    }
}
