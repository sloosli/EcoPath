using Microsoft.AspNetCore.Mvc;

namespace RouteBuilderSite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new OkObjectResult("Here will be the map, just imagine how cool is that");
        }
    }
}
