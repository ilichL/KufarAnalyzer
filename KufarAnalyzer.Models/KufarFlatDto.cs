using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.Models
{
    public class KufarFlatDto
    {
        public string Address { get; set; }

        public double PriceUsd { get; set; }

        public double? PricePerSquareMeterUsd { get; set; }

        public int? RoomCount { get; set; }

        public int? Floor { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string District { get; set; }

        public string[]? Subway { get; set; }
    }
}
