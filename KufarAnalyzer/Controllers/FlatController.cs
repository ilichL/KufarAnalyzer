using KufarAnalyzer.Ifrastructure.Abstractions;
using KufarAnalyzer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KufarAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class FlatController : ControllerBase
    {
        private readonly IKufarFlatService kufarFlatService;

        public FlatController (IKufarFlatService kufarFlatService)
        {
            this.kufarFlatService = kufarFlatService;
        }


        [HttpGet("ParseKufarFlats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPricePerSquareMeterByFloor>))]
        public async Task<IActionResult> ParseKufarFlats()
        {
            await kufarFlatService.GetFlats();

            return Ok();
        }


        [HttpGet("GetKufarFlatsOneDay")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPricePerSquareMeterByFloor>))]
        public async Task<IActionResult> GetFlatsOneDay()
        {
            await kufarFlatService.GetFlatsOneDay();

            return Ok();
        }


        [HttpGet("PricePerSquareMeterByFloor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPricePerSquareMeterByFloor>))]
        public async Task<IActionResult> GetPricePerSquareMeterByFloor()
        {
            var result = await kufarFlatService.GetEveragePricePerSquareMeterByFloor();
            return Ok(result);
        }


        [HttpGet("PricePerSquareMeterByRoom")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPricePerSquareMeterByRoom>))]
        public async Task<IActionResult> GetEveragePricePerSquareMeterByRoom()
        {
            var result = await kufarFlatService.GetEveragePricePerSquareMeterByRoom();
            return Ok(result);
        }


        [HttpGet("PricePerSquareMeterBySubway")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EveragePricePerSquareMeterBySubway>))]
        public async Task<IActionResult> GetEveragePricePerSquareMeterBySubway()
        {
            var result = await kufarFlatService.GetEveragePricePerSquareMeterBySubway();
            return Ok(result);
        }


        [HttpGet("KufarOneDayFlatsByDistrict")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EveragePricePerSquareMeterBySubway>))]
        public async Task<IActionResult> GetKufarOneDayFlatsByDistrict(string district)
        {
            var result = await kufarFlatService.GetKufarOneDayFlatsByDistrict(district);
            return Ok(result);
        }


        [HttpGet("IsFlatOnMap")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EveragePricePerSquareMeterBySubway>))]
        public async Task<IActionResult> IsFlatOnMap(List<Point> points)
        {
            var result = await kufarFlatService.IsFlatOnMap(points);
            return Ok(result);
        }

    }
}
