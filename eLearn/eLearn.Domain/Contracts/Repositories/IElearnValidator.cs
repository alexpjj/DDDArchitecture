﻿using eLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Contracts.Repositories
{
    public interface IElearnValidator : Core.IRepository<ElearnValidator, long>
    {
    }
}
