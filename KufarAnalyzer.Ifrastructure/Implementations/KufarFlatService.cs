using AutoMapper;
using KufarAnalyzer.Data;
using KufarAnalyzer.Data.Entities;
using KufarAnalyzer.DataAccess.Abstractions;
using KufarAnalyzer.Ifrastructure.Abstractions;
using KufarAnalyzer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Point = KufarAnalyzer.Models.Point;

namespace KufarAnalyzer.Ifrastructure.Implementations
{
    public class KufarFlatService : IKufarFlatService
    {
        private readonly IUnitOfWork UnitOfWork;

        private readonly IKufarFlatRepository KufarFlatRepository;

        private readonly IHttpClientFactory HttpClientFactory;

        private readonly IKufarFlatOneDayRepository KufarFlatOneDayRepository;

        private readonly IMapper Mapper;


        public KufarFlatService (IUnitOfWork unitOfWork, IKufarFlatRepository kufarFlatRepository,
            IHttpClientFactory httpClientFactory, IKufarFlatOneDayRepository kufarFlatOneDayRepository,
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            KufarFlatRepository = kufarFlatRepository;
            HttpClientFactory = httpClientFactory;
            KufarFlatOneDayRepository = kufarFlatOneDayRepository;
            Mapper = mapper;
        }


        public async Task GetFlatsOneDay()
        {
            var httpClient = HttpClientFactory.CreateClient();
            var url = "https://api.kufar.by/search-api/v2/search/rendered-paginated?" +
                "cat=1010&cur=USD&gtsy=country-belarus~province-minsk~locality-minsk" +
                "&lang=ru&rnt=2&size=30&typ=let";

            HttpResponseMessage response;

            response = await httpClient.GetAsync(url);

            var jsonText = await response.Content.ReadAsStringAsync();
            var jsonRoot = JObject.Parse(jsonText);

            var ads = (JArray)jsonRoot["ads"];
            var flats = new List<KufarFlatOneDay>();
            string district = null;
            bool onlineBookingEnabled = false;

            foreach (var ad in ads)
            {
                var priceUsdStr = ad["price_usd"].Value<string>();
                double priceUsd = 0;

                if (priceUsdStr.Length > 1)
                {
                    priceUsd = double.Parse($"{priceUsdStr[..^2]},{priceUsdStr[^2..]}");
                }

                var adParameters = (JArray)ad["ad_parameters"];

                foreach (var parameter in adParameters)
                {

                    var name = parameter["p"].Value<string>();
                    var value = parameter["v"];
                    var value1 = parameter["vl"];

                    switch (name)
                    {
                        case "area":
                            district = value1.Value<string>();
                            break;
                        case "booking_enabled":
                            onlineBookingEnabled = value.Value<bool>();
                            break;
                    }

                    flats.Add(new KufarFlatOneDay
                    {
                        Id = Guid.NewGuid(),
                        District = district,
                        PriceUsd = priceUsd
                    });
                }




                await KufarFlatOneDayRepository.AddRange(flats);
                await UnitOfWork.Commit();
            }

        }

        public async Task<KufarFlatOneDayDto> GetKufarOneDayFlatsByDistrict(string district)
        {
            return Mapper.Map<KufarFlatOneDayDto>(await KufarFlatOneDayRepository.Get()
                .Where(flat => flat.District.Equals(district))
                .GroupBy(flat => flat.PriceUsd).ToListAsync());               
        }


        public async Task GetFlats()
        {
            var httpClient = HttpClientFactory.CreateClient();

            var url = "https://api.kufar.by/search-api/v2/search" +
                "/rendered-paginated?cat=1010&cur=USD&gtsy=" +
                "country-belarus~province-minsk~locality-minsk&lang=ru&size=30&typ=sell";

            HttpResponseMessage response;

            response = await httpClient.GetAsync(url);

            var jsonText = await response.Content.ReadAsStringAsync();
            var jsonRoot = JObject.Parse(jsonText);

            var ads = (JArray)jsonRoot["ads"];
            var flats = new List<KufarFlat>();

            foreach (var ad in ads)
            {
                var priceUsdStr = ad["price_usd"].Value<string>();
                double priceUsd = 0;

                if (priceUsdStr.Length > 1)
                {
                    priceUsd = double.Parse($"{priceUsdStr[..^2]},{priceUsdStr[^2..]}");
                }


                double? pricePerSquareMeterUsd = null;
                int? roomCount = null;
                double? squareMeterSize = null;
                int? floor = null;
                int? houseMaxFloor = null;
                int? buildYear = null;
                double[] coordinates = null;
                string district = null;
                string?[] subway = null;
                var accountParameters = (JArray)ad["account_parameters"];
                var address = accountParameters.First(t => t["p"].Value<string>() == "address")["v"].Value<string>();

                var adParameters = (JArray)ad["ad_parameters"];

                foreach (var parameter in adParameters)
                {

                    var name = parameter["p"].Value<string>();
                    var value = parameter["v"];
                    var value1 = parameter["vl"];

                    switch (name)
                    {
                        case "square_meter":
                            pricePerSquareMeterUsd = value.Value<double?>();
                            break;
                        case "rooms":
                            roomCount = int.Parse(value.Value<string>());
                            break;
                        case "area":
                            district = value1.Value<string>();
                            break;
                        case "size":
                            squareMeterSize = value.Value<double?>();
                            break;
                        case "floor":
                            floor = ((JArray)value)[0].Value<int?>();
                            break;
                        case "coordinates":
                            coordinates = ((JArray)value)?.Values<double>()?
                                .ToArray();
                            break;
                        case "metro":
                            subway = ((JArray)value1)?.Values<string>()?
                                .ToArray();
                            break;
                    }
                   
                }
                if(pricePerSquareMeterUsd > 500)
                {
                    flats.Add(new KufarFlat
                    {
                        Id = Guid.NewGuid(),
                        PriceUsd = priceUsd,
                        RoomCount = roomCount,
                        Latitude = coordinates.LastOrDefault(),
                        Longitude = coordinates.FirstOrDefault(),
                        PricePerSquareMeterUsd = pricePerSquareMeterUsd,
                        Floor = floor,
                        District = district,
                        Address = address,
                        Subway = subway?.Where(f => f is not null).Take(subway.Length).ToArray(),
                    });
                }

            }

            await KufarFlatRepository.AddRange(flats);
            await UnitOfWork.Commit();
        }


