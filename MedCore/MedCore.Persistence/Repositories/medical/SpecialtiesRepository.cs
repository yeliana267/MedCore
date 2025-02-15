﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCore.Domain.Entities;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;

namespace MedCore.Persistence.Repositories.medical
{
    public class SpecialtiesRepository : BaseRepository<Specialties, short>, ISpecialtiesRepository
    {
        public SpecialtiesRepository(MedCoreContext context) : base(context)
        {

        } 
    }
}
