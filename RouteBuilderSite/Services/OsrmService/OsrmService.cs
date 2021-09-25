using Osrmnet;
using Osrmnet.RouteService;
using RouteBuilderSite.Dto.Api;
using RouteBuilderSite.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace RouteBuilderSite.Services.OsrmService
{
    public class OsrmService : IOsrmService, IDisposable
    {
        private Osrm _osrmEngine;

        public OsrmService()
        {
            var config = new EngineConfig
            {
                Algorithm = Algorithm.CH,
                StorageConfig = "Foot.osrm"
            };

            _osrmEngine = new Osrm(config);
        }

        public async Task<IList<Route>> GetRoutes(RouteRequest routeRequest)
        {
            var routeParameters = new RouteParameters
            {
                ContinueStraight = false,
                Overview = OverviewType.False,
                Geometries = GeometriesType.Polyline6,
                Annotations = AnnotationsType.Duration | AnnotationsType.Nodes,
                Steps = false,
                NumberOfAlternatives = 20,
                Coordinates = new List<Coordinate>
                {
                    new Coordinate(routeRequest.From[0], routeRequest.From[1]),
                    new Coordinate(routeRequest.To[0], routeRequest.To[1]),
                }
            };

            var routeResult = new RouteResult();
            var status = await Task.Run(() => _osrmEngine.Route(routeParameters, out routeResult));
            if (status == Status.Error)
                throw new BuildRouteException(routeRequest, "Error while building the route");

            return routeResult.Routes;
        }

        public void Dispose()
        {
            _osrmEngine.Dispose();
        }
    }
}
