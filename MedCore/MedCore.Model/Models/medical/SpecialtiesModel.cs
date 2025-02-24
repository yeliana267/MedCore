

namespace MedCore.Model.Models.medical
{
    public class SpecialtiesModel
    {
        public short Id { get; set; }
        public string SpecialtyName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } // Add this property to fix the error
    }
}
