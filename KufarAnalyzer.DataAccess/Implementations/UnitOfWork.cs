using KufarAnalyzer.Data;
using KufarAnalyzer.Data.Entities;
using KufarAnalyzer.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KufarContext Context;

        private readonly IKufarFlatRepository KufarFlatRepository;

        private readonly IKufarFlatOneDayRepository KufarFlatOneDayRepository;


        public UnitOfWork(KufarContext context, IKufarFlatRepository kufarFlatRepository, IKufarFlatOneDayRepository kufarFlatOneDayRepository)
        {
            Context = context;
            KufarFlatRepository = kufarFlatRepository;
            KufarFlatOneDayRepository = kufarFlatOneDayRepository;
        }


        public IKufarFlatOneDayRepository kufarFlatOneDay => KufarFlatOneDayRepository;

        public IKufarFlatRepository KufarFlats => KufarFlatRepository;


        public async Task<int> Commit()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
