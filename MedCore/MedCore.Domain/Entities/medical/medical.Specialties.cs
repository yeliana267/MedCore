
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities
{
    public class MedicalSpecialty : BaseEntity
    {
        public int SpecialityID { get; set; }
        public string SpecialityName { get; set; } = string.Empty;
    }
    
}
