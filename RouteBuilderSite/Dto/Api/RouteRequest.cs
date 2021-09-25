using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RouteBuilderSite.Dto.Api
{
    [JsonObject]
    public class RouteRequest
    {
        [Required(ErrorMessage = "Need start coordinates")]
        [MinLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        [MaxLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        public float[] From { get; set; }

        [JsonProperty]
        [Required(ErrorMessage = "Need end coordinates")]
        [MinLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        [MaxLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        public float[] To { get; set; }

        [JsonProperty]
        [Required(ErrorMessage = "Please, specify foot or bicycle")]
        public TransportType TransportType { get; set; }
    }
}
