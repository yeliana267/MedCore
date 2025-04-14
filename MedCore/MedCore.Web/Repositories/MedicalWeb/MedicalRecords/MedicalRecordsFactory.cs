using MedCore.Application.Dtos.Medical.MedicalRecordsDto;
using MedCore.Web.Models.Medical.MedicalRecordsModels;

namespace MedCore.Web.Repositories.Medical
{
    public static class MedicalRecordsFactory
    {
        public static SaveMedicalRecordsDto CreateSaveDto(CreateMedicalRecordsModel model)
        {
            return new SaveMedicalRecordsDto
            {
                PatientID = model.PatientID,
                DoctorID = model.DoctorID,
                Diagnosis = model.Diagnosis,
                Treatment = model.Treatment,
                DateOfVisit = model.DateOfVisit,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static UpdateMedicalRecordsDto CreateUpdateDto(EditMedicalRecordsModel model)
        {
            return new UpdateMedicalRecordsDto
            {
                MedicalRecordsId = model.MedicalRecordId,
                PatientID = model.PatientID,
                DoctorID = model.DoctorID,
                Diagnosis = model.Diagnosis,
                Treatment = model.Treatment,
                DateOfVisit = model.DateOfVisit,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}