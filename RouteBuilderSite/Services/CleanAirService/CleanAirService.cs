using RouteBuilderSite.Dto.Air;
using System.Linq;

namespace RouteBuilderSite.Services.CleanAirService
{
    public class CleanAirService : ICleanAirService
    {
        private readonly PostgresContext _db;

        public CleanAirService(PostgresContext postgresContext)
        {
            _db = postgresContext;
        }

        public AirZone[] GetAllAirZones() => GetAirZonesAround(0, 0, -1);

        public AirZone[] GetAirZonesAround(double lat = 0, double lon = 0, int zoom = -1)
        {
            var result = DbHelper.RawSqlQuery(_db, 
@"select device_id , device_geo_x, device_geo_y, green_index
from(
select rd.device_id, rd.device_geo_x, rd.device_geo_y, fm.green_index, dense_rank() OVER(PARTITION BY rd.device_id order by fm.date_measure desc) dr
from f_measures fm,
raw_devices rd
where 1 = 1
and fm.device_id = rd.device_id) tmp
where dr = 1
and device_geo_x is not null;", 
                x => new AirZone((double)x[1], (double)x[2], (AirQuality)(int)(double)x[3]));
            return result.ToArray();
        }

        public AirZone GetNearestAirZone(double lat, double lon)
        {
            var device = _db.RawDevices
                .OrderBy(d => (d.DeviceGeoX - lat) * (d.DeviceGeoX - lat) + (d.DeviceGeoY - lon) * (d.DeviceGeoY - lon))
                .First();
            var airQuality = (AirQuality)(int)_db.FMeasures
                .Where(fm => fm.DeviceId == device.DeviceId)
                .OrderByDescending(fm => fm.DateMeasure)
                .Select(dm => dm.GreenIndex)
                .First();
            var resultZone = new AirZone(device.DeviceGeoY ?? 0, device.DeviceGeoX ?? 0, airQuality);
            return resultZone;
        }
    }
}
