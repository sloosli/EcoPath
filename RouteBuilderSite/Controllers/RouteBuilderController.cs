using Microsoft.AspNetCore.Mvc;
using RouteBuilderSite.Dto.Api;
using RouteBuilderSite.Services.OsrmService;
using System.Linq;
using System.Threading.Tasks;

namespace RouteBuilderSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteBuilderController : ControllerBase
    {
        private readonly IOsrmService _osrmService;

        public RouteBuilderController(IOsrmService osrmService)
        {
            _osrmService = osrmService;
        }

        [HttpPost]
        public async Task<JsonResult> Post(RouteRequest routeRequest)
        {
            if (!ModelState.IsValid)
                return new JsonResult(new { error = "Bad request" })
                {
                    StatusCode = 400
                };

            var routes = await _osrmService.GetRoutes(routeRequest);
            return new JsonResult(new RouteResponse { Routes = routes.ToArray() });
        }

        [HttpGet]
        public string Get()
        {
            return "Hello world!";
        }
    }
}
