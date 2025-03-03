using MedCore.Application.Base;
using MedCore.Application.Dtos.Medical.MedicalRecordsDto;

namespace MedCore.Application.Interfaces.Medical
{
    public interface IMedicalRecordsService : IBaseService<SaveMedicalRecordsDto, UpdateMedicalRecordsDto, RemoveMedicalRecordsDto>
    {

    }
}
