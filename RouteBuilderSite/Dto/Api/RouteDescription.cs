using Osrmnet;
using System.Collections.Generic;

namespace RouteBuilderSite.Dto.Api
{
    public class RouteDescription
    {
        public IList<RouteLeg> Legs { get; set; }
        public double Duration { get; set; }
        public double Distance { get; set; }
    }
}
