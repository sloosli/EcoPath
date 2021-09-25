﻿using Osrmnet;
using RouteBuilderSite.Dto.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteBuilderSite.Services.OsrmService
{
    public interface IOsrmService
    {
        Task<string> GetRoutes(RouteRequest routeRequest);
    }
}
