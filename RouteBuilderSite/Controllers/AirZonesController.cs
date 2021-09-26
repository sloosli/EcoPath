using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteBuilderSite.Services.CleanAirService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteBuilderSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirZonesController : ControllerBase
    {
        private readonly ICleanAirService _cleanAirService;

        public AirZonesController(ICleanAirService cleanAirService)
        {
            _cleanAirService = cleanAirService;
        }

        [HttpGet]
        public string Get()
        {
            var zones = _cleanAirService.GetAllAirZones();
            var resultJsonBuilder = new StringBuilder("{\"routes\":[");
            for (var index = 0; index < zones.Length - 1; ++index)
            {
                resultJsonBuilder.Append(zones[index].ToRouteJson());
                resultJsonBuilder.Append(',');
            }
            resultJsonBuilder.Append(zones[zones.Length - 1].ToRouteJson());
            resultJsonBuilder.Append("]}");
            var resultJson = resultJsonBuilder.ToString();
            return resultJson;
        }
    }
}
