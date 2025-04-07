using MedCore.Application.Dtos.Medical.AvailibilityModesDto;
using MedCore.Web.Models.Medical.AvailabilityModes;

namespace MedCore.Web.Repositories.Medical
{
    public static class AvailabilityModesFactory
    {
        public static SaveAvailabilityModesDto CreateSaveDto(CreateAvailabilityModesModel model)
        {
            return new SaveAvailabilityModesDto
            {
                AvailabilityMode = model.AvailabilityMode,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static UpdateAvailabilityModesDto CreateUpdateDto(EditAvailabilityModesModel model)
        {
            return new UpdateAvailabilityModesDto
            {
                AvailabilityModesId = model.AvailabilityModesId,
                AvailabilityMode = model.AvailabilityMode,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