        public async Task<List<FlatPricePerSquareMeterByFloor>> GetEveragePricePerSquareMeterByFloor()
        {
            var result = await KufarFlatRepository.Get()
                .Where(flat => flat.PricePerSquareMeterUsd.HasValue && flat.Floor.HasValue)
                 .GroupBy(flat => flat.Floor.Value)
                  .Select(flat => new FlatPricePerSquareMeterByFloor()
                  {
                      PricePerSquareMeterUsd = flat.Average(f => f.PricePerSquareMeterUsd.Value),
                      Floor = (int)flat.Average(f => f.Floor.Value)
                  }).ToListAsync();
            return result;
        }


        public async Task<List<FlatPricePerSquareMeterByRoom>> GetEveragePricePerSquareMeterByRoom()
        {
            var result = await KufarFlatRepository.Get()
                .Where(flat => flat.PricePerSquareMeterUsd.HasValue && flat.RoomCount.HasValue)
                 .GroupBy(flat => flat.RoomCount.Value)
                  .Select(flat => new FlatPricePerSquareMeterByRoom()
                  {
                      PricePerSquareMeterUsd = flat.Average(f => f.PricePerSquareMeterUsd.Value),
                      RoomCount = flat.Key
                  }).ToListAsync();
            return result;
        }
        

        public async Task<List<EveragePricePerSquareMeterBySubway>> GetEveragePricePerSquareMeterBySubway()
        {
            var result = await KufarFlatRepository.Get()
                .Where(flat => flat.PricePerSquareMeterUsd.HasValue && flat.Subway.Length > 0)
                .GroupBy(flat => flat.Subway.First())
                  .Select(flat => new EveragePricePerSquareMeterBySubway()
                  {
                      PricePerSquareMeterUsd = flat.Average(f => f.PricePerSquareMeterUsd.Value),
                      Subway = flat.First().Subway
                  }).ToListAsync();
            return result;
        }


        public async Task<List<KufarFlat>> GetFlatsOnMap(double latitudeA, double longitudeA, double latitudeB, double longitudeB)
        {
            var flats = await KufarFlatRepository.Get()
                .Where(flat => flat.Longitude.HasValue && flat.Latitude.HasValue)
                .ToListAsync();
            


            var result = flats.Where(f => f.Latitude > latitudeA
                                          && f.Latitude < latitudeB
                                          && f.Longitude < longitudeA
                                          && f.Longitude > longitudeB)
                .ToList();

            return result;
        }

        public async Task<List<KufarFlatDto>> IsFlatOnMap(List<Point> points)
        {
            if (points.First() != points.Last())
                return null;

            var flats = await KufarFlatRepository.Get()
                .Where(flat => flat.Latitude.HasValue && flat.Longitude.HasValue).ToListAsync();

            var result = new List<KufarFlatDto>();

            foreach(var flat in flats)
            {
                int j = points.Count - 1;
                for (int i = 0; i < points.Count; i++)
                {
                    if ((points[i].Y < flat.Longitude && points[j].Y >= flat.Longitude || points[j].Y < flat.Longitude && points[i].Y >= flat.Longitude) &&
                         (points[i].X + (flat.Longitude - points[i].Y) / (points[j].Y - points[i].Y) * (points[j].X - points[i].X) < flat.Latitude))
                    {
                        result.Add(Mapper.Map<KufarFlatDto>(flat));
                    }
                    j = i;
                }
            }

            return result;
        }
    }
}
