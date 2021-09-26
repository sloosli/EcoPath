using Microsoft.AspNetCore.Mvc;
using RouteBuilderSite.Dto.Routes;
using RouteBuilderSite.Services.CleanAirService;
using RouteBuilderSite.Services.OsrmService;
using System.Threading.Tasks;

namespace RouteBuilderSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteBuilderController : ControllerBase
    {
        private readonly IOsrmService _osrmService;
        private readonly ICleanAirService _cleanAirService;

        public RouteBuilderController(IOsrmService osrmService, ICleanAirService cleanAirService)
        {
            _osrmService = osrmService;
            _cleanAirService = cleanAirService;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] RouteRequest routeRequest)
        {
            if (routeRequest.TransportType == TransportType.Other)
                return "We currently not support other transport";

            try
            {
                return await _osrmService.GetRoutes(routeRequest);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                return "We are so sorry but here is an error";
            }
        }

        [HttpGet]
        public string Get()
        {
            return "Hello world!";
        }
    }
}
