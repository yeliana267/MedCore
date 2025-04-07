
namespace MedCore.Application.Dtos.Medical.AvailibilityModesDto
{
    public class UpdateAvailabilityModesDto : AvailabilityModesDto
    {
        public short AvailabilityModesId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
