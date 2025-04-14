namespace MedCore.Web.Models.Medical.SpecialtiesModels
{
    public class SpecialtiesModel
    {
        public short SpecialtiesId { get; set; }
        public string SpecialtyName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
