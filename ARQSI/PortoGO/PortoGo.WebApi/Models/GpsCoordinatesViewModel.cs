using System.ComponentModel.DataAnnotations;

namespace PortoGo.WebApi.Models
{
    public class GpsCoordinatesViewModel
    {
        [Required]
        public long Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? Altitude { get; set; }
    }
}