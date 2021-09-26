using RouteBuilderSite.Dto.Routes;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RouteBuilderSite.Services.OsrmService
{
    public class OsrmWebService : IOsrmService
    {
        public async Task<string> GetRoutes(RouteRequest routeRequest)
        {
            var profile = routeRequest.TransportType == Dto.Routes.TransportType.Foot ? "foot" : "bike";
            var urlQuery = $"http://router.project-osrm.org/route/v1/{profile}/" +
                $"{routeRequest.From[0].ToString(CultureInfo.InvariantCulture)}," +
                $"{routeRequest.From[1].ToString(CultureInfo.InvariantCulture)};" +
                $"{routeRequest.To[0].ToString(CultureInfo.InvariantCulture)}," +
                $"{routeRequest.To[1].ToString(CultureInfo.InvariantCulture)}" +
                $"?alternatives=true&steps=false&geometries=geojson&overview=simplified&annotations=true";

            var request = (HttpWebRequest)WebRequest.Create(urlQuery);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            var jsonString = string.Empty;


            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonString = await reader.ReadToEndAsync();
            }

            return jsonString;
        }
    }
}
