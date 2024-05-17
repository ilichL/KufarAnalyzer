using KufarAnalyzer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.Data
{
    public class KufarContext : DbContext
    {
        DbSet<KufarFlat> KufarFlats { get; set; }
        DbSet<KufarFlatOneDay> KufarFlatOneDays { get; set; }


        public KufarContext(DbContextOptions<KufarContext> options)
            : base(options) { }
    }
}
