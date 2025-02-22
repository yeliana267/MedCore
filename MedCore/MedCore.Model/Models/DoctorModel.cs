

namespace MedCore.Model.Models
{
    public class DoctorModel
    {
        public int DoctorID { get; set; }
        public decimal ConsultationFee { get; set; }
        public short AvailabilityModeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
