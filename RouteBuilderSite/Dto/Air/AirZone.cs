using Newtonsoft.Json;
using System.Globalization;

namespace RouteBuilderSite.Dto.Air
{
    [JsonObject]
    public class AirZone
    {
        public AirZone(double longitude = 0, double latitude = 0, AirQuality airQuality = AirQuality.Unkwon)
        {
            Longitude = longitude;
            Latitude = latitude;
            AirQuality = airQuality;
        }

        [JsonProperty]
        public double Longitude { get; }
        [JsonProperty]
        public double Latitude { get; }

        [JsonProperty]
        public AirQuality AirQuality { get; }

        public override string ToString()
        {
            return $"\"Longitude\":{Longitude.ToString(CultureInfo.InvariantCulture)}," +
                $"\"Latitude\":{Latitude.ToString(CultureInfo.InvariantCulture)}," +
                $"\"AirQuality\": {(int)AirQuality}";
        }

        public string ToRouteJson()
        {
            var qualityMap = new[] { "CircleGreen", "CircleYellow", "CircleRed" };
            return $"{{\"coordinates\":[[{Latitude.ToString(CultureInfo.InvariantCulture)}," +
                $"{Longitude.ToString(CultureInfo.InvariantCulture)}]]," +
                $"\"type\":\"{qualityMap[(int)AirQuality - 1]}\"}}";
        }
    }
}
