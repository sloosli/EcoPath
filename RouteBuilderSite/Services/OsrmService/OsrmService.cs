using Osrmnet;
using Osrmnet.RouteService;
using RouteBuilderSite.Dto.Routes;
using RouteBuilderSite.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteBuilderSite.Services.OsrmService
{
    public class OsrmService : IDisposable//, IOsrmService
    {
        private readonly Dictionary<TransportType, Osrm> _osrmEngineTransportMap =
            new Dictionary<TransportType, Osrm>();

        public OsrmService()
        {
            var bikeConfig = new EngineConfig
            {
                Algorithm = Algorithm.CH,
                StorageConfig = "bicycle.osrm"
            };
            var osrmBikeEngine = new Osrm(bikeConfig);
            _osrmEngineTransportMap.Add(TransportType.Bike, osrmBikeEngine);
            var scooterConfig = new EngineConfig
            {
                Algorithm = Algorithm.CH,
                StorageConfig = "scooter.osrm"
            };
            var osrmScooterEngine = new Osrm(scooterConfig);
            _osrmEngineTransportMap.Add(TransportType.Scooter, osrmScooterEngine);
            var footConfig = new EngineConfig
            {
                Algorithm = Algorithm.CH,
                StorageConfig = "foot.osrm"
            };
            var osrmFootEngine = new Osrm(footConfig);
            _osrmEngineTransportMap.Add(TransportType.Foot, osrmFootEngine);
        }

        public async Task<IList<Route>> GetRoutes(RouteRequest routeRequest)
        {
            var routeParameters = new RouteParameters
            {
                ContinueStraight = false,
                Overview = OverviewType.Simplified,
                Geometries = GeometriesType.GeoJSON,
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
            var currentOsrmEngine = _osrmEngineTransportMap.GetValueOrDefault(routeRequest.TransportType);
            var status = await Task.Run(() => currentOsrmEngine.Route(routeParameters, out routeResult));
            if (status == Status.Error)
                throw new BuildRouteException(routeRequest, "Error while building the route");

            return routeResult.Routes;
        }

        public void Dispose()
        {
            foreach (var osrmEngineTransportPair in _osrmEngineTransportMap)
            {
                osrmEngineTransportPair.Value.Dispose();
            }
        }
    }
}
