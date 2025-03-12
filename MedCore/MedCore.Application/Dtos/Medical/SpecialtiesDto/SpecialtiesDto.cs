
namespace MedCore.Application.Dtos.Medical.SpecialtiesDto
{
    public class SpecialtiesDto : DtoBase
    {
        public required string SpecialtyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
