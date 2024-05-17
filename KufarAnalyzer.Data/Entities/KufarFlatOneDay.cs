using KufarAnalyzer.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.Data.Entities
{
    public class KufarFlatOneDay : BaseEntity
    {
        public bool OnlineBookingEnabled { get; set; }

        public string District { get; set; }

        public double PriceUsd { get; set; }


    }
}
