﻿using KufarAnalyzer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.DataAccess.Abstractions
{
    public interface IUnitOfWork 
    {
        IKufarFlatRepository KufarFlats { get; }

        Task<int> Commit();
    }
}
