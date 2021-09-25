using RouteBuilderSite.Dto.Api;
using System;

namespace RouteBuilderSite.Exceptions
{
    internal class BuildRouteException : Exception
    {
        public RouteRequest RouteRequest { get; }

        public BuildRouteException(RouteRequest routeRequest, string message = "") : base(message)
        {
            RouteRequest = routeRequest;
        }
    }
}
