namespace MedCore.Web.Models.Medical.AvailabilityModes
{
    public class AvailabilityModesModel
    {
        public short AvailabilityModesId { get; set; }
        public string AvailabilityMode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}

