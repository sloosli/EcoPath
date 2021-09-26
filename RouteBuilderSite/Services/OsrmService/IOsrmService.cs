using RouteBuilderSite.Dto.Routes;
using System.Threading.Tasks;

namespace RouteBuilderSite.Services.OsrmService
{
    public interface IOsrmService
    {
        Task<string> GetRoutes(RouteRequest routeRequest);
    }
}
