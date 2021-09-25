using System;

#nullable disable

namespace RouteBuilderSite.Models
{
    public partial class RawMeasurement
    {
        public DateTime DateMeasure { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? Co2 { get; set; }
        public decimal? Los { get; set; }
        public decimal? Pm1 { get; set; }
        public decimal? Pm25 { get; set; }
        public decimal? Pm10 { get; set; }
        public decimal? Pressure { get; set; }
        public decimal? Aqi { get; set; }
        public decimal? Formald { get; set; }
        public int DeviceId { get; set; }
    }
}
