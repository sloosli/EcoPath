using System.ComponentModel.DataAnnotations;

namespace RouteBuilderSite.Dto.Api
{
    public class RouteRequest
    {
        [Required(ErrorMessage = "Need start coordinates")]
        [MinLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        [MaxLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        public double[] From { get; }

        [Required(ErrorMessage = "Need start coordinates")]
        [MinLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        [MaxLength(2, ErrorMessage = "Must be exact 2 float numbers")]
        public double[] To { get; }

        [Required]
        public TransportType TransportType { get; }
    }
}
