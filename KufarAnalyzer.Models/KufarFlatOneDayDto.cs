using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.Models
{
    public class KufarFlatOneDayDto
    {
        public bool OnlineBookingEnabled { get; set; }

        public string District { get; set; }

        public double PriceUsd { get; set; }
    }
}
