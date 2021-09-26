using RouteBuilderSite.Dto.Air;

namespace RouteBuilderSite.Services.CleanAirService
{
    public interface ICleanAirService
    {
        AirZone[] GetAllAirZones();

        AirZone[] GetAirZonesAround(double lat, double lon, int zoom);

        AirZone GetNearestAirZone(double lat, double lon);
    }
}
