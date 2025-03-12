
namespace MedCore.Application.Dtos.Medical.AvailibilityModesDto
{
    public class AvailabilityModesDto : DtoBase
    {
        public required string AvailabilityMode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
