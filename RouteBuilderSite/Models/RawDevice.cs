#nullable disable

namespace RouteBuilderSite.Models
{
    public partial class RawDevice
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public double? DeviceGeoX { get; set; }
        public double? DeviceGeoY { get; set; }
    }
}
