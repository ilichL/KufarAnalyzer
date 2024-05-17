using KufarAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.Ifrastructure.Abstractions
{
    public interface IKufarFlatService
    {
        Task GetFlats();

        Task<List<FlatPricePerSquareMeterByFloor>> GetEveragePricePerSquareMeterByFloor();

        Task<List<FlatPricePerSquareMeterByRoom>> GetEveragePricePerSquareMeterByRoom();

        Task<List<EveragePricePerSquareMeterBySubway>> GetEveragePricePerSquareMeterBySubway();

        Task<KufarFlatOneDayDto> GetKufarOneDayFlatsByDistrict(string district);

        Task<List<KufarFlatDto>> IsFlatOnMap(List<Point> points);

        Task GetFlatsOneDay();
    }
}
