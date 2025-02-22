namespace MedCore.Model.Models
{
    public class AvailabilityModesModel
    {
        public short Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

    }
}
