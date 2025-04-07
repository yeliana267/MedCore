using MedCore.Application.Dtos.Medical.SpecialtiesDto;
using MedCore.Web.Models.Medical.SpecialtiesModels;

namespace MedCore.Web.Repositories.Medical
{
    public static class SpecialtiesFactory
    {
        public static SaveSpecialtiesDto CreateSaveDto(CreateSpecialtiesModel model)
        {
            return new SaveSpecialtiesDto
            {
                SpecialtyName = model.SpecialtyName,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static UpdateSpecialtiesDto CreateUpdateDto(EditSpecialtiesModel model)
        {
            return new UpdateSpecialtiesDto
            {
                SpecialtiesId = model.SpecialtiesId,
                SpecialtyName = model.SpecialtyName,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}