using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCore.Domain.Entities;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.medical
{
    public interface ISpecialtiesRepository : IBaseReporsitory<Specialties, short>
    {
    }
}
