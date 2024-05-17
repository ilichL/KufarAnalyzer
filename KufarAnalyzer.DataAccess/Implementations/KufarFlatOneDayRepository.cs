using KufarAnalyzer.Data;
using KufarAnalyzer.Data.Entities;
using KufarAnalyzer.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.DataAccess.Implementations
{
    public class KufarFlatOneDayRepository :Repository<KufarFlatOneDay>, IKufarFlatOneDayRepository
    {
        public KufarFlatOneDayRepository(KufarContext Context) : base(Context)
        {
        }
    }
}
