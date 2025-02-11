using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    internal interface IMedicalRecordsRepository : IBaseReporsitory<MedicalRecords, int>
    {
    }
}
